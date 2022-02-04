#pragma once

#ifndef _HEADER_DIRECTX_HPP_
#define _HEADER_DIRECTX_HPP_

#include <DirectXMath.h>
#include <d3d11.h>
#include <windows.h>
#include <wrl/client.h>

#include "_pshader.h"
#include "_vshader.h"

#pragma comment(lib, "user32.lib")
#pragma comment(lib, "d3d11.lib")
#pragma comment(lib, "dxgi.lib")

#define DLLEXPORT extern "C" __declspec(dllexport)

using Microsoft::WRL::ComPtr;

struct ConstantBuffer {
    DirectX::XMFLOAT4X4 mat_scl;
    DirectX::XMFLOAT4X4 mat_rot;
    DirectX::XMFLOAT4X4 mat_trs;
    DirectX::XMFLOAT4X4 mat_view;
    DirectX::XMFLOAT4X4 mat_proj;
    DirectX::XMFLOAT4 vec_color;
    DirectX::XMFLOAT4 vec_light;
    DirectX::XMFLOAT4 params;
};

extern ConstantBuffer g_cbuffer;

inline bool Error(const char* msg) {
    MessageBoxA(nullptr, msg, "Error", MB_OK | MB_ICONERROR);
    return false;
}

void SetMatrixScale(float scl_x, float scl_y, float scl_z);
void SetMatrixRotate(float deg_x, float deg_y, float deg_z);
void SetMatrixTranslate(float pos_x, float pos_y, float pos_z);
void SetMatrixView(float pos_x, float pos_y, float pos_z, float dir_x, float dir_y, float dir_z, float upp_x,
    float upp_y, float upp_z);
void SetMatrixProject(float width, float height, float angle, float near_z, float far_z, bool parse);
void SetVectorColor(float col_r, float col_g, float col_b, float col_a);
void SetVectorLight(float x, float y, float z, float w);
void SetVectorParams(float x, float y, float z, float w);
void SetVectorParamsX(float x);
void SetVectorParamsY(float y);
void SetVectorParamsZ(float z);
void SetVectorParamsW(float w);

#endif
