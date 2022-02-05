using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Skydogs.SkdApp.GraphicsObject;

namespace Skydogs.SkdApp.Manager;

interface IGraphicsManager
{
    Dictionary<string, Image> Images { get; }
    Dictionary<string, Font> Fonts { get; }
    Dictionary<string, Brush> Brushes { get; }
}

interface ICtrGraphicsManager : IGraphicsManager
{
    void Draw();
}

interface IRefGraphicsManager : IGraphicsManager
{
    void Load(string key);
    void AddGraphicsObject(IGraphicsObject obj);
}

class GraphicsManager : ICtrGraphicsManager, IRefGraphicsManager
{
    private const int MAX_GRAPHICS_OBJECTS = 500;
    private IGraphicsObject[] _objs = new IGraphicsObject[MAX_GRAPHICS_OBJECTS];

    private readonly PrivateFontCollection s_pfc = new PrivateFontCollection();
    public Dictionary<string, Image> Images { get; } = new Dictionary<string, Image>();
    public Dictionary<string, Font> Fonts { get; } = new Dictionary<string, Font>();
    public Dictionary<string, Brush> Brushes { get; } = new Dictionary<string, Brush>();

    public GraphicsManager()
    {
        s_pfc.AddFontFile("SourceHanSerif-Heavy.otf");
        Fonts.Add("Logue", new Font(s_pfc.Families[0], 20));
        Brushes.Add("White", new SolidBrush(Color.White));
    }

    void ICtrGraphicsManager.Draw()
    {
        for (int i = 0; i < MAX_GRAPHICS_OBJECTS; ++i)
        {
            if (_objs[i] == null)
                return;
            _objs[i].Draw();
            _objs[i] = null;
        }
    }

    void IRefGraphicsManager.Load(string key)
    {
        var bmp = new Bitmap("reimu.png");
        Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
        BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
        DirectX.LoadImageWithKey(key, (int)bmpData.Scan0, (uint)bmpData.Width, (uint)bmpData.Height);
        bmp.UnlockBits(bmpData);
    }

    void IRefGraphicsManager.AddGraphicsObject(IGraphicsObject obj)
    {
        for (int i = 0; i < MAX_GRAPHICS_OBJECTS; ++i)
        {
            if (_objs[i] != null)
                continue;
            _objs[i] = obj;
            return;
        }
    }
}
