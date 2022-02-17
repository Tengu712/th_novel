namespace Skydogs.SkdApp.GameObject.Effect;

class EffectFadeStart : IEffect
{
    private readonly float _sub;

    public EffectFadeStart(int upto)
    {
        _sub = 1.0f / (float)System.Math.Max(upto, 1);
    }

    public bool Update(IRefGameInformation ginf)
    {
        ginf.CG.Image.Alpha = System.Math.Max(ginf.CG.Image.Alpha - _sub, 0.0f);
        return ginf.CG.Image.Alpha == 0.0f;
    }
}
