using System.Collections.Generic;

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
        return ginf.EventManager.ClickedMouseLeft(0, 0, GeneralInformation.WIDTH, GeneralInformation.HEIGHT);
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
        return ginf.EventManager.ClickedMouseLeft(0, 0, GeneralInformation.WIDTH, GeneralInformation.HEIGHT);
    }
}

class ChangeBackGround : ICommand
{
    private readonly string _imageName;
    private readonly bool _isForce;

    public ChangeBackGround(string str)
    {
        var parser = new Parser(str);
        parser.Skip();
        _imageName = parser.GetNext();
        _isForce = parser.GetNext().Equals("!");
    }

    bool ICommand.Update(GameInformation ginf)
    {
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
        parser.Skip();
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

class TimeSet : ICommand
{
    private readonly int _time = 0;

    public TimeSet(string str)
    {
        var parser = new Parser(str);
        parser.Skip();
        _time = Parser.ConvertClockToInt(parser.GetNext());
    }

    bool ICommand.Update(GameInformation ginf)
    {
        ginf.AllTime = _time;
        return true;
    }
}

class TimeAdd : ICommand
{
    private readonly int _time = 0;

    public TimeAdd(string str)
    {
        var parser = new Parser(str);
        parser.Skip();
        _time = Parser.ConvertClockToInt(parser.GetNext());
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
    private readonly int _add = 0;

    public FlagAdd(string str)
    {
        var parser = new Parser(str);
        parser.Skip();
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
                flag = true;
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
