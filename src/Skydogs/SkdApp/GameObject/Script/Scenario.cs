using System.Collections.Generic;
using Skydogs.SkdApp.Resource;
using Skydogs.SkdApp.Manager;
using Skydogs.SkdApp.Scene.GameScene;

namespace Skydogs.SkdApp.GameObject.Script;

class Scenario
{
    private readonly LinkedList<Block> _blocks = new LinkedList<Block>();
    private Block _currentBlock = null;

    public Scenario(IRefGameInformation ginf, LoadImageRequest rqImage, string key)
    {
        var data = ResourceX.GetScenario(key);
        if (data == null)
            Program.Panic($"The scenario '{key}' not exist.");
        rqImage.Add(ResourceX.GetKeysBackGround(data[0]));
        ginf.BackGround.SetPlace(data[0]);
        var idx = 1;
        while (true)
        {
            try
            {
                var block = new Block(data, ref idx);
                block.GetLoadRequest(rqImage);
                _blocks.AddLast(block);
            }
            catch (System.IO.EndOfStreamException)
            {
                break;
            }
        }
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
