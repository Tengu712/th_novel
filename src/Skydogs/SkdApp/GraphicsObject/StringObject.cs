using System.Drawing;
using Skydogs.SkdApp.Manager;
using Skydogs.SkdApp.Resource;

namespace Skydogs.SkdApp.GraphicsObject;

enum StringObjectAlignment
{
    Left = 0, Center = 1, Right = 2,
}

interface IStringObject : IGraphicsObject
{
    string String { get; set; }
    float MaxX { get; set; }
    float MaxY { get; set; }
    float Size { get; set; }
    StringObjectAlignment Alignment { get; set; }
}

class StringObject : IStringObject
{
    public float PosX { get; set; } = 0.0f;
    public float PosY { get; set; } = 0.0f;

    private string _string = "";
    public string String
    {
        get { return _string; }
        set { _string = value == null ? "" : value; }
    }
    public float MaxX { get; set; } = GeneralInformation.WIDTH;
    public float MaxY { get; set; } = GeneralInformation.HEIGHT;
    public float Size { get; set; } = 32.0f;
    public StringObjectAlignment Alignment { get; set; } = StringObjectAlignment.Left;

    public StringObject() { }

    void IGraphicsObject.Draw() => DirectX.DrawString(String, PosX, PosY, MaxX, MaxY, Size, (int)Alignment);
}