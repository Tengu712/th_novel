using System;
using System.Reflection;
using System.Windows.Forms;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        if (!System.IO.File.Exists("directx.dll"))
        {
            MessageBox.Show("'directx.dll' not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        var managers = new Managers();
        var mainform = new MainForm(managers);
        if (!DirectX.InitializeDirectX((int)mainform.Handle, GeneralInformation.WIDTH, GeneralInformation.HEIGHT))
        {
            MessageBox.Show("Failed to initialize DirectX.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        mainform.Show();
        while (mainform.Created)
        {
            Application.DoEvents();
            DirectX.ClearSetBackBuffer(0.0f, 0.0f, 0.0f);
            managers.Update();
            DirectX.Present();
        }
    }

    public static Assembly GetAssembly() => typeof(Program).Assembly;

    public static void Panic(string message)
    {
        MessageBox.Show(message, "Runtime Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        throw new System.Exception(message);
    }
}
