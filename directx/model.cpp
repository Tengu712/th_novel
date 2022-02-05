#include "_directx.hpp"

bool CreateModelBuffer(unsigned int num_vtx, Vertex* data_vtx, unsigned int* data_indx, ModelBuffer* p_mbuf) {
    D3D11_BUFFER_DESC desc_vbuffer = {
        sizeof(Vertex) * num_vtx, D3D11_USAGE_DEFAULT, D3D11_BIND_VERTEX_BUFFER, 0U, 0U, 0U};
    D3D11_SUBRESOURCE_DATA dataVBuffer = {data_vtx, 0U, 0U};
    if (FAILED(g_pDevice->CreateBuffer(&desc_vbuffer, &dataVBuffer, p_mbuf->p_vbuffer.GetAddressOf())))
        return Error("Failed to create vertex buffer.");
    D3D11_BUFFER_DESC desc_index = {
        sizeof(unsigned int) * p_mbuf->num_idx, D3D11_USAGE_DEFAULT, D3D11_BIND_INDEX_BUFFER, 0U, 0U, 0U};
    D3D11_SUBRESOURCE_DATA dataIndex = {data_indx, 0U, 0U};
    if (FAILED(g_pDevice->CreateBuffer(&desc_index, &dataIndex, p_mbuf->p_ibuffer.GetAddressOf())))
        return Error("Failed to create index buffer.");
    return true;
}

void DrawModel(ModelBuffer* p_mbuf) {
    unsigned int strides = sizeof(Vertex);
    unsigned int offsets = 0U;
    g_pContext->IASetVertexBuffers(0U, 1U, p_mbuf->p_vbuffer.GetAddressOf(), &strides, &offsets);
    g_pContext->IASetIndexBuffer(p_mbuf->p_ibuffer.Get(), DXGI_FORMAT_R32_UINT, 0U);
    g_pContext->UpdateSubresource(g_pCBuffer.Get(), 0U, nullptr, &g_cbuffer, 0U, 0U);
    g_pContext->VSSetConstantBuffers(0U, 1U, g_pCBuffer.GetAddressOf());
    g_pContext->DrawIndexed(p_mbuf->num_idx, 0U, 0U);
}
