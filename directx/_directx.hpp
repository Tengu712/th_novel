#pragma once

#ifndef _HEADER_DIRECTX_HPP_
#define _HEADER_DIRECTX_HPP_

#include <DirectXMath.h>
#include <d2d1.h>
#include <d3d11.h>
#include <dwrite.h>
#include <windows.h>
#include <wrl/client.h>

#include <string>
#include <unordered_map>

#include "_pshader.h"
#include "_vshader.h"

#pragma comment(lib, "user32.lib")
#pragma comment(lib, "d2d1.lib")
#pragma comment(lib, "d3d11.lib")
#pragma comment(lib, "dwrite.lib")
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
    DirectX::XMFLOAT4 params;
};

struct Vertex {
    float vertex_data[12];
};

struct ModelBuffer {
    unsigned int num_idx;
    ComPtr<ID3D11Buffer> p_vbuffer;
    ComPtr<ID3D11Buffer> p_ibuffer;
};

extern HWND g_hWnd;
extern unsigned int g_width;
extern unsigned int g_height;
extern ComPtr<ID3D11Device> g_pDevice;
extern ComPtr<ID3D11DeviceContext> g_pContext;
extern ComPtr<IDXGISwapChain> g_pSwapchain;
extern ComPtr<ID3D11RenderTargetView> g_pBackbuffer;
extern ComPtr<ID3D11VertexShader> g_pVShader;
extern ComPtr<ID3D11PixelShader> g_pPShader;
extern ComPtr<ID3D11InputLayout> g_pILayout;
extern ComPtr<ID3D11Buffer> g_pCBuffer;
extern ConstantBuffer g_cbuffer;
extern ModelBuffer g_idea;
extern ComPtr<ID2D1RenderTarget> g_pD2DRT;
extern ComPtr<ID2D1SolidColorBrush> g_pD2DBrush;
extern ComPtr<IDWriteTextFormat> g_pDWTextformat;
extern std::unordered_map<std::string, ComPtr<ID3D11ShaderResourceView>> g_umImages;

inline bool Error(const char* msg) {
    MessageBoxA(nullptr, msg, "Error", MB_OK | MB_ICONERROR);
    return false;
}

bool CreateDrawingDevices();
bool CreateBackBuffer();
bool CreateShader();
bool SetRenderConfigure();
bool CreateIdea();
bool InitializeDirect2D();

void SetMatrixScale(float scl_x, float scl_y, float scl_z);
void SetMatrixRotate(float deg_x, float deg_y, float deg_z);
void SetMatrixTranslate(float pos_x, float pos_y, float pos_z);
void SetMatrixView(float pos_x, float pos_y, float pos_z, float dir_x, float dir_y, float dir_z, float upp_x,
    float upp_y, float upp_z);
void SetMatrixProject(float width, float height, float angle, float near_z, float far_z, bool parse);
void SetVectorColor(float col_r, float col_g, float col_b, float col_a);
void SetVectorParams(float x, float y, float z, float w);
void SetVectorParamsX(float x);
void SetVectorParamsY(float y);
void SetVectorParamsZ(float z);
void SetVectorParamsW(float w);

bool CreateModelBuffer(unsigned int num_vtx, Vertex* data_vtx, unsigned int* data_indx, ModelBuffer* p_mbuf);
void DrawModel(ModelBuffer* p_mbuf);

bool CreateImageShaderResourceView(
    unsigned char* data, unsigned int width, unsigned int height, ComPtr<ID3D11ShaderResourceView>* p_pSRView);

#endif
