using System.Collections.Generic;

namespace Skydogs.SkdApp.GameObject.Script;

class BlockHeader
{
    public int StartTime { get; private set; } = 0;
    public int EndTime { get; private set; } = 0;
    public bool IsForced { get; private set; } = false;

    public BlockHeader(string str)
    {
        var parser = new Parser(str);
        parser.Skip();
        StartTime = Parser.ConvertClockToInt(parser.GetNext());
        EndTime = Parser.ConvertClockToInt(parser.GetNext());
        IsForced = parser.GetNext().Equals("!");
    }

    public bool Check(GameInformation ginf)
    {
        return StartTime <= ginf.Time && ginf.Time < EndTime && IsForced;
    }
}

class Block
{
    private BlockHeader _header = null;
    private readonly LinkedList<ICommand> _commands = new LinkedList<ICommand>();
    private LinkedListNode<ICommand> _currentCommand = null;

    public Block(string[] data, ref int i)
    {
        for (; i < data.Length; ++i)
        {
            if (data[i].Length == 0)
            {
                if (_header != null)
                    return;
                else
                    continue;
            }
            if (data[i][0] == '@')
                continue;
            if (data[i][0] == '%')
            {
                if (_header != null)
                    Program.Panic($"[Script] Block start in block.\n{i}:{data[i]}");
                _header = new BlockHeader(data[i]);
                continue;
            }
            if (_header == null)
                Program.Panic($"[Script] Command found without block.\n{i}:{data[i]}");
            _commands.AddLast(Parser.CreateCommand(data, ref i));
        }
        throw new System.IO.EndOfStreamException();
    }

    public bool IsEnd()
    {
        return _currentCommand == null;
    }

    public bool Check(GameInformation ginf)
    {
        return _header.Check(ginf);
    }

    public void Update(GameInformation ginf)
    {
        if (_currentCommand == null)
            _currentCommand = _commands.First;
        if (_currentCommand.Value.Update(ginf))
            _currentCommand = _currentCommand.Next;
        if (_currentCommand == null)
            ginf.Scene = GameSceneID.Neutral;
    }
}
