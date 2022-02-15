using System.Collections.Generic;

namespace Skydogs.SkdApp.GameObject.Script;

class BlockHeader
{
    private readonly int _startTime;
    private readonly int _endTime;
    private readonly Condition _condition;

    public BlockHeader(string str)
    {
        var parser = new Parser(str);
        parser.Skip();
        _startTime = Parser.ConvertClockToInt(parser.GetNext());
        _endTime = Parser.ConvertClockToInt(parser.GetNext());
        _condition = new Condition(parser.GetNext());
    }

    public bool Check(GameInformation ginf)
    {
        return _startTime <= ginf.Time && ginf.Time < _endTime && _condition.Check(ginf);
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
        if (_currentCommand == null)
            Program.Panic("[Script] Empty block found.");
        if (_currentCommand.Value.Update(ginf))
            _currentCommand = _currentCommand.Next;
        if (_currentCommand == null)
            ginf.Scene = GameSceneID.Neutral;
    }
}
