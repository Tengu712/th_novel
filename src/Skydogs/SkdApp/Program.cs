using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        if (args.Length > 0 && args[0].Equals("debug"))
            AllocConsole();
        MainForm mainform = new MainForm();
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
            mainform.UpdateForm();
            DirectX.Present();
        }
    }

    public static Assembly GetAssembly()
    {
        return typeof(Program).Assembly;
    }

    [DllImport("kernel32.dll", EntryPoint = "AllocConsole")]
    private static extern bool AllocConsole();
}
