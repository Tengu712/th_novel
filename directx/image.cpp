#include "_directx.hpp"

bool CreateImageShaderResourceView(
    unsigned char* data, unsigned int width, unsigned int height, ComPtr<ID3D11ShaderResourceView>* p_pSRView) {
    
    D3D11_TEXTURE2D_DESC desc_tex = {width, height, 1, 1, DXGI_FORMAT_B8G8R8A8_UNORM, {1, 0}, D3D11_USAGE_DYNAMIC,
        D3D11_BIND_SHADER_RESOURCE, D3D11_CPU_ACCESS_WRITE, 0};
    ComPtr<ID3D11Texture2D> p_layer = nullptr;
    if (FAILED(g_pDevice->CreateTexture2D(&desc_tex, nullptr, p_layer.GetAddressOf())))
        return Error("Failed to create font texture.");

    D3D11_MAPPED_SUBRESOURCE res_mapped;
    g_pContext->Map(p_layer.Get(), 0U, D3D11_MAP_WRITE_DISCARD, 0U, &res_mapped);
    unsigned char* p_bits = (unsigned char*)res_mapped.pData;
    memcpy(p_bits, data, width * height * 4);
    g_pContext->Unmap(p_layer.Get(), 0U);

    D3D11_SHADER_RESOURCE_VIEW_DESC desc_srview;
    ZeroMemory(&desc_srview, sizeof(D3D11_SHADER_RESOURCE_VIEW_DESC));
    desc_srview.Format = desc_tex.Format;
    desc_srview.ViewDimension = D3D11_SRV_DIMENSION_TEXTURE2D;
    desc_srview.Texture2D.MostDetailedMip = 0;
    desc_srview.Texture2D.MipLevels = desc_tex.MipLevels;
    if (FAILED(g_pDevice->CreateShaderResourceView(p_layer.Get(), &desc_srview, p_pSRView->GetAddressOf())))
        return Error("Failed to create shader resource view.");

    return true;
}
