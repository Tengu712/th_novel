using System;
using System.Drawing;
using Skydogs.SkdApp.GraphicsObject;

namespace Skydogs.SkdApp.Manager;

class GraphicsManager : ICtrGraphicsManager, IRefGraphicsManager
{
    private IGraphicsObject _obj;

    public GraphicsManager()
    {
        this._obj = new SkdImage("reimu.png");
    }

    public void Draw(Graphics g)
    {
        _obj.Draw(g);
    }

    public void AddImage()
    {
        Console.WriteLine("Add image");
    }
}
