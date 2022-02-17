using Skydogs.SkdApp.GraphicsObject;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.GameObject;

class CGObject
{
    private readonly IRefGraphicsManager _graphicsManager;

    public ImageObject Image { get; private set; }

    public CGObject(IRefGraphicsManager graphicsManager)
    {
        _graphicsManager = graphicsManager;
        Image = new ImageObject
        {
            SqSize = 1280.0f,
            Red = 0.0f,
            Green = 0.0f,
            Blue = 0.0f,
            Alpha = 1.0f,
            IsCenter = false,
        };
    }

    public void Draw() => _graphicsManager.AddGraphicsObject(Image);
}
