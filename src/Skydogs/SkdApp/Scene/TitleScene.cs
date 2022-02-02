using System;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.Scene;

class TitleScene : IScene
{
    private GameManager _gmanager = null;

    public TitleScene(GameManager gmanager)
    {
        this._gmanager = gmanager;
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
