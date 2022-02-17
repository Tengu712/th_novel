using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Resources;
using System.Runtime.InteropServices;

namespace Skydogs.SkdApp.Resource;

class ResourceX
{
    public static string GetKeyScenario(string splace, int day) => $"snr.{day}.{splace}";

    public static string[] GetKeysBackGround(string place)
    {
        var res = new string[3];
        res[0] = $"img.{place}.day";
        res[1] = $"img.{place}.evening";
        res[2] = $"img.{place}.night";
        return res;
    }

    public static string GetKeyBackGround(string place, int time)
    {
        if (420 <= time && time < 960)
            return $"img.{place}.day";
        else if (960 <= time && time < 1080)
            return $"img.{place}.evening";
        else
            return $"img.{place}.night";
    }

    public static void LoadImage(string key)
    {
        using (var stream = (Program.GetAssembly()).GetManifestResourceStream("resource.resx"))
        using (var rset = new ResXResourceSet(stream))
        {
            var bmp = (Bitmap)rset.GetObject(key);
            if (bmp == null)
                Program.Panic($"'{key}' image not found.");
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            if (!DirectX.LoadImageWithKey(key, (int)bmpData.Scan0, (uint)bmpData.Width, (uint)bmpData.Height))
                Program.Panic($"Failed to load '{key}' image.");
            bmp.UnlockBits(bmpData);
        }
    }

    public static string[] GetScenario(string key)
    {
        using (var stream = (Program.GetAssembly()).GetManifestResourceStream("resource.resx"))
        using (var rset = new ResXResourceSet(stream))
        {
            var res = (string[])rset.GetObject(key);
            if (res == null)
                Program.Panic($"'{key}' scenario not found.");
            return res;
        }
    }

    private ResourceX() { }
}
