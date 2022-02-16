using Skydogs.SkdApp.GraphicsObject;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.GameObject;

class CGObject
{
    private readonly IRefManagers _managers;
    private readonly GameInformation _ginf;

    public ImageObject Image { get; private set; }

    public CGObject(IRefManagers managers, GameInformation ginf)
    {
        _managers = managers;
        _ginf = ginf;
        Image = new ImageObject { SqSize = 1280.0f, Alpha = 0.0f, IsCenter = false };
    }

    public void Draw() => _managers.GraphicsManager.AddGraphicsObject(Image);
}
