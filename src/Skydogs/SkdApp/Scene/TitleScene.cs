using System;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.Scene;

class TitleScene : IScene
{
    private IRefGameManager _gameManager = null;

    public TitleScene(IRefGameManager gameManager)
    {
        this._gameManager = gameManager;
    }

    public void Init()
    {
        Console.WriteLine("Hello");
    }

    public void Update()
    {
        Console.WriteLine("update title scene.");
    }
}
