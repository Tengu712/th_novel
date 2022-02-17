using Skydogs.SkdApp.GraphicsObject;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.GameObject;

class LogueBoxObject
{
    private readonly IRefGraphicsManager _graphicsManager;
    private readonly ImageObject _logueBox;
    private readonly StringObject _speakerName;
    private readonly StringObject _logue;

    public bool IsActive { get; set; } = false;

    public LogueBoxObject(IRefGraphicsManager graphcisManager)
    {
        _graphicsManager = graphcisManager;
        _logueBox = new ImageObject
        {
            ImageName = "img.loguebox",
            PosY = 480.0f,
            Width = GeneralInformation.WIDTH,
            Height = 320.0f,
            Alpha = 0.8f,
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

    public void Draw()
    {
        if (!IsActive)
            return;
        _graphicsManager.AddGraphicsObject(_logueBox);
        _graphicsManager.AddGraphicsObject(_speakerName);
        _graphicsManager.AddGraphicsObject(_logue);
    }
}
