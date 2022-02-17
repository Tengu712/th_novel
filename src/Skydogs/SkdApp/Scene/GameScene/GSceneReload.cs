using Skydogs.SkdApp.GameObject;
using Skydogs.SkdApp.GameObject.Script;
using Skydogs.SkdApp.Manager;
using Skydogs.SkdApp.Resource;

namespace Skydogs.SkdApp.Scene.GameScene;

class GSceneReload : IGameScene
{
    private readonly IRefManagers _managers;
    private readonly ICtrGameInformation _ginf;

    public GSceneReload(IRefManagers managers, ICtrGameInformation ginf)
    {
        _managers = managers;
        _ginf = ginf;
    }

    void IGameScene.Update()
    {
        _ginf.Scenario = new Scenario(ResourceX.GetKeyScenario(_ginf.SPlace, _ginf.Day));
        var rqImage = new LoadImageRequest();
        _ginf.Scenario.GetLoadRequest(rqImage);
        ImageLoader.LoadTemp(rqImage);
        _ginf.Scene = GameSceneID.Check;
        System.GC.Collect();
    }
}