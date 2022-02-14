using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Skydogs.SkdApp;

class DirectX
{
    private DirectX() { }

    [DllImport("directx.dll", EntryPoint = "InitializeDirectX")]
    private static extern bool s_initializeDirectX(int h_wnd, uint width, uint height);
    public static bool InitializeDirectX(int h_wnd, uint width, uint height)
    {
        try
        {
            return s_initializeDirectX(h_wnd, width, height);
        }
        catch (DllNotFoundException e)
        {
            MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }

    [DllImport("directx.dll", EntryPoint = "ClearSetBackBuffer")]
    private static extern void s_clearSetBackBuffer(float r, float g, float b);
    public static void ClearSetBackBuffer(float r, float g, float b)
    {
        s_clearSetBackBuffer(r, g, b);
    }

    [DllImport("directx.dll", EntryPoint = "Present")]
    private static extern void s_present();
    public static void Present()
    {
        s_present();
    }

    [DllImport("directx.dll", EntryPoint = "LoadImageWithKey")]
    private static extern bool s_loadImageWithKey(string key, int data, uint width, uint height);
    public static bool LoadImageWithKey(string key, int data, uint width, uint height)
    {
        return s_loadImageWithKey(key, data, width, height);
    }

    [DllImport("directx.dll", EntryPoint = "UnloadImageWithKey")]
    private static extern void s_unloadImageWithKey(string key);
    public static void UnloadImageWithKey(string key)
    {
        s_unloadImageWithKey(key);
    }

    [DllImport("directx.dll", EntryPoint = "DrawImageWithKey")]
    private static extern void s_drawImageWithKey(string key, float pos_x, float pos_y,
        float scl_x, float scl_y, float deg, float r, float g, float b, float a);
    public static void DrawImageWithKey(string key, float pos_x, float pos_y,
        float scl_x, float scl_y, float deg, float r, float g, float b, float a)
    {
        s_drawImageWithKey(key, pos_x, pos_y, scl_x, scl_y, deg, r, g, b, a);
    }

    [DllImport("directx.dll", EntryPoint = "DrawString", CharSet = CharSet.Unicode)]
    private static extern void
        s_drawString(string str, float pos_x, float pos_y, float max_x, float max_y, float size, int alignment);
    public static void
        DrawString(string str, float pos_x, float pos_y, float max_x, float max_y, float size, int alignment)
    {
        s_drawString(str, pos_x, pos_y, max_x, max_y, size, alignment);
    }
}
