using System.Drawing;

namespace Skydogs.SkdApp.GraphicsObject;

class SkdImage : IGraphicsObject
{
    private readonly Image _image = null;

    public SkdImage(string filename)
    {
        this._image = Image.FromFile(filename);
    }

    public void Draw(Graphics g)
    {
        g.DrawImage(_image, 20, 20, 1280, 1280);
    }
}
