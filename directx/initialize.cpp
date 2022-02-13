#include "_directx.hpp"

bool CreateDrawingDevices() {
    ComPtr<IDXGIFactory> p_factory = nullptr;
    if (FAILED(CreateDXGIFactory(__uuidof(IDXGIFactory), (void**)p_factory.GetAddressOf())))
        return Error("Failed to create IDXGIFactory.");

    D3D_FEATURE_LEVEL feature_levels[] = {
        D3D_FEATURE_LEVEL_11_1,
        D3D_FEATURE_LEVEL_11_0,
    };
    if (FAILED(D3D11CreateDevice(nullptr, D3D_DRIVER_TYPE_HARDWARE, nullptr, D3D11_CREATE_DEVICE_BGRA_SUPPORT,
            feature_levels, ARRAYSIZE(feature_levels), D3D11_SDK_VERSION, g_pDevice.GetAddressOf(), nullptr,
            g_pContext.GetAddressOf())))
        return Error("Failed to create Direct3D11 device.");

    DXGI_SWAP_CHAIN_DESC desc_swapchain = {
        {g_width, g_height, {60U, 1U}, DXGI_FORMAT_R8G8B8A8_UNORM, DXGI_MODE_SCANLINE_ORDER_UNSPECIFIED,
            DXGI_MODE_SCALING_UNSPECIFIED},
        {1, 0},
        DXGI_USAGE_RENDER_TARGET_OUTPUT,
        1U,
        g_hWnd,
        true,
        DXGI_SWAP_EFFECT_DISCARD,
        DXGI_SWAP_CHAIN_FLAG_ALLOW_MODE_SWITCH,
    };

    if (FAILED(p_factory->CreateSwapChain(g_pDevice.Get(), &desc_swapchain, g_pSwapchain.GetAddressOf())))
        return Error("Failed to create swapchain.");

    return true;
}

bool CreateBackBuffer() {
    ComPtr<ID3D11Texture2D> p_backbuffer = nullptr;
    if (FAILED(g_pSwapchain->GetBuffer(0, __uuidof(ID3D11Texture2D), (void**)p_backbuffer.GetAddressOf())))
        return Error("Failed to get backbuffer.");
    if (FAILED(g_pDevice->CreateRenderTargetView(p_backbuffer.Get(), nullptr, g_pBackbuffer.GetAddressOf())))
        return Error("Failed to create render target view.");
    return true;
}

bool CreateShader() {
    if (FAILED(g_pDevice->CreateVertexShader(g_vs_main, sizeof(g_vs_main), nullptr, g_pVShader.GetAddressOf())))
        return Error("Failed to create vertex shader.");
    if (FAILED(g_pDevice->CreatePixelShader(g_ps_main, sizeof(g_ps_main), nullptr, g_pPShader.GetAddressOf())))
        return Error("Failed to create pixel shader.");

    D3D11_INPUT_ELEMENT_DESC desc_elem[] = {
        {"POSITION", 0, DXGI_FORMAT_R32G32B32_FLOAT, 0, 0, D3D11_INPUT_PER_VERTEX_DATA, 0},
        {"COLOR", 0, DXGI_FORMAT_R32G32B32A32_FLOAT, 0, D3D11_APPEND_ALIGNED_ELEMENT, D3D11_INPUT_PER_VERTEX_DATA, 0},
        {"NORMAL", 0, DXGI_FORMAT_R32G32B32_FLOAT, 0, D3D11_APPEND_ALIGNED_ELEMENT, D3D11_INPUT_PER_VERTEX_DATA, 0},
        {"TEXCOORD", 0, DXGI_FORMAT_R32G32_FLOAT, 0, D3D11_APPEND_ALIGNED_ELEMENT, D3D11_INPUT_PER_VERTEX_DATA, 0},
    };
    if (FAILED(g_pDevice->CreateInputLayout(
            desc_elem, ARRAYSIZE(desc_elem), g_vs_main, sizeof(g_vs_main), g_pILayout.GetAddressOf())))
        return Error("Failed to create input element.");

    D3D11_BUFFER_DESC desc_cb = {sizeof(ConstantBuffer), D3D11_USAGE_DEFAULT, D3D11_BIND_CONSTANT_BUFFER, 0U, 0U, 0U};
    if (FAILED(g_pDevice->CreateBuffer(&desc_cb, nullptr, g_pCBuffer.GetAddressOf())))
        return Error("Failed to create constant buf.");

    memset(&g_cbuffer, 0, sizeof(ConstantBuffer));
    SetMatrixScale(1.0f, 1.0f, 1.0f);
    SetMatrixRotate(0.0f, 0.0f, 0.0f);
    SetMatrixTranslate(0.0f, 0.0f, 0.0f);
    SetMatrixView(0.0f, 0.0f, -10.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f, 0.0f);
    SetMatrixProject(g_width, g_height, 0.0f, 1.0f, 2000.0f, false);
    SetVectorColor(1.0f, 1.0f, 1.0f, 1.0f);
    SetVectorParams(0.0f, 0.0f, 0.0f, 0.0f);

    return true;
}

