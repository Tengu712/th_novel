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
    public static bool LoadImage(string key)
    {
        using (var stream = (Program.GetAssembly()).GetManifestResourceStream("resource.resx"))
        using (var rset = new ResXResourceSet(stream))
        {
            var bmp = (Bitmap)rset.GetObject(key);
            if (bmp == null)
                return false;
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            if (!DirectX.LoadImageWithKey(key, (int)bmpData.Scan0, (uint)bmpData.Width, (uint)bmpData.Height))
                return false;
            bmp.UnlockBits(bmpData);
        }
        return true;
    }

    private ResourceX() { }
}
