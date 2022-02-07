using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.GraphicsObject;

interface IImageObject : IGraphicsObject
{
    string ImageName { get; set; }
    float Width { get; set; }
    float Height { get; set; }
    float SqSize { set; }
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
    public float SqSize
    {
        set { Width = value; Height = value; }
    }
    public float Deg { get; set; } = 0.0f;
    public float Red { get; set; } = 1.0f;
    public float Green { get; set; } = 1.0f;
    public float Blue { get; set; } = 1.0f;
    public float Alpha { get; set; } = 1.0f;
    public bool IsCenter { get; set; } = false;

    public ImageObject() { }

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
