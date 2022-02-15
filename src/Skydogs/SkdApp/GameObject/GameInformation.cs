using Skydogs.SkdApp.Manager;
using Skydogs.SkdApp.GameObject.UI;

namespace Skydogs.SkdApp.GameObject;

enum GameSceneID
{
    Reload, Neutral, Selection, Talking,
}

enum SelectionID
{
    None, Talk,
}

class GameInformation
{
    public GameSceneID Scene { get; set; } = GameSceneID.Neutral;
    public SelectionID Selection { get; set; } = SelectionID.None;

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
    public LogueBoxObject LogueBox { get; private set; } = null;
    public BackGroundObject BackGround { get; set; } = null;
    public CharactorObject LeftCharactor { get; set; } = null;
    public CharactorObject CenterCharactor { get; set; } = null;
    public CharactorObject RightCharactor { get; set; } = null;

    public GameInformation(IRefManagers managers)
    {
        LogueBox = new LogueBoxObject(managers);
    }
}
