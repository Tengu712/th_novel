using System;
using Skydogs.SkdApp.GraphicsObject;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.Scene;

class TitleScene : IScene
{
    private IRefManagers _managers = null;

    public TitleScene(IRefManagers managers)
    {
        _managers = managers;
    }

    public void Init()
    {
    }

    public void Update()
    {
        _managers.GetRefGraphicsManager().AddGraphicsObject(new ImageObject("reimu", 0.0f, 0.0f, 1280.0f, 1280.0f));
    }
}
