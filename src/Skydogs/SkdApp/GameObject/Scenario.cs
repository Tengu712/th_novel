using System.Collections.Generic;
using Skydogs.SkdApp.Resource;

namespace Skydogs.SkdApp.GameObject;

class BlockHeader
{
    public int StartTime {get; private set; } = 0;
    public int EndTime {get; private set; } = 0;
    public bool IsForced { get; private set; } = false;

    private int ConvertClockToInt(string clock)
    {
        var arr = clock.Split(':');
        if (arr.Length < 2)
            throw new System.FormatException();
        return int.Parse(arr[0]) * 60 + int.Parse(arr[1]);
    }

    public BlockHeader(string str)
    {
        int prev = 0;
        int cnt = 0;
        for (int i = 0; i < str.Length; ++i)
        {
            if (str[i] != ' ')
                continue;
            switch (cnt)
            {
                case 1:
                    StartTime = ConvertClockToInt(str.Substring(prev, i - prev));
                    break;
                case 2:
                    EndTime = ConvertClockToInt(str.Substring(prev, i - prev));
                    break;
                case 3:
                    IsForced = str.Substring(prev, i - prev).Equals("!");
                    break;
                default:
                    break;
            }
            prev = i + 1;
            ++cnt;
        }
    }
}

class Block
{
    private BlockHeader _header = null;

    public Block() { }

    public int Load(string[] data, int idx)
    {
        for (int i = idx; i < data.Length; ++i)
        {
            if (data[i].Length == 0)
            {
                if (_header != null)
                    return i + 1;
                else
                    continue;
            }
            if (data[i][0] == '@')
                continue;
            if (data[i][0] == '%')
            {
                if (_header != null)
                    throw new System.FormatException();
                _header = new BlockHeader(data[i]);
                System.Console.WriteLine(_header.StartTime);
                System.Console.WriteLine(_header.EndTime);
                System.Console.WriteLine(_header.IsForced);
                continue;
            }
            if (_header == null)
                throw new System.FormatException();
            System.Console.Write("  ");
            System.Console.WriteLine(data[i]);
        }
        return -1;
    }
}

class Scenario
{
    private readonly LinkedList<Block> blocks = new LinkedList<Block>();

    public Scenario(string key)
    {
        var data = ResourceX.GetScenario(key);
        var idx = 0;
        while (true)
        {
            var block = new Block();
            idx = block.Load(data, idx);
            if (idx < 0)
                break;
            blocks.AddLast(block);
        }
    }
}
