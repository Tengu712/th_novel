#include "_directx.hpp"

HWND g_hWnd = nullptr;
unsigned int g_width = 0;
unsigned int g_height = 0;
ComPtr<ID3D11Device> g_pDevice = nullptr;
ComPtr<ID3D11DeviceContext> g_pContext = nullptr;
ComPtr<IDXGISwapChain> g_pSwapchain = nullptr;
ComPtr<ID3D11RenderTargetView> g_pBackbuffer = nullptr;
ComPtr<ID3D11VertexShader> g_pVShader = nullptr;
ComPtr<ID3D11PixelShader> g_pPShader = nullptr;
ComPtr<ID3D11InputLayout> g_pILayout = nullptr;
ComPtr<ID3D11Buffer> g_pCBuffer = nullptr;
ConstantBuffer g_cbuffer;
ModelBuffer g_idea;
ComPtr<ID2D1RenderTarget> g_pD2DRT = nullptr;
ComPtr<ID2D1SolidColorBrush> g_pD2DBrush = nullptr;
ComPtr<IDWriteFactory> g_pDWFactory = nullptr;
std::unordered_map<std::string, ComPtr<ID3D11ShaderResourceView>> g_umImages;
