using System;
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

    private ImageObject reimu = new ImageObject("reimu", 0.0f, 0.0f, 1280.0f, 1280.0f);
    private StringObject marisa = new StringObject("魔理沙！", 0.0f, 0.0f);

    public void Update()
    {
        _managers.GraphicsManager.AddGraphicsObject(reimu);
        _managers.GraphicsManager.AddGraphicsObject(marisa);
    }
}
