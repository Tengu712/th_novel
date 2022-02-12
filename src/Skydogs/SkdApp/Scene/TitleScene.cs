using Skydogs.SkdApp.GraphicsObject;
using Skydogs.SkdApp.Manager;
using Skydogs.SkdApp.Resource;

namespace Skydogs.SkdApp.Scene;

class TitleScene : IScene
{
    private readonly IRefManagers _managers;

    public TitleScene(IRefManagers managers)
    {
        _managers = managers;
        //var request = new LoadRequest();
        //ImageLoader.LoadTemp(request);
    }

    void IScene.Update()
    {
        _managers.GraphicsManager.AddGraphicsObject(new StringObject { String = "Start" });
    }
}
