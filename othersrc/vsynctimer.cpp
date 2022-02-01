#include <dxgi.h>
#include <windows.h>
#include <wrl/client.h>

#pragma comment(lib, "user32.lib")
#pragma comment(lib, "dxgi.lib")

#define DLLEXPORT extern "C" __declspec(dllexport)

using Microsoft::WRL::ComPtr;

ComPtr<IDXGIOutput> g_pOutput = nullptr;

DLLEXPORT bool __stdcall CreateVSyncTimer() {
    ComPtr<IDXGIFactory1> p_factory = nullptr;
    if (FAILED(CreateDXGIFactory1(__uuidof(IDXGIFactory1), (void**)p_factory.GetAddressOf()))) {
        MessageBoxA(nullptr, "Failed to create 'IDXGIFactory1' instance.", "Error", MB_OK | MB_ICONERROR);
        return false;
    }

    ComPtr<IDXGIAdapter1> p_adapter = nullptr;
    for (int i = 0; ; ++i) {
        if (p_factory->EnumAdapters1(i, p_adapter.GetAddressOf()) == DXGI_ERROR_NOT_FOUND)
            break;
        for (int j = 0; ; ++j) {
            if (p_adapter->EnumOutputs(j, g_pOutput.GetAddressOf()) == DXGI_ERROR_NOT_FOUND)
                break;
            if (g_pOutput != nullptr)
                break;
        }
        if (g_pOutput == nullptr)
            continue;
        break;
    }

    if (g_pOutput == nullptr) {
        MessageBoxA(nullptr, "No monitor found.", "Error", MB_OK | MB_ICONERROR);
        return false;
    }

    return true;
}

DLLEXPORT void __stdcall WaitVsync() {
    g_pOutput->WaitForVBlank();
}
