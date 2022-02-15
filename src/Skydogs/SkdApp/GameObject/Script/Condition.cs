using System;

namespace Skydogs.SkdApp.GameObject.Script;

class ConditionPart
{
    private readonly Func<GameInformation, bool> _func;
    private readonly ConditionPart _next = null;
    private readonly bool _and = false;

    public ConditionPart(Parser parser)
    {
        _func = Default;
        switch (parser.GetNext())
        {
            case "!":
                _func = Force;
                break;
            case "talk":
                _func = Talk;
                break;
            case "goto":
            case ">":
            case "<":
            case "=":
            case ">=":
            case "<=":
                return;
            default:
                Program.Panic($"[Script] Wrong condition syntax.\n{parser.GetAll()}");
                return;
        }
        var next = parser.GetNext();
        if (string.IsNullOrEmpty(next))
            return;
        switch (next)
        {
            case "&&":
                _and = true;
                break;
            case "||":
                _and = false;
                break;
            default:
                Program.Panic($"[Script] Wrong condition syntax.\n{parser.GetAll()}");
                return;
        }
        _next = new ConditionPart(parser);
    }

    private bool Default(GameInformation ginf) => false;
    private bool Force(GameInformation ginf) => true;
    private bool Talk(GameInformation ginf) => ginf.Selection == SelectionID.Talk;

    public bool Check(GameInformation ginf)
    {
        if (_next == null)
            return _func(ginf);
        if (_and)
            return _func(ginf) && _next.Check(ginf);
        else
            return _func(ginf) || _next.Check(ginf);
    }
}

class Condition
{
    private readonly ConditionPart _first;

    public Condition(string str)
    {
        var parser = new Parser(str);
        _first = new ConditionPart(parser);
    }

    public bool Check(GameInformation ginf) => _first.Check(ginf);
}