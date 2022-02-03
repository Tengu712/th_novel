using System;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Skydogs.SkdApp;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        if (!CreateVSyncTimer())
            return;
        MainForm mainform = new MainForm();
        mainform.Show();
        while (mainform.Created)
        {
            Application.DoEvents();
            mainform.UpdateForm();
            WaitVsync();
        }
    }

    [DllImport("vsynctimer.dll", EntryPoint = "CreateVSyncTimer")]
    private static extern bool CreateVSyncTimer();

    [DllImport("vsynctimer.dll", EntryPoint = "WaitVsync")]
    private static extern void WaitVsync();
}
