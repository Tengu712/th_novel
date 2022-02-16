namespace Skydogs.SkdApp.GameObject.Effect;

class EffectFadeout : IEffect
{
    private readonly EffectWait _effectWait;

    public EffectFadeout(int upto)
    {
        _effectWait = new EffectWait(System.Math.Max(upto, 1));
    }

    public bool Update(GameInformation ginf)
    {
        ginf.CG.Image.ImageName = "";
        ginf.CG.Image.Red = 0.0f;
        ginf.CG.Image.Green = 0.0f;
        ginf.CG.Image.Blue = 0.0f;
        ginf.CG.Image.Alpha = System.Math.Min((float)_effectWait.Count / (float)_effectWait.Upto, 1.0f);
        return _effectWait.Update(ginf);
    }
}
