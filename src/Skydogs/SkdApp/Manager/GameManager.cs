using Skydogs.SkdApp.Scene;

namespace Skydogs.SkdApp.Manager;

class GameManager
{
    private IScene _scene = null;

    public GameManager()
    {
        this._scene = new TitleScene(this);
        _scene.Init();
    }

    public void Update()
    {
        _scene.Update();
    }
}