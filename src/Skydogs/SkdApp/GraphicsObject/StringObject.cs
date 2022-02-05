using System.Drawing;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.GraphicsObject;

interface IStringObject : IGraphicsObject
{
    string String { get; set; }
    string FontName { get; set; }
    string BrushName { get; set; }
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
    private string _fontName = "Logue";
    public string FontName
    {
        get { return _fontName; }
        set { _fontName = value == null ? "Logue" : value; }
    }
    private string _brushName = "White";
    public string BrushName
    {
        get { return _brushName; }
        set { _brushName = value == null ? "White" : value; }
    }

    public StringObject(string str)
    {
        String = str;
    }

    public StringObject(string str, float posx, float posy) : this(str)
    {
        PosX = posx;
        PosY = posy;
    }

    public StringObject(string str, string fontName, string brushName) : this(str)
    {
        FontName = fontName;
        BrushName = brushName;
    }

    public StringObject(string str, string fontName, string brushName, float posx, float posy) : this(str, posx, posy)
    {
        FontName = fontName;
        BrushName = brushName;
    }

    void IGraphicsObject.Draw()
    {
    }
}