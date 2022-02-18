using System.Collections.Generic;
using Skydogs.SkdApp.GameObject.Script;
using Skydogs.SkdApp.Manager;
using Skydogs.SkdApp.Scene.GameScene;

namespace Skydogs.SkdApp.GameObject;

enum SelectionID
{
    None, Talk,
}

interface IRefGameInformation
{
    GameSceneID Scene { get; set; }
    SelectionID Selection { get; set; }
    bool ClickedLeft { get; set; }

    int AllTime { get; set; }
    public int Time { get; }
    public int Day { get; }
    string SPlace { get; set; }
    Dictionary<string, int> Flag { get; }

    BackGroundObject BackGround { get; }
    CharactorObject LeftCharactor { get; }
    CharactorObject CenterCharactor { get; }
    CharactorObject RightCharactor { get; }
    CGObject CG { get; }
    LogueBoxObject LogueBox { get; }
}

interface ICtrGameInformation : IRefGameInformation
{
    IGameScene GameScene { get; }
    Scenario Scenario { get; set; }
    void ChangeGameScene();
    void DrawBaseGraphics();
}

class GameInformation : ICtrGameInformation
{
    private readonly IRefManagers _managers;
    public IGameScene GameScene { get; private set; }
    public Scenario Scenario { get; set; }

    public GameSceneID Scene { get; set; } = GameSceneID.Reload;
    public SelectionID Selection { get; set; } = SelectionID.None;
    public bool ClickedLeft { get; set; } = false;

    public int AllTime { get; set; } = 480;
    public int Time
    {
        get { return AllTime % 1440; }
    }
    public int Day
    {
        get { return AllTime / 1440; }
    }
    public string SPlace { get; set; } = "marisahome";
    public Dictionary<string, int> Flag { get; } = new Dictionary<string, int>();

    public BackGroundObject BackGround { get; private set; }
    public CharactorObject LeftCharactor { get; private set; }
    public CharactorObject CenterCharactor { get; private set; }
    public CharactorObject RightCharactor { get; private set; }
    public CGObject CG { get; private set; }
    public LogueBoxObject LogueBox { get; private set; }

    public GameInformation(IRefManagers managers)
    {
        _managers = managers;
        GameScene = new GSceneReload(managers, this);
        BackGround = new BackGroundObject(managers.GraphicsManager, this);
        CG = new CGObject(managers.GraphicsManager);
        LogueBox = new LogueBoxObject(managers.GraphicsManager);
    }

    void ICtrGameInformation.DrawBaseGraphics()
    {
        BackGround.Draw();
        CG.Draw();
        LogueBox.Draw();
    }

    void ICtrGameInformation.ChangeGameScene()
    {
        switch (Scene)
        {
            case GameSceneID.Reload:
                GameScene = new GSceneReload(_managers, this);
                break;
            case GameSceneID.Check:
                GameScene = new GSceneCheck(_managers, this);
                break;
            case GameSceneID.Selection:
                GameScene = new GSceneSelecting(_managers, this);
                break;
            case GameSceneID.Talking:
                GameScene = new GSceneTalking(_managers, this);
                break;
        }
    }
}
