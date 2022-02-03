using System;
using System.Drawing;

namespace Skydogs.SkdApp.Manager;

class GraphicsManager : ICtrGraphicsManager, IRefGraphicsManager
{

    public GraphicsManager()
    {
    }

    public void Draw(Graphics g)
    {
        Console.WriteLine("Draw all");
    }

    public void AddImage()
    {
        Console.WriteLine("Add image");
    }
}
