using Skydogs.SkdApp.GraphicsObject;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.GameObject.UI;

class LogueBoxObject : AUIObject
{
    private readonly ImageObject _logueBox;
    private readonly StringObject _speakerName;
    private readonly StringObject _logue;

    public bool IsActive { get; set; } = false;

    public LogueBoxObject(IRefManagers managers)
        : base(managers, 0, 0, GeneralInformation.WIDTH, GeneralInformation.HEIGHT)
    {
        _logueBox = new ImageObject
        {
            ImageName = "img.loguebox",
            PosY = 480.0f,
            Width = GeneralInformation.WIDTH,
            Height = 320.0f,
            IsCenter = false,
        };
        _speakerName = new StringObject
        {
            PosY = 640.0f,
            Size = 30.0f,
            Alignment = StringObjectAlignment.Center,
        };
        _logue = new StringObject
        {
            PosX = 200.0f,
            PosY = 520.0f,
            MaxX = GeneralInformation.WIDTH - 200.0f,
            Size = 34.0f,
        };
    }

    public void Set(string speakerName, string logue)
    {
        _speakerName.String = speakerName;
        _logue.String = logue;
        IsActive = true;
    }

    public override void Draw()
    {
        if (!IsActive)
            return;
        _managers.GraphicsManager.AddGraphicsObject(_logueBox);
        _managers.GraphicsManager.AddGraphicsObject(_speakerName);
        _managers.GraphicsManager.AddGraphicsObject(_logue);
    }
}
