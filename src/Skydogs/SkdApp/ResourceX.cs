using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Resources;
using System.Runtime.InteropServices;

namespace Skydogs.SkdApp;

class ResourceX
{
    private static readonly int s_fontsize = 64;
    private static readonly int s_cnvsWidth = s_fontsize + 40;
    private static readonly int s_cnvsHeight = s_fontsize;
    private static readonly Font font = new Font("Arial", s_fontsize, GraphicsUnit.Pixel);

    private ResourceX() { }

    public static bool LoadImage(string key)
    {
        var stream = (Program.GetAssembly()).GetManifestResourceStream("resource.resx");
        var rset = new ResXResourceSet(stream);
        var bmp = (Bitmap)rset.GetObject(key);
        if (bmp == null)
            return false;
        BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
        if (!DirectX.LoadImageWithKey(key, (int)bmpData.Scan0, (uint)bmpData.Width, (uint)bmpData.Height))
            return false;
        bmp.UnlockBits(bmpData);
        return true;
    }

    public static bool LoadCharacterImage(string key, char charactor)
    {
        var bmp = new Bitmap(s_cnvsWidth, s_cnvsHeight);
        (Graphics.FromImage(bmp)).DrawString(charactor.ToString(), font, Brushes.White, 0, 0);
        BitmapData bmpData = bmp.LockBits(
            new Rectangle(0, 0, s_cnvsWidth, s_cnvsHeight), ImageLockMode.ReadWrite, bmp.PixelFormat);

        byte[] bits = new byte[s_cnvsWidth * s_cnvsHeight * 4];
        Marshal.Copy((IntPtr)bmpData.Scan0, bits, 0, bits.Length);

        int leftest = 1000;
        int rightest = 0;
        for (int i = 1; i < s_cnvsHeight - 1; ++i)
        {
            for (int j = 1; j < s_cnvsWidth - 1; ++j)
            {
                if (bits[s_cnvsWidth * 4 * i + j * 4 + 3] == 255)
                    continue;
                for (int k = 0; k < 9; ++k)
                {
                    int raw = s_cnvsWidth * 4 * (i - 1 + k / 3);
                    int col = (j - 1 + (k % 3)) * 4 + 3;
                    if (bits[raw + col] != 255)
                        continue;
                    bits[s_cnvsWidth * 4 * i + j * 4 + 3] = 128;
                    leftest = Math.Min(leftest, j);
                    rightest = Math.Max(rightest, j);
                    break;
                }
            }
        }

        leftest -= 4;
        byte[] res = new byte[s_cnvsHeight * s_cnvsHeight * 4];
        for (int i = 0; i < s_cnvsHeight; ++i)
        {
            int k = 0;
            for (int j = 0; j < s_cnvsHeight; ++j)
            {
                if (j < leftest)
                    continue;
                if (j >= rightest)
                    break;
                res[s_cnvsHeight * 4 * i + k * 4 + 0] = 255;
                res[s_cnvsHeight * 4 * i + k * 4 + 1] = 255;
                res[s_cnvsHeight * 4 * i + k * 4 + 2] = 255;
                res[s_cnvsHeight * 4 * i + k * 4 + 3] = bits[s_cnvsWidth * 4 * i + j * 4 + 3];
                ++k;
            }
        }

        IntPtr ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(byte)) * res.Length);
        Marshal.Copy(res, 0, ptr, res.Length);
        if (!DirectX.LoadImageWithKey("chr." + charactor.ToString(), (int)ptr, (uint)s_cnvsHeight, (uint)s_cnvsHeight))
            return false;
        bmp.UnlockBits(bmpData);
        return true;
    }
}
