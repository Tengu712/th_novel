using System.Collections.Generic;
using Skydogs.SkdApp.GameObject.Script;
using Skydogs.SkdApp.GraphicsObject;
using Skydogs.SkdApp.Resource;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.GameObject.Script;

class Scenario
{
    private readonly IRefManagers _managers;
    private readonly LinkedList<Block> _blocks = new LinkedList<Block>();
    private Block _currentBlock = null;

    public Scenario(IRefManagers managers, string key)
    {
        _managers = managers;
        var data = ResourceX.GetScenario(key);
        if (data == null)
            Program.Panic($"The scenario '{key}' not exist.");
        var idx = 0;
        while (true)
        {
            try
            {
                _blocks.AddLast(new Block(data, ref idx));
            }
            catch (System.IO.EndOfStreamException)
            {
                break;
            }
        }
    }

    public void Check(GameInformation ginf)
    {
        if (_currentBlock != null && !_currentBlock.IsEnd())
        {
            ginf.Scene = GameSceneID.Talking;
            return;
        }
        foreach (var i in _blocks)
        {
            if (!i.Check(ginf))
                continue;
            _currentBlock = i;
            ginf.Scene = GameSceneID.Talking;
            return;
        }
        _currentBlock = null;
        ginf.Scene = GameSceneID.Selection;
    }

    public void Update(GameInformation ginf)
    {
        if (_currentBlock == null)
            Program.Panic("Null scenario updated.");
        _currentBlock.Update(ginf);
        ginf.LogueBox.Draw(_managers.GraphicsManager);
    }
}
