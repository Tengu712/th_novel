using System.Drawing;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.GraphicsObject;

interface IImageObject : IGraphicsObject
{
    string ImageName { get; set; }
    float Width { get; set; }
    float Height { get; set; }
    float Deg { get; set; }
    float Red { get; set; }
    float Green { get; set; }
    float Blue { get; set; }
    float Alpha { get; set; }
    bool IsCenter { get; set; }
}

class ImageObject : IImageObject
{
    public float PosX { get; set; } = 0.0f;
    public float PosY { get; set; } = 0.0f;

    private string _imageName = "";
    public string ImageName
    {
        get { return _imageName; }
        set { _imageName = value == null ? "" : value; }
    }
    public float Width { get; set; } = 0.0f;
    public float Height { get; set; } = 0.0f;
    public float Deg { get; set; } = 0.0f;
    public float Red { get; set; } = 1.0f;
    public float Green { get; set; } = 1.0f;
    public float Blue { get; set; } = 1.0f;
    public float Alpha { get; set; } = 1.0f;
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

    void IGraphicsObject.Draw()
    {
        if (IsCenter)
            DirectX.DrawImageWithKey(ImageName, PosX + Width / 2.0f, PosY + Height / 2.0f, Width, Height,
                Deg, Red, Green, Blue, Alpha);
        else
            DirectX.DrawImageWithKey(ImageName, PosX, PosY, Width, Height, Deg, Red, Green, Blue, Alpha);
    }
}
