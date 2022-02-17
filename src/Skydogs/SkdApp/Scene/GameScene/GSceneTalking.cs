using Skydogs.SkdApp.GameObject;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.Scene.GameScene;

class GSceneTalking : IGameScene
{
    private readonly IRefManagers _managers;
    private readonly ICtrGameInformation _ginf;

    public GSceneTalking(IRefManagers managers, ICtrGameInformation ginf)
    {
        _managers = managers;
        _ginf = ginf;
    }

    void IGameScene.Update()
    {
        _ginf.ClickedLeft = _managers.EventManager.ClickedMouseLeft(
            0, 0, GeneralInformation.WIDTH, GeneralInformation.HEIGHT);
        _ginf.Scenario.Update(_ginf);
    }
}