using Skydogs.SkdApp.GameObject.Effect;
using Skydogs.SkdApp.GraphicsObject;
using Skydogs.SkdApp.Manager;
using Skydogs.SkdApp.Resource;

namespace Skydogs.SkdApp.GameObject;

class BackGroundObject
{
    private readonly IRefGraphicsManager _graphicsManager;
    private readonly IRefGameInformation _ginf;
    private readonly ImageObject _imageObject;
    private EffectSwapBackGround _effect = null;

    public string Place { get; private set; } = "";

    public BackGroundObject(IRefGraphicsManager graphicsManager, IRefGameInformation ginf)
    {
        _graphicsManager = graphicsManager;
        _ginf = ginf;
        _imageObject = new ImageObject { Width = 1280.0f, Height = 720.0f, IsCenter = false };
    }

    public void SwapStart() => _effect = new EffectSwapBackGround(ResourceX.GetKeyBackGround(Place, _ginf.Time), 30);
    public void SetPlace(string place) => Place = place;

    public void Draw()
    {
        _imageObject.ImageName = ResourceX.GetKeyBackGround(Place, _ginf.Time);
        _graphicsManager.AddGraphicsObject(_imageObject);
        if (_effect == null)
            return;
        _graphicsManager.AddGraphicsObject(_effect.GetImageObject());
        if (_effect.Update())
            _effect = null;
    }
}
