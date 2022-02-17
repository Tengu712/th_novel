using Skydogs.SkdApp.GameObject.Effect;
using Skydogs.SkdApp.GraphicsObject;
using Skydogs.SkdApp.Manager;
using Skydogs.SkdApp.Resource;

namespace Skydogs.SkdApp.GameObject;

class BackGroundObject
{
    private readonly IRefManagers _managers;
    private readonly GameInformation _ginf;
    private readonly ImageObject _imageObject;

    public string _place = "";
    private EffectSwapBackGround _effect = null;

    public BackGroundObject(IRefManagers managers, GameInformation ginf)
    {
        _managers = managers;
        _ginf = ginf;
        _imageObject = new ImageObject { Width = 1280.0f, Height = 720.0f, IsCenter = false };
    }

    public void SwapStart() => _effect = new EffectSwapBackGround(ResourceX.GetKeyBackGround(_place, _ginf.Time), 30);
    public void SetPlace(string place) => _place = place;

    public void Draw()
    {
        _imageObject.ImageName = ResourceX.GetKeyBackGround(_place, _ginf.Time);
        _managers.GraphicsManager.AddGraphicsObject(_imageObject);
        if (_effect == null)
            return;
        _managers.GraphicsManager.AddGraphicsObject(_effect.GetImageObject());
        if (_effect.Update())
            _effect = null;
    }
}
