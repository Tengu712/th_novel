using System.Collections.Generic;
using Skydogs.SkdApp.GameObject.Effect;

namespace Skydogs.SkdApp.GameObject.Script;

interface ICommand
{
    bool Update(GameInformation ginf);
}

class MonoLogue : ICommand
{
    private readonly string _logue;

    public MonoLogue(string str)
    {
        _logue = str.Replace("\\n", "\n");
    }

    bool ICommand.Update(GameInformation ginf)
    {
        ginf.LogueBox.Set("", _logue);
        return ginf.LogueBox.Clicked();
    }
}

class Logue : ICommand
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

    bool ICommand.Update(GameInformation ginf)
    {
        ginf.LogueBox.Set(_name, _logue);
        return ginf.LogueBox.Clicked();
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

    bool ICommand.Update(GameInformation ginf)
    {
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

    bool ICommand.Update(GameInformation ginf)
    {
        return true;
    }
}

class Wait : ICommand
{
    private readonly EffectWait _effect;

    public Wait(string str)
    {
        _effect = new EffectWait(int.Parse(str));
    }

    bool ICommand.Update(GameInformation ginf) => _effect.Update(ginf);
}

class Goto : ICommand
{
    private readonly EffectFadeout _effect;
    private readonly string _destination;

    public Goto(string str)
    {
        _effect = new EffectFadeout(60);
        _destination = str;
    }

    bool ICommand.Update(GameInformation ginf)
    {
        ginf.LogueBox.IsActive = false;
        if (_effect.Update(ginf))
        {
            ginf.Place = _destination;
            ginf.Scene = GameSceneID.Reload;
        }
        return false;
    }
}

class TimeSet : ICommand
{
    private readonly int _time;

    public TimeSet(string str)
    {
        _time = Parser.ConvertClockToInt(str);
    }

    bool ICommand.Update(GameInformation ginf)
    {
        ginf.AllTime = _time;
        return true;
    }
}

class TimeAdd : ICommand
{
    private readonly int _time;

    public TimeAdd(string str)
    {
        _time = Parser.ConvertClockToInt(str);
    }

    bool ICommand.Update(GameInformation ginf)
    {
        int prev = ginf.Day;
        ginf.AllTime += _time;
        if (prev != ginf.Day)
            ginf.Scene = GameSceneID.Reload;
        return true;
    }
}

class FlagAdd : ICommand
{
    private readonly string _flagName;
    private readonly int _add;

    public FlagAdd(string str)
    {
        var parser = new Parser(str);
        _flagName = parser.GetNext();
        _add = int.Parse(parser.GetNext());
    }

    bool ICommand.Update(GameInformation ginf)
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

    bool ICommand.Update(GameInformation ginf)
    {
        return true;
    }
}
