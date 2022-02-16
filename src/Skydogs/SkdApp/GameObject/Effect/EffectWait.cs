namespace Skydogs.SkdApp.GameObject.Effect;

class EffectWait : IEffect
{
    public int Count { get; private set; } = 0;
    public int Upto { get; private set; } = 0;

    public EffectWait(int upto)
    {
        Upto = upto;
    }

    public bool Update(GameInformation ginf)
    {
        if (Count >= Upto)
            return true;
        ++Count;
        return false;
    }
}