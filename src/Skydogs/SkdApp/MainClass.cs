using System;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Skydogs.SkdApp;

class MainClass
{
    [STAThread]
    public static void Main(string[] args)
    {
        MainForm mainform = new MainForm();
        mainform.Show();
        if (!CreateVSyncTimer())
            return;
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
