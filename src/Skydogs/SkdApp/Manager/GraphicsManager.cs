using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Resources;
using System.Runtime.InteropServices;
using Skydogs.SkdApp.GraphicsObject;

namespace Skydogs.SkdApp.Manager;

interface ICtrGraphicsManager
{
    void Draw();
}

interface IRefGraphicsManager
{
    void Load(string key);
    void AddGraphicsObject(IGraphicsObject obj);
}

class GraphicsManager : ICtrGraphicsManager, IRefGraphicsManager
{
    private const int MAX_GRAPHICS_OBJECTS = 500;
    private IGraphicsObject[] _objs = new IGraphicsObject[MAX_GRAPHICS_OBJECTS];

    public GraphicsManager() { }

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
        var stream = (Program.GetAssembly()).GetManifestResourceStream("resource.resx");
        var rset = new ResXResourceSet(stream);
        var bmp = (Bitmap)rset.GetObject(key);
        if (bmp == null)
            return;
        BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
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
