using System.Collections.Generic;
using Skydogs.SkdApp.Resource;
using Skydogs.SkdApp.Manager;
using Skydogs.SkdApp.Scene.GameScene;

namespace Skydogs.SkdApp.GameObject.Script;

class Scenario
{
    private readonly LinkedList<Block> _blocks = new LinkedList<Block>();
    private Block _currentBlock = null;

    public Scenario(string key)
    {
        var data = ResourceX.GetScenario(key);
        if (data == null)
            Program.Panic($"The scenario '{key}' not exist.");
        var idx = 1;
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

    public void GetLoadRequest(LoadImageRequest rqImage)
    {
        foreach (var i in _blocks)
            i.GetLoadRequest(rqImage);
    }

    public void Check(IRefGameInformation ginf)
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

    public void Update(IRefGameInformation ginf)
    {
        if (_currentBlock == null)
            Program.Panic("Null scenario updated.");
        _currentBlock.Update(ginf);
    }
}
