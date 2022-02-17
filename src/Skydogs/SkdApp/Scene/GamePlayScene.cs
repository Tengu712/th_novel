using Skydogs.SkdApp.GraphicsObject;
using Skydogs.SkdApp.Manager;
using Skydogs.SkdApp.Resource;
using Skydogs.SkdApp.GameObject;
using Skydogs.SkdApp.GameObject.GameScene;
using Skydogs.SkdApp.GameObject.Script;

namespace Skydogs.SkdApp.Scene;

class GamePlayScene : IScene
{
    private readonly IRefManagers _managers;
    private GameInformation _ginf;
    private Scenario _scenario;
    private Selection _selection;

    public GamePlayScene(IRefManagers managers)
    {
        _managers = managers;
        _ginf = new GameInformation(_managers);
        _selection = new Selection(_managers, _ginf);
        Reload();
    }

    private void Reload()
    {
        _scenario = new Scenario(_managers, _ginf, $"snr.{_ginf.Day}.{_ginf.Place}");
        var rq = new LoadImageRequest();
        _scenario.GetLoadRequest(rq);
        ImageLoader.LoadTemp(rq);
        System.GC.Collect();
        _ginf.Scene = GameSceneID.Neutral;
    }

    void IScene.Update()
    {
        switch (_ginf.Scene)
        {
            case GameSceneID.Reload:
                Reload();
                break;
            case GameSceneID.Neutral:
                _scenario.Check();
                break;
            case GameSceneID.Selection:
                _selection.Update();
                break;
            case GameSceneID.Talking:
                _scenario.Update();
                break;
            default:
                break;
        }
    }
}
