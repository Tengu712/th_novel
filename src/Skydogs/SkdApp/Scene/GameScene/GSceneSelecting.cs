using Skydogs.SkdApp.GameObject;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.Scene.GameScene;

class GSceneSelecting : IGameScene
{
    private readonly IRefManagers _managers;
    private readonly IRefGameInformation _ginf;

    public GSceneSelecting(IRefManagers managers, IRefGameInformation ginf)
    {
        _managers = managers;
        _ginf = ginf;
    }

    void IGameScene.Update()
    {
        _ginf.Selection = SelectionID.Talk;
        _ginf.Scene = GameSceneID.Check;
        System.Console.WriteLine("Select talk!");
    }
}