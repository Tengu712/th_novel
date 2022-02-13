using Skydogs.SkdApp.GraphicsObject;
using Skydogs.SkdApp.Manager;
using Skydogs.SkdApp.Resource;
using Skydogs.SkdApp.GameObject;

namespace Skydogs.SkdApp.Scene;

class TitleScene : IScene
{
    private readonly IRefManagers _managers;

    public TitleScene(IRefManagers managers)
    {
        _managers = managers;
        //var request = new LoadRequest();
        //ImageLoader.LoadTemp(request);
        var s = new Scenario("snr.1.hakureishrine");
    }

    void IScene.Update()
    {
        _managers.GraphicsManager.AddGraphicsObject(
                new ImageObject { ImageName = "img.load", SqSize = 1280.0f, IsCenter = false });
        _managers.GraphicsManager.AddGraphicsObject(
            new StringObject { String = "仏にはさくらの花をたてまつれ\nわがのちの世を人とぶらはば", PosX = 360.0f, PosY = 360.0f });
    }
}
