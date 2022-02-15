using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.GameObject.GameScene;

class Selection : IGameScene
{
    private readonly IRefManagers _managers;
    private readonly GameInformation _ginf;

    public Selection(IRefManagers managers, GameInformation ginf)
    {
        _managers = managers;
        _ginf = ginf;
    }

    public void Update()
    {
        _ginf.Selection = SelectionID.Talk;
        _ginf.Scene = GameSceneID.Neutral;
        System.Console.WriteLine("Select talk!");
    }
}