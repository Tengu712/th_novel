using Skydogs.SkdApp.Manager;
using Skydogs.SkdApp.GameObject;

namespace Skydogs.SkdApp.Scene;

class GamePlayScene : IScene
{
    private readonly IRefManagers _managers;
    private readonly ICtrGameInformation _ginf;

    public GamePlayScene(IRefManagers managers)
    {
        _managers = managers;
        _ginf = new GameInformation(_managers);
    }

    void IScene.Update()
    {
        _ginf.ChangeGameScene();
        _ginf.GameScene.Update();
        _ginf.DrawBaseGraphics();
    }
}
