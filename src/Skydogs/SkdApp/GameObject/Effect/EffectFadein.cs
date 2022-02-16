namespace Skydogs.SkdApp.GameObject.Effect;

class EffectFadein : IEffect
{
    private readonly EffectWait _effectWait;
    private float _start = 1.0f;

    public EffectFadein(int upto)
    {
        _effectWait = new EffectWait(System.Math.Max(upto, 1));
    }

    public bool Update(GameInformation ginf)
    {
        if (_effectWait.Count == 0)
            _start = ginf.CG.Image.Alpha;
        ginf.CG.Image.Alpha = _start *
            (1.0f - System.Math.Min((float)_effectWait.Count / (float)_effectWait.Upto, 1.0f));
        return _effectWait.Update(ginf);
    }
}
