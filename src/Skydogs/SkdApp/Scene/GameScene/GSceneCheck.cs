using Skydogs.SkdApp.GameObject;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.Scene.GameScene;

class GSceneCheck : IGameScene
{
    private readonly IRefManagers _managers;
    private readonly ICtrGameInformation _ginf;

    public GSceneCheck(IRefManagers managers, ICtrGameInformation ginf)
    {
        _managers = managers;
        _ginf = ginf;
    }

    void IGameScene.Update()
    {
        _ginf.Scenario.Check(_ginf);
    }
}