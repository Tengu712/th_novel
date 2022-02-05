#include "_directx.hpp"

void SetMatrixScale(float scl_x, float scl_y, float scl_z) {
    DirectX::XMStoreFloat4x4(
        &g_cbuffer.mat_scl, DirectX::XMMatrixTranspose(DirectX::XMMatrixScaling(scl_x, scl_y, scl_z)));
}

void SetMatrixRotate(float deg_x, float deg_y, float deg_z) {
    DirectX::XMStoreFloat4x4(&g_cbuffer.mat_rot,
        DirectX::XMMatrixTranspose(DirectX::XMMatrixRotationRollPitchYaw(DirectX::XMConvertToRadians(deg_x),
            DirectX::XMConvertToRadians(deg_y), DirectX::XMConvertToRadians(deg_z))));
}

void SetMatrixTranslate(float pos_x, float pos_y, float pos_z) {
    DirectX::XMStoreFloat4x4(
        &g_cbuffer.mat_trs, DirectX::XMMatrixTranspose(DirectX::XMMatrixTranslation(pos_x, pos_y, pos_z)));
}

void SetMatrixView(float pos_x, float pos_y, float pos_z, float dir_x, float dir_y, float dir_z, float upp_x,
    float upp_y, float upp_z) {
    DirectX::XMStoreFloat4x4(&g_cbuffer.mat_view,
        DirectX::XMMatrixTranspose(DirectX::XMMatrixLookToLH(DirectX::XMVectorSet(pos_x, pos_y, pos_z, 0.0f),
            DirectX::XMVectorSet(dir_x, dir_y, dir_z, 0.0f), DirectX::XMVectorSet(upp_x, upp_y, upp_z, 0.0f))));
}

void SetMatrixProject(float width, float height, float angle, float near_z, float far_z, bool parse) {
    DirectX::XMStoreFloat4x4(&g_cbuffer.mat_proj,
        DirectX::XMMatrixTranspose(parse ? DirectX::XMMatrixPerspectiveFovLH(angle, width / height, near_z, far_z)
                                         : DirectX::XMMatrixOrthographicLH(width, height, near_z, far_z)));
}

void SetVectorColor(float col_r, float col_g, float col_b, float col_a) {
    DirectX::XMStoreFloat4(&g_cbuffer.vec_color, DirectX::XMVectorSet(col_r, col_g, col_b, col_a));
}

void SetVectorParams(float x, float y, float z, float w) {
    DirectX::XMStoreFloat4(&g_cbuffer.params, DirectX::XMVectorSet(x, y, z, w));
}

void SetVectorParamsX(float x) {
    DirectX::XMStoreFloat4(
        &g_cbuffer.params, DirectX::XMVectorSet(x, g_cbuffer.params.y, g_cbuffer.params.z, g_cbuffer.params.w));
}

void SetVectorParamsY(float y) {
    DirectX::XMStoreFloat4(
        &g_cbuffer.params, DirectX::XMVectorSet(g_cbuffer.params.x, y, g_cbuffer.params.z, g_cbuffer.params.w));
}

void SetVectorParamsZ(float z) {
    DirectX::XMStoreFloat4(
        &g_cbuffer.params, DirectX::XMVectorSet(g_cbuffer.params.x, g_cbuffer.params.y, z, g_cbuffer.params.w));
}

void SetVectorParamsW(float w) {
    DirectX::XMStoreFloat4(
        &g_cbuffer.params, DirectX::XMVectorSet(g_cbuffer.params.w, g_cbuffer.params.y, g_cbuffer.params.z, w));
}
