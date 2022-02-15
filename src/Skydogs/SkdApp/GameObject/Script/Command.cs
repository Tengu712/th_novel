namespace Skydogs.SkdApp.GameObject.Script;

interface ICommand
{
    bool Update(GameInformation ginf);
}

class MonoLogue : ICommand
{
    private readonly string _logue = null;

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
    private readonly string _name = null;
    private readonly string _logue = null;

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
    private readonly string _imageName = null;
    private readonly bool _isForce = false;

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
    private readonly string _name = null;
    private readonly string _express = null;
    private readonly string _position = null;
    private readonly bool _isForce = false;

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
    private readonly string _flagName = null;
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
