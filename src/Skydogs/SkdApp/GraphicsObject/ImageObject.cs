using System.Drawing;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.GraphicsObject;

interface IImageObject : IGraphicsObject
{
    float Width { get; set; }
    float Height { get; set; }
    bool IsCenter { get; set; }
}

class ImageObject : IImageObject
{
    private readonly string _imageName = null;

    public float PosX { get; set; } = 0.0f;
    public float PosY { get; set; } = 0.0f;
    public float Width { get; set; } = 0.0f;
    public float Height { get; set; } = 0.0f;
    public bool IsCenter { get; set; } = false;

    public ImageObject(string imageName)
    {
        _imageName = imageName;
    }

    public ImageObject(string imageName, float posx, float posy)
    {
        _imageName = imageName;
        SubConstructor(posx, posy);
    }

    public ImageObject(string imageName, float posx, float posy, float width, float height)
    {
        _imageName = imageName;
        SubConstructor(posx, posy, width, height);
    }

    public ImageObject(string imageName, float posx, float posy, float width, float height, bool iscenter)
    {
        _imageName = imageName;
        SubConstructor(posx, posy, width, height, iscenter);
    }

    private void SubConstructor(float posx, float posy)
    {
        PosX = posx;
        PosY = posy;
    }

    private void SubConstructor(float posx, float posy, float width, float height)
    {
        SubConstructor(posx, posy);
        Width = width;
        Height = height;
    }

    private void SubConstructor(float posx, float posy, float width, float height, bool iscenter)
    {
        SubConstructor(posx, posy, width, height);
        IsCenter = iscenter;
    }

    public void Draw(IRefGraphicsManager graphicsManager, Graphics g)
    {
        if (_imageName == null)
            return;
        if (!graphicsManager.Images.ContainsKey(_imageName))
            return;
        if (IsCenter)
            g.DrawImage(graphicsManager.Images[_imageName], PosX + Width / 2.0f, PosY + Height / 2.0f, Width, Height);
        else
            g.DrawImage(graphicsManager.Images[_imageName], PosX, PosY, Width, Height);
    }
}
