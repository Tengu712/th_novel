using System;
using System.Windows.Forms;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        MainForm mainform = new MainForm();
        if (!DirectX.InitializeDirectX((int)mainform.Handle, 1280, 720))
        {
            MessageBox.Show("Failed to initialize DirectX.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        mainform.Show();
        while (mainform.Created)
        {
            Application.DoEvents();
            DirectX.ClearSetBackBuffer(0.0f, 0.0f, 0.0f);
            mainform.UpdateForm();
            DirectX.Present();
        }
    }
}
