namespace Skydogs.SkdApp.GameObject.Effect;

class EffectWait : IEffect
{
    public int Count { get; private set; } = 0;
    public int Upto { get; private set; } = 0;

    public EffectWait(int upto)
    {
        Upto = upto;
    }

    public bool Update()
    {
        if (Count >= Upto)
            return true;
        ++Count;
        return false;
    }

    public bool Update(GameInformation ginf) => Update();
}