using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Skydogs.SkdApp;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        MainForm mainform = new MainForm();
        try
        {
            if (!InitializeDirectX((int)mainform.Handle, 1280, 720))
            {
                MessageBox.Show("Failed to initialize DirectX.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        catch (DllNotFoundException e)
        {
            MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        mainform.Show();
        while (mainform.Created)
        {
            Application.DoEvents();
            ClearSetBackBuffer(0.0f, 0.0f, 0.0f);
            mainform.UpdateForm();
            Present();
        }
    }

    [DllImport("directx.dll", EntryPoint = "InitializeDirectX")]
    private static extern bool InitializeDirectX(int h_wnd, uint width, uint height);

    [DllImport("directx.dll", EntryPoint = "ClearSetBackBuffer")]
    private static extern void ClearSetBackBuffer(float r, float g, float b);

    [DllImport("directx.dll", EntryPoint = "Present")]
    private static extern void Present();
}
