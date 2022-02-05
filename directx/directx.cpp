#include "_directx.hpp"

DLLEXPORT bool __stdcall InitializeDirectX(int h_wnd, unsigned int width, unsigned int height) {
    g_hWnd = (HWND)h_wnd;
    g_width = width;
    g_height = height;
    bool res = true;
    res = res && CreateDrawingDevices();
    res = res && CreateBackBuffer();
    res = res && CreateShader();
    res = res && SetRenderConfigure();
    res = res && CreateIdea();
    return res;
}

DLLEXPORT void __stdcall ClearSetBackBuffer(float r, float g, float b) {
    float clearColor[4] = {r, g, b, 1.0f};
    g_pContext->OMSetRenderTargets(1U, g_pBackbuffer.GetAddressOf(), nullptr);
    g_pContext->ClearRenderTargetView(g_pBackbuffer.Get(), clearColor);
}

DLLEXPORT void __stdcall Present() {
    g_pSwapchain->Present(1U, 0U);
}

DLLEXPORT bool __stdcall LoadImageWithKey(const char* key, int data, unsigned int width, unsigned int height) {
    return CreateImageShaderResourceView((unsigned char*)data, width, height, &g_umImages[key]);
}

DLLEXPORT void __stdcall DrawImageWithKey(const char* key, float pos_x, float pos_y, float scl_x, float scl_y,
    float deg, float r, float g, float b, float a) {
    if (g_umImages.find(key) == g_umImages.end()) {
        g_cbuffer.params.x = 0.0f;
    } else {
        g_pContext->PSSetShaderResources(0U, 1U, g_umImages[key].GetAddressOf());
        g_cbuffer.params.x = 1.0f;
    }
    SetMatrixScale(scl_x, scl_y, 1.0f);
    SetMatrixRotate(0.0f, 0.0, deg);
    SetMatrixTranslate(pos_x, pos_x, 0.0f);
    SetVectorColor(r, g, b, a);
    DrawModel(&g_idea);
}
