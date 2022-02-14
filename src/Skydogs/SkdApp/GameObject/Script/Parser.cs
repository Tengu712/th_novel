namespace Skydogs.SkdApp.GameObject.Script;

class Parser
{
    private readonly string _str = null;
    private int prevIdx = 0;

    public Parser(string str)
    {
        _str = str;
    }

    public void Skip()
    {
        for (int i = prevIdx; i < _str.Length; ++i)
        {
            if (_str[i] != ' ')
                continue;
            prevIdx = i + 1;
            return;
        }
    }

    public string GetNext()
    {
        for (int i = prevIdx; i < _str.Length; ++i)
        {
            if (_str[i] != ' ')
                continue;
            string res = _str.Substring(prevIdx, i - prevIdx);
            prevIdx = i + 1;
            return res;
        }
        string last = _str.Substring(prevIdx);
        prevIdx = _str.Length;
        return last;
    }

    public string GetAfter()
    {
        return _str.Substring(prevIdx);
    }

    public static int ConvertClockToInt(string clock)
    {
        if (clock == null)
            throw new System.ArgumentNullException();
        var arr = clock.Split(':');
        if (arr.Length < 2)
            throw new System.FormatException();
        return int.Parse(arr[0]) * 60 + int.Parse(arr[1]);
    }

    public static ICommand CreateCommand(string str)
    {
        if (str == null)
            throw new System.ArgumentNullException();
        if (str.Length == 0)
            throw new System.ArgumentException();
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