bool SetRenderConfigure() {
    D3D11_VIEWPORT viewport = {0.0f, 0.0f, (float)g_width, (float)g_height, 0.0f, 1.0f};
    g_pContext->RSSetViewports(1U, &viewport);

    D3D11_BLEND_DESC desc_blend;
    memset(&desc_blend, 0, sizeof(D3D11_BLEND_DESC));
    desc_blend.AlphaToCoverageEnable = FALSE;
    desc_blend.IndependentBlendEnable = FALSE;
    desc_blend.RenderTarget[0].BlendEnable = TRUE;
    desc_blend.RenderTarget[0].SrcBlend = D3D11_BLEND_SRC_ALPHA;
    desc_blend.RenderTarget[0].DestBlend = D3D11_BLEND_INV_SRC_ALPHA;
    desc_blend.RenderTarget[0].BlendOp = D3D11_BLEND_OP_ADD;
    desc_blend.RenderTarget[0].SrcBlendAlpha = D3D11_BLEND_ONE;
    desc_blend.RenderTarget[0].DestBlendAlpha = D3D11_BLEND_ONE;
    desc_blend.RenderTarget[0].BlendOpAlpha = D3D11_BLEND_OP_ADD;
    desc_blend.RenderTarget[0].RenderTargetWriteMask = D3D11_COLOR_WRITE_ENABLE_ALL;
    ComPtr<ID3D11BlendState> p_bstate = nullptr;
    if (FAILED(g_pDevice->CreateBlendState(&desc_blend, p_bstate.GetAddressOf())))
        return Error("Failed to create blend state.");
    float blend_factor[4] = {D3D11_BLEND_ZERO, D3D11_BLEND_ZERO, D3D11_BLEND_ZERO, D3D11_BLEND_ZERO};
    g_pContext->OMSetBlendState(p_bstate.Get(), blend_factor, 0xffffffff);

    g_pContext->IASetPrimitiveTopology(D3D11_PRIMITIVE_TOPOLOGY_TRIANGLELIST);
    g_pContext->IASetInputLayout(g_pILayout.Get());
    g_pContext->VSSetShader(g_pVShader.Get(), nullptr, 0U);
    g_pContext->PSSetShader(g_pPShader.Get(), nullptr, 0U);

    return true;
}

bool CreateIdea() {
    g_idea.num_idx = 6U;
    struct Vertex data_pcnu[4U] = {
        {-0.5f, -0.5f, +0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f, 0.0f, -1.0f, 0.0f, 1.0f},
        {-0.5f, +0.5f, +0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f, 0.0f, -1.0f, 0.0f, 0.0f},
        {+0.5f, +0.5f, +0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f, 0.0f, -1.0f, 1.0f, 0.0f},
        {+0.5f, -0.5f, +0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f, 0.0f, -1.0f, 1.0f, 1.0f},
    };
    unsigned int data_indx[6U] = {0, 1, 2, 0, 2, 3};
    return CreateModelBuffer(g_idea.num_idx, data_pcnu, data_indx, &g_idea);
}

bool InitializeDirect2D() {
    ComPtr<ID2D1Factory> p_factory = nullptr;
    if (FAILED(D2D1CreateFactory(D2D1_FACTORY_TYPE_SINGLE_THREADED, p_factory.GetAddressOf())))
        return Error("Failed to create Direct2D factory.");

    ComPtr<IDXGISurface> p_backbuffer = nullptr;
    if (FAILED(g_pSwapchain->GetBuffer(0, __uuidof(IDXGISurface), (void**)p_backbuffer.GetAddressOf())))
        return Error("Failed to create backbuffer for Direct2D.");

    float dpi_x, dpi_y;
    p_factory->GetDesktopDpi(&dpi_x, &dpi_y);
    D2D1_RENDER_TARGET_PROPERTIES props = D2D1::RenderTargetProperties(D2D1_RENDER_TARGET_TYPE_DEFAULT,
        D2D1::PixelFormat(DXGI_FORMAT_UNKNOWN, D2D1_ALPHA_MODE_PREMULTIPLIED), dpi_x, dpi_y);
    if (FAILED(p_factory->CreateDxgiSurfaceRenderTarget(p_backbuffer.Get(), &props, g_pD2DRT.GetAddressOf())))
        return Error("Failed to create Direct2D render target.");

    if (FAILED(g_pD2DRT->CreateSolidColorBrush(D2D1::ColorF(D2D1::ColorF::WhiteSmoke), g_pD2DBrush.GetAddressOf())))
        return Error("Failed to create brush.");

    if (FAILED(DWriteCreateFactory(DWRITE_FACTORY_TYPE_SHARED, __uuidof(IDWriteFactory),
            reinterpret_cast<IUnknown**>(g_pDWFactory.GetAddressOf()))))
        return Error("Failed to create DirectDraw factory.");

    return true;
}
