using System.Collections.Generic;
using Skydogs.SkdApp.GameObject.Effect;
using Skydogs.SkdApp.Resource;
using Skydogs.SkdApp.Scene.GameScene;

namespace Skydogs.SkdApp.GameObject.Script;

interface ICommand
{
    void GetLoadRequest(LoadImageRequest rqImage);
    bool Update(IRefGameInformation ginf);
}

abstract class ACommandNoResource : ICommand
{
    public ACommandNoResource() { }
    public void GetLoadRequest(LoadImageRequest rqImage) { }
    public abstract bool Update(IRefGameInformation ginf);
}

class MonoLogue : ACommandNoResource
{
    private readonly string _logue;

    public MonoLogue(string str)
    {
        _logue = str.Replace("\\n", "\n");
    }

    public override bool Update(IRefGameInformation ginf)
    {
        ginf.LogueBox.Set("", _logue);
        return ginf.ClickedLeft;
    }
}

class Logue : ACommandNoResource
{
    private readonly string _name;
    private readonly string _logue;

    public Logue(string str)
    {
        var parser = new Parser(str, '/');
        parser.Skip();
        _name = parser.GetNext();
        _logue = parser.GetNext().Replace("\\n", "\n");
    }

    public override bool Update(IRefGameInformation ginf)
    {
        ginf.LogueBox.Set(_name, _logue);
        return ginf.ClickedLeft;
    }
}

class ChangeBackGround : ICommand
{
    private readonly string _place;
    private readonly bool _isForce;

    public ChangeBackGround(string str)
    {
        var parser = new Parser(str);
        _place = parser.GetNext();
        _isForce = parser.GetNext().Equals("!");
    }

    public void GetLoadRequest(LoadImageRequest rqImage) => rqImage.Add(ResourceX.GetKeysBackGround(_place));

    public bool Update(IRefGameInformation ginf)
    {
        if (ginf.BackGround.Place.Equals(_place))
            return true;
        if (!_isForce)
            ginf.BackGround.SwapStart();
        ginf.BackGround.SetPlace(_place);
        return true;
    }
}

class ChangeCharactor : ICommand
{
    private readonly string _name;
    private readonly string _express;
    private readonly string _position;
    private readonly bool _isForce;

    public ChangeCharactor(string str)
    {
        var parser = new Parser(str);
        _name = parser.GetNext();
        _express = parser.GetNext();
        _position = parser.GetNext();
        _isForce = parser.GetNext().Equals("!");
    }

    public void GetLoadRequest(LoadImageRequest rqImage)
    {
    }

    public bool Update(IRefGameInformation ginf)
    {
        return true;
    }
}

class Wait : ACommandNoResource
{
    private readonly EffectWait _effect;

    public Wait(string str)
    {
        _effect = new EffectWait(int.Parse(str));
    }

    public override bool Update(IRefGameInformation ginf) => _effect.Update(ginf);
}

class Fadein : ACommandNoResource
{
    private readonly EffectFadein _effect;

    public Fadein(string str)
    {
        _effect = new EffectFadein(int.Parse(str));
    }

    public override bool Update(IRefGameInformation ginf) => _effect.Update(ginf);
}

class FadeStart : ACommandNoResource
{
    private readonly EffectFadeStart _effect;

    public FadeStart(string str)
    {
        _effect = new EffectFadeStart(int.Parse(str));
    }

    public override bool Update(IRefGameInformation ginf) => _effect.Update(ginf);
}

class DelBox : ACommandNoResource
{
    public DelBox() { }

    public override bool Update(IRefGameInformation ginf)
    {
        ginf.LogueBox.IsActive = false;
        return true;
    }
}

class Goto : ACommandNoResource
{
    private readonly EffectFadeout _effect;
    private readonly string _destination;

    public Goto(string str)
    {
        _effect = new EffectFadeout(30);
        _destination = str;
    }

    public override bool Update(IRefGameInformation ginf)
    {
        ginf.LogueBox.IsActive = false;
        if (_effect.Update(ginf))
        {
            ginf.SPlace = _destination;
            ginf.Scene = GameSceneID.Reload;
        }
        return false;
    }
}

class TimeSet : ACommandNoResource
{
    private readonly int _time;

    public TimeSet(string str)
    {
        _time = Parser.ConvertClockToInt(str);
    }

    public override bool Update(IRefGameInformation ginf)
    {
        ginf.AllTime = _time;
        return true;
    }
}

class TimeAdd : ACommandNoResource
{
    private readonly int _time;

    public TimeAdd(string str)
    {
        _time = Parser.ConvertClockToInt(str);
    }

    public override bool Update(IRefGameInformation ginf)
    {
        int prev = ginf.Day;
        ginf.AllTime += _time;
        if (prev != ginf.Day)
            ginf.Scene = GameSceneID.Reload;
        return true;
    }
}

class FlagAdd : ACommandNoResource
{
    private readonly string _flagName;
    private readonly int _add;

    public FlagAdd(string str)
    {
        var parser = new Parser(str);
        _flagName = parser.GetNext();
        _add = int.Parse(parser.GetNext());
    }

    public override bool Update(IRefGameInformation ginf)
    {
        return true;
    }
}

class BulletCommand : ICommand
{
    private readonly string _kind;
    private readonly LinkedList<ICommand> _succeededs = new LinkedList<ICommand>();
    private readonly LinkedList<ICommand> _faileds = new LinkedList<ICommand>();

    public BulletCommand(string[] data, ref int i)
    {
        if (data == null)
            Program.Panic("[Script] Tried to create #bul command from null string array.");
        var parser = new Parser(data[i]);
        parser.Skip();
        _kind = parser.GetNext();
        ++i;
        var flag = false;
        for (; i < data.Length; ++i)
        {
            if (data[i].Equals("#end"))
                return;
            if (data[i].Equals("#failed"))
            {
                flag = true;
                continue;
            }
            if (flag)
                _succeededs.AddLast(Parser.CreateCommand(data, ref i));
            else
                _faileds.AddLast(Parser.CreateCommand(data, ref i));
        }
        Program.Panic("[Script] #bul command not closed with #end.");
    }

    public void GetLoadRequest(LoadImageRequest rqImage)
    {
        foreach (var i in _succeededs)
            i.GetLoadRequest(rqImage);
        foreach (var i in _faileds)
            i.GetLoadRequest(rqImage);
    }

    public bool Update(IRefGameInformation ginf)
    {
        return true;
    }
}
