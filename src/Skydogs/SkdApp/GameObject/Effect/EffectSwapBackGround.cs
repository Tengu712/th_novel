using Skydogs.SkdApp.GraphicsObject;
using Skydogs.SkdApp.Manager;
using Skydogs.SkdApp.Resource;

namespace Skydogs.SkdApp.GameObject.Effect;

class EffectSwapBackGround : IEffect
{
    private readonly string _imageName;
    private readonly EffectWait _effect;

    public EffectSwapBackGround(string imageName, int upto)
    {
        _imageName = imageName;
        _effect = new EffectWait(upto);
    }

    public bool Update() => _effect.Update();
    public bool Update(GameInformation ginf) => _effect.Update();

    public ImageObject GetImageObject() => new ImageObject
    {
        ImageName = _imageName,
        Width = 1280.0f,
        Height = 720.0f,
        Alpha = 1.0f - System.Math.Max((float)_effect.Count / (float)_effect.Upto, 0.0f),
        IsCenter = false
    };
}