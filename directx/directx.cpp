#include "_directx.hpp"

HWND g_hWnd = nullptr;
unsigned int g_width = 0;
unsigned int g_height = 0;
ComPtr<ID3D11Device> g_pDevice = nullptr;
ComPtr<ID3D11DeviceContext> g_pContext = nullptr;
ComPtr<IDXGISwapChain> g_pSwapchain = nullptr;
ComPtr<ID3D11RenderTargetView> g_pBackbuffer = nullptr;
ComPtr<ID3D11DepthStencilView> g_pDSView = nullptr;
ComPtr<ID3D11VertexShader> g_pVShader = nullptr;
ComPtr<ID3D11PixelShader> g_pPShader = nullptr;
ComPtr<ID3D11InputLayout> g_pILayout = nullptr;
ComPtr<ID3D11Buffer> g_pCBuffer = nullptr;
ConstantBuffer g_cbuffer;

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

    D3D11_DEPTH_STENCIL_DESC desc_dsbuf = {true, D3D11_DEPTH_WRITE_MASK_ALL, D3D11_COMPARISON_LESS, false,
        D3D11_DEFAULT_STENCIL_READ_MASK, D3D11_DEFAULT_STENCIL_WRITE_MASK, D3D11_STENCIL_OP_KEEP,
        D3D11_STENCIL_OP_KEEP};
    ComPtr<ID3D11DepthStencilState> p_dsstate = nullptr;
    if (FAILED(g_pDevice->CreateDepthStencilState(&desc_dsbuf, p_dsstate.GetAddressOf())))
        return Error("Failed to create depth stancil stencil.");
    g_pContext->OMSetDepthStencilState(p_dsstate.Get(), 0U);

    D3D11_TEXTURE2D_DESC desc_dstex = {g_width, g_height, 1U, 1U, DXGI_FORMAT_R24G8_TYPELESS, {1, 0},
        D3D11_USAGE_DEFAULT, D3D11_BIND_DEPTH_STENCIL | D3D11_BIND_SHADER_RESOURCE, 0U, 0U};
    ComPtr<ID3D11Texture2D> p_dstex = nullptr;
    if (FAILED(g_pDevice->CreateTexture2D(&desc_dstex, nullptr, p_dstex.GetAddressOf())))
        return Error("Failed to create depth stencil buffer texture.");
    D3D11_DEPTH_STENCIL_VIEW_DESC desc_view = {DXGI_FORMAT_D24_UNORM_S8_UINT, D3D11_DSV_DIMENSION_TEXTURE2D, 0U, {0U}};
    if (FAILED(g_pDevice->CreateDepthStencilView(p_dstex.Get(), &desc_view, g_pDSView.GetAddressOf())))
        return Error("Failed to create depth stencil buf view.");

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
    SetMatrixView(0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f, 0.0f);
    SetMatrixProject(g_width, g_height, DirectX::XM_PIDIV4, 1.0f, 2000.0f, true);
    SetVectorColor(1.0f, 1.0f, 1.0f, 1.0f);
    SetVectorLight(0.0f, 0.0f, 1.0f, 0.0f);
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

DLLEXPORT bool __stdcall InitializeDirectX(int h_wnd, unsigned int width, unsigned int height) {
    g_hWnd = (HWND)h_wnd;
    g_width = width;
    g_height = height;
    bool res = true;
    res = res && CreateDrawingDevices();
    res = res && CreateBackBuffer();
    res = res && CreateShader();
    res = res && SetRenderConfigure();
    return res;
}

DLLEXPORT void __stdcall ClearSetBackBuffer(float r, float g, float b) {
    float clearColor[4] = {r, g, b, 1.0f};
    g_pContext->OMSetRenderTargets(1U, g_pBackbuffer.GetAddressOf(), g_pDSView.Get());
    g_pContext->ClearRenderTargetView(g_pBackbuffer.Get(), clearColor);
    g_pContext->ClearDepthStencilView(g_pDSView.Get(), D3D11_CLEAR_DEPTH | D3D11_CLEAR_STENCIL, 1.0f, 0U);
}

DLLEXPORT void __stdcall Present() {
    g_pSwapchain->Present(1U, 0U);
}
