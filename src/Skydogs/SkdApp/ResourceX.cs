using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Resources;
using System.Runtime.InteropServices;

namespace Skydogs.SkdApp;

class ResourceX
{
    private const int FONTSIZE = 50;
    private const int CANVAS_HEIGHT = 64;
    private const int CANVAS_WIDTH = CANVAS_HEIGHT + 40;
    private static readonly PrivateFontCollection s_pfc = new PrivateFontCollection();
    private static readonly Dictionary<string, Font> s_fonts = new Dictionary<string, Font>();
    private static readonly Dictionary<string, float> s_aspects = new Dictionary<string, float>();

    private ResourceX() { }

    public static bool LoadFonts(string key)
    {
        var stream = (Program.GetAssembly()).GetManifestResourceStream("resource.resx"); ;
        var rset = new ResXResourceSet(stream);
        var data = (byte[])rset.GetObject(key);
        IntPtr ptr = Marshal.AllocCoTaskMem(data.Length);
        Marshal.Copy(data, 0, ptr, data.Length);
        s_pfc.AddMemoryFont(ptr, data.Length);
        Marshal.FreeCoTaskMem(ptr);
        s_fonts.Add(key, new Font(s_pfc.Families[s_pfc.Families.Length - 1], FONTSIZE, GraphicsUnit.Pixel));
        return true;
    }

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
        var bmp = new Bitmap(CANVAS_WIDTH, CANVAS_HEIGHT);
        var sf = new StringFormat();
        sf.Alignment = StringAlignment.Center;
        sf.LineAlignment = StringAlignment.Center;
        (Graphics.FromImage(bmp)).DrawString(charactor.ToString(), s_fonts[key], Brushes.White, FONTSIZE / 2, FONTSIZE / 2, sf);
        BitmapData bmpData = bmp.LockBits(
            new Rectangle(0, 0, CANVAS_WIDTH, CANVAS_HEIGHT), ImageLockMode.ReadWrite, bmp.PixelFormat);

        byte[] bits = new byte[CANVAS_WIDTH * CANVAS_HEIGHT * 4];
        Marshal.Copy((IntPtr)bmpData.Scan0, bits, 0, bits.Length);

        int leftest = 1000;
        int rightest = 0;
        for (int i = 1; i < CANVAS_HEIGHT - 1; ++i)
        {
            for (int j = 1; j < CANVAS_WIDTH - 1; ++j)
            {
                if (bits[CANVAS_WIDTH * 4 * i + j * 4 + 3] == 255)
                    continue;
                for (int k = 0; k < 9; ++k)
                {
                    int raw = CANVAS_WIDTH * 4 * (i - 1 + k / 3);
                    int col = (j - 1 + (k % 3)) * 4 + 3;
                    if (bits[raw + col] != 255)
                        continue;
                    bits[CANVAS_WIDTH * 4 * i + j * 4 + 3] = 128;
                    leftest = Math.Min(leftest, j);
                    rightest = Math.Max(rightest, j);
                    break;
                }
            }
        }

        leftest -= 2;
        rightest += 2;
        byte[] res = new byte[CANVAS_HEIGHT * CANVAS_HEIGHT * 4];
        for (int i = 0; i < CANVAS_HEIGHT; ++i)
        {
            int k = 0;
            for (int j = 0; j < CANVAS_WIDTH; ++j)
            {
                if (k >= CANVAS_HEIGHT)
                    break;
                if (j <= leftest)
                    continue;
                if (j >= rightest)
                    break;
                res[CANVAS_HEIGHT * 4 * i + k * 4 + 0] = 255;
                res[CANVAS_HEIGHT * 4 * i + k * 4 + 1] = 255;
                res[CANVAS_HEIGHT * 4 * i + k * 4 + 2] = 255;
                res[CANVAS_HEIGHT * 4 * i + k * 4 + 3] = bits[CANVAS_WIDTH * 4 * i + j * 4 + 3];
                ++k;
            }
        }

        IntPtr ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(byte)) * res.Length);
        Marshal.Copy(res, 0, ptr, res.Length);
        string chrkey = "chr." + key + "." + charactor.ToString();
        if (!DirectX.LoadImageWithKey(chrkey, (int)ptr, (uint)CANVAS_HEIGHT, (uint)CANVAS_HEIGHT))
            return false;
        bmp.UnlockBits(bmpData);
        s_aspects.Add(chrkey, (float)(rightest - leftest) / (float)CANVAS_HEIGHT);
        return true;
    }

    public static float GetAspect(string key)
    {
        if (!s_aspects.ContainsKey(key))
            return 1.0f;
        return s_aspects[key];
    }
}
