using Skydogs.SkdApp.GraphicsObject;
using Skydogs.SkdApp.Manager;
using Skydogs.SkdApp.Resource;
using Skydogs.SkdApp.GameObject;
using Skydogs.SkdApp.GameObject.Script;

namespace Skydogs.SkdApp.Scene;

class TitleScene : IScene
{
    private readonly IRefManagers _managers;
    private GameInformation _ginf;
    private Scenario _scenario;

    public TitleScene(IRefManagers managers)
    {
        _managers = managers;
        _ginf = new GameInformation(_managers.EventManager);
        Reload();
    }

    private void Reload()
    {
        _scenario = new Scenario(_managers, "snr.1.hakureishrine");
        System.GC.Collect();
    }

    void IScene.Update()
    {
        switch(_ginf.Scene)
        {
            case GameSceneID.Reload:
                Reload();
                break;
            case GameSceneID.Neutral:
                _scenario.Check(_ginf);
                break;
            case GameSceneID.Selection:
                System.Console.WriteLine("Selecting...");
                break;
            case GameSceneID.Talking:
                _scenario.Update(_ginf);
                break;
            default:
                break;
        }
    }
}
