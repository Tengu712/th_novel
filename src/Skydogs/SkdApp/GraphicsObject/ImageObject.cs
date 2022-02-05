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

    public ImageObject(string imageName, float posx, float posy, float width, float height,
        float r, float g, float b, float a, bool iscenter) : this(imageName, posx, posy, width, height, iscenter)
    {
        Red = r;
        Green = g;
        Blue = b;
        Alpha = a;
    }

    void IGraphicsObject.Draw()
    {
        float pos_x = PosX;
        float pos_y = PosY;
        if (IsCenter)
        {
            pos_x -= Width / 2.0f;
            pos_y -= Height / 2.0f;
        }
        DirectX.DrawImageWithKey(ImageName, pos_x, pos_y, Width, Height, Deg, Red, Green, Blue, Alpha);
    }
}
