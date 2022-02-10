using System.Drawing;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.GraphicsObject;

interface IStringObject : IGraphicsObject
{
    string String { get; set; }
    string FontName { get; set; }
    int Height { get; set; }
}

class StringObject : IStringObject
{
    public float PosX { get; set; } = 0.0f;
    public float PosY { get; set; } = 0.0f;
    public bool IsScreen { get; set; } = true;

    private string _string = "";
    public string String
    {
        get { return _string; }
        set { _string = value == null ? "" : value; }
    }
    private string _fontName = "fnt.normal";
    public string FontName
    {
        get { return _fontName; }
        set { _fontName = value == null ? "fnt.normal" : value; }
    }
    public int Height { get; set; } = 64;

    public StringObject() { }

    void IGraphicsObject.Draw()
    {
        float x_start = PosX;
        float y_start = PosY;
        if (IsScreen)
        {
            x_start -= (float)GeneralInformation.WIDTH / 2.0f;
            y_start -= (float)GeneralInformation.HEIGHT / 2.0f;
            y_start *= -1.0f;
        }
        x_start += Height / 2.0f;
        y_start -= Height / 2.0f;

        float x = x_start;
        float y = y_start;
        for (int i = 0; i < String.Length; ++i)
        {
            if (String[i] == '$' && i + 1 < String.Length)
            {
                switch (String[i + 1])
                {
                    case 'n':
                        x = x_start;
                        y -= Height;
                        ++i;
                        continue;
                    default:
                        break;
                }
            }
            string chrkey = "chr." + FontName + "." + String[i].ToString();
            DirectX.DrawImageWithKey(chrkey, x, y, Height, Height, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f);
            x += (float)Height * ResourceX.GetAspect(chrkey);
        }
    }
}