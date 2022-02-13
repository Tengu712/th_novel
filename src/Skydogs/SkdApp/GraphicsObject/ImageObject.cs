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
    bool IsScreen { get; set; }
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
    public bool IsCenter { get; set; } = true;
    public bool IsScreen { get; set; } = true;

    public ImageObject() { }

    void IGraphicsObject.Draw()
    {
        float x = PosX;
        float y = PosY;
        if (IsScreen)
        {
            x -= (float)GeneralInformation.WIDTH / 2.0f;
            y -= (float)GeneralInformation.HEIGHT / 2.0f;
            y *= -1.0f;
        }
        if (!IsCenter)
        {
            x += Width / 2.0f;
            y -= Height / 2.0f;
        }
        DirectX.DrawImageWithKey(ImageName, x, y, Width, Height, Deg, Red, Green, Blue, Alpha);
    }
}
