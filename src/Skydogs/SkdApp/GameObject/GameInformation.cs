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

    public int AllTime { get; set; } = 480;
    public int Time
    {
        get { return AllTime % 1440; }
    }
    public int Day
    {
        get { return AllTime / 1440; }
    }
    public string Place { get; set; } = "marisahome";
    public BackGroundObject BackGround { get; set; } = null;
    public CharactorObject LeftCharactor { get; set; } = null;
    public CharactorObject CenterCharactor { get; set; } = null;
    public CharactorObject RightCharactor { get; set; } = null;
    public CGObject CG { get; set; } = null;
    public LogueBoxObject LogueBox { get; private set; } = null;

    public GameInformation(IRefManagers managers)
    {
        BackGround = new BackGroundObject(managers, this);
        CG = new CGObject(managers, this);
        LogueBox = new LogueBoxObject(managers);
    }

    public void DrawBaseGraphics()
    {
        if (BackGround != null)
            BackGround.Draw();
        if (CG != null)
            CG.Draw();
        if (LogueBox != null)
            LogueBox.Draw();
    }
}
