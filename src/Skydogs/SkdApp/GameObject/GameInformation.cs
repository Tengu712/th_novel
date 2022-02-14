

using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.GameObject;

enum GameSceneID
{
    Reload, Neutral, Selection, Talking,
}

class GameInformation
{
    public IRefEventManager EventManager { get; private set; } = null;
    public GameSceneID Scene { get; set; } = GameSceneID.Neutral;
    public string Selection { get; set; } = "";

    public int AllTime { get; set; } = 0;
    public int Time
    {
        get { return AllTime % 1440; }
    }
    public int Day
    {
        get { return AllTime / 1440; }
    }
    public string Place { get; set; } = "";
    public LogueBoxObject LogueBox { get; set; } = new LogueBoxObject();
    public BackGroundObject BackGround { get; set; } = null;
    public CharactorObject LeftCharactor { get; set; } = null;
    public CharactorObject CenterCharactor { get; set; } = null;
    public CharactorObject RightCharactor { get; set; } = null;

    public GameInformation(IRefEventManager eventManager)
    {
        EventManager = eventManager;
    }
}
