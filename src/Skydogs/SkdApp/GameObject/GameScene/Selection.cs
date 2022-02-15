using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.GameObject.GameScene;

class Selection : IGameScene
{
    private readonly IRefManagers _managers;

    public Selection(IRefManagers managers)
    {
        _managers = managers;
    }

    public void Update(GameInformation ginf)
    {
        ginf.Selection = SelectionID.Talk;
        ginf.Scene = GameSceneID.Neutral;
        System.Console.WriteLine("Select talk!");
    }
}