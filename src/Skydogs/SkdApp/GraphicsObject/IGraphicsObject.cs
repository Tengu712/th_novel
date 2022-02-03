using System.Drawing;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.GraphicsObject;

interface IGraphicsObject
{
    float PosX { get; set; }
    float PosY { get; set; }
    
    void Draw(IRefGraphicsManager graphicsManager, Graphics g);
}
