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

    public string GetAfter()
    {
        return _str.Substring(_prevIdx);
    }

    public static int ConvertClockToInt(string clock)
    {
        if (string.IsNullOrEmpty(clock))
            Program.Panic("[Script] Tried to convert null or empty clock to int.");
        var arr = clock.Split(':');
        if (arr.Length < 2)
            Program.Panic($"[Script] Wrong clock syntax.\n{clock}");
        return int.Parse(arr[0]) * 60 + int.Parse(arr[1]);
    }

    public static ICommand CreateCommand(string str)
    {
        if (string.IsNullOrEmpty(str))
            Program.Panic("[Script] Tried to create commands from null string.");
        if (str.Length == 0)
            Program.Panic("[Script] Tried to create commands from white line.");
        switch (str[0])
        {
            case '#':
                return CreateSystemCommand(str);
            case '/':
                return new Logue(str);
            default:
                return new MonoLogue(str);
        }
    }

    private static ICommand CreateSystemCommand(string str)
    {
        var parser = new Parser(str);
        switch (parser.GetNext())
        {
            case "#bg":
                return new ChangeBackGround(str);
            case "#ch":
                return new ChangeCharactor(str);
            case "#timeset":
                return new TimeSet(str);
            case "#timeadd":
                return new TimeAdd(str);
            case "#add":
                return new FlagAdd(str);
            default:
                return new MonoLogue(str);
        }
    }
}
