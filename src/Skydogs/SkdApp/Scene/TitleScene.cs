using Skydogs.SkdApp.GraphicsObject;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.Scene;

class TitleScene : IScene
{
    private readonly IRefManagers _managers;

    public TitleScene(IRefManagers managers)
    {
        _managers = managers;
    }

    void IScene.Update()
    {
    }
}
