using System.Drawing;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.GraphicsObject;

interface IStringObject : IGraphicsObject
{
    string FontName { get; set; }
    string BrushName { get; set; }
}

class StringObject : IStringObject
{
    private readonly string _str = null;

    public float PosX { get; set; } = 0.0f;
    public float PosY { get; set; } = 0.0f;
    public string FontName { get; set; } = null;
    public string BrushName { get; set; } = null;

    public StringObject(string str)
    {
        this._str = str;
    }

    public void Draw(IRefGraphicsManager graphicsManager, Graphics g)
    {
        if (_str == null)
            return;
    }
}