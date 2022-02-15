namespace Skydogs.SkdApp.GameObject.Script;

class Parser
{
    private readonly string _str = null;
    private int _prevIdx = 0;
    private readonly char _separator;

    public Parser(string str, char separator = ' ')
    {
        if (str == null)
            Program.Panic("[Script] Parser given null string to construct.");
        _str = str;
        _separator = separator;
    }

    public void Skip()
    {
        for (int i = _prevIdx; i < _str.Length; ++i)
        {
            if (_str[i] != _separator)
                continue;
            _prevIdx = i + 1;
            return;
        }
    }

    public string GetNext()
    {
        if (_prevIdx == _str.Length)
            return string.Empty;
        for (int i = _prevIdx; i < _str.Length; ++i)
        {
            if (_str[i] != _separator)
                continue;
            string res = _str.Substring(_prevIdx, i - _prevIdx);
            _prevIdx = i + 1;
            return res;
        }
        string last = _str.Substring(_prevIdx);
        _prevIdx = _str.Length;
        return last;
    }

    public string GetAfter() => _str.Substring(_prevIdx);

    public string GetAll() => _str;

    public static int ConvertClockToInt(string clock)
    {
        if (string.IsNullOrEmpty(clock))
            Program.Panic("[Script] Tried to convert null or empty clock to int.");
        var parser = new Parser(clock, ':');
        return int.Parse(parser.GetNext()) * 60 + int.Parse(parser.GetNext());
    }

    public static ICommand CreateCommand(string[] data, ref int i)
    {
        if (data == null)
            Program.Panic("[Script] Tried to create commands from null string array.");
        switch (data[i][0])
        {
            case '#':
                return CreateSystemCommand(data, ref i);
            case '/':
                return new Logue(data[i]);
            default:
                return new MonoLogue(data[i]);
        }
    }

    private static ICommand CreateSystemCommand(string[] data, ref int i)
    {
        if (data == null)
            Program.Panic("[Script] Tried to create system commands from null string array.");
        var parser = new Parser(data[i]);
        switch (parser.GetNext())
        {
            case "#bg":
                return new ChangeBackGround(parser.GetAfter());
            case "#ch":
                return new ChangeCharactor(parser.GetAfter());
            case "#wait":
                return new Wait(parser.GetAfter());
            case "#goto":
                return new Goto(parser.GetAfter());
            case "#timeset":
                return new TimeSet(parser.GetAfter());
            case "#timeadd":
                return new TimeAdd(parser.GetAfter());
            case "#add":
                return new FlagAdd(parser.GetAfter());
            case "#bul":
                return new BulletCommand(data, ref i);
            default:
                Program.Panic($"Unexpected command.\n{data[i]}");
                return null;
        }
    }
}
