using System.Drawing;
using Skydogs.SkdApp.Manager;
using Skydogs.SkdApp.Resource;

namespace Skydogs.SkdApp.GraphicsObject;

interface IStringObject : IGraphicsObject
{
    string String { get; set; }
    float Size { get; set; }
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
    public float Size { get; set; } = 32.0f;

    public StringObject() { }

    void IGraphicsObject.Draw()
    {
        DirectX.DrawString(String, PosX, PosY, Size);
    }
}