using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Resources;

namespace Skydogs.SkdApp;

class ResourceX
{
    private ResourceX() {}

    public static void LoadImage(string key)
    {
        var stream = (Program.GetAssembly()).GetManifestResourceStream("resource.resx");
        var rset = new ResXResourceSet(stream);
        var bmp = (Bitmap)rset.GetObject(key);
        if (bmp == null)
            return;
        BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
        DirectX.LoadImageWithKey(key, (int)bmpData.Scan0, (uint)bmpData.Width, (uint)bmpData.Height);
        bmp.UnlockBits(bmpData);
    }
}