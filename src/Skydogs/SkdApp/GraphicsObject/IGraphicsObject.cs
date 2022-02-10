using System.Drawing;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.GraphicsObject;

interface IGraphicsObject
{
    float PosX { get; set; }
    float PosY { get; set; }
    bool IsScreen { get; set; }

    void Draw();
}
