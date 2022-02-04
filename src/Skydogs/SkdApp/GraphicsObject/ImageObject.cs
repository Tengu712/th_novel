using System.Drawing;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.GraphicsObject;

interface IImageObject : IGraphicsObject
{
    string ImageName { get; set; }
    float Width { get; set; }
    float Height { get; set; }
    bool IsCenter { get; set; }
}

class ImageObject : IImageObject
{
    public string ImageName { get; set; } = null;
    public float PosX { get; set; } = 0.0f;
    public float PosY { get; set; } = 0.0f;
    public float Width { get; set; } = 0.0f;
    public float Height { get; set; } = 0.0f;
    public bool IsCenter { get; set; } = false;

    public ImageObject(string imageName)
    {
        ImageName = imageName;
    }

    public ImageObject(string imageName, float posx, float posy) : this(imageName)
    {
        PosX = posx;
        PosY = posy;
    }

    public ImageObject(string imageName, float posx, float posy, float width, float height)
        : this(imageName, posx, posy)
    {
        Width = width;
        Height = height;
    }

    public ImageObject(string imageName, float posx, float posy, float width, float height, bool iscenter)
        : this(imageName, posx, posy, width, height)
    {
        IsCenter = iscenter;
    }

    public void Draw(IRefGraphicsManager graphicsManager, Graphics g)
    {
        if (ImageName == null)
            return;
        if (!graphicsManager.Images.ContainsKey(ImageName))
            return;
        if (IsCenter)
            g.DrawImage(graphicsManager.Images[ImageName], PosX + Width / 2.0f, PosY + Height / 2.0f, Width, Height);
        else
            g.DrawImage(graphicsManager.Images[ImageName], PosX, PosY, Width, Height);
    }
}
