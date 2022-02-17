using System.Collections.Generic;
using Skydogs.SkdApp.GameObject.Script;
using Skydogs.SkdApp.GraphicsObject;
using Skydogs.SkdApp.Resource;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.GameObject.GameScene;

class Scenario : IGameScene
{
    private readonly IRefManagers _managers;
    private readonly GameInformation _ginf;
    private readonly string _place;

    private readonly LinkedList<Block> _blocks = new LinkedList<Block>();
    private Block _currentBlock = null;

    public Scenario(IRefManagers managers, GameInformation ginf, string key)
    {
        _managers = managers;
        _ginf = ginf;
        var data = ResourceX.GetScenario(key);
        if (data == null)
            Program.Panic($"The scenario '{key}' not exist.");
        _place = data[0];
        ginf.BackGround.SetPlace(_place);
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
        rqImage.Add($"img.{_place}.day");
        //rqImage.Add($"img.{_place}.evening");
        //rqImage.Add($"img.{_place}.night");
        foreach (var i in _blocks)
            i.GetLoadRequest(rqImage);
    }

    public void Check()
    {
        if (_currentBlock != null && !_currentBlock.IsEnd())
        {
            _ginf.Scene = GameSceneID.Talking;
            return;
        }
        foreach (var i in _blocks)
        {
            if (!i.Check(_ginf))
                continue;
            _currentBlock = i;
            _ginf.Scene = GameSceneID.Talking;
            return;
        }
        _currentBlock = null;
        _ginf.Scene = GameSceneID.Selection;
    }

    public void Update()
    {
        if (_currentBlock == null)
            Program.Panic("Null scenario updated.");
        _currentBlock.Update(_ginf);
        _ginf.DrawBaseGraphics();
    }
}
