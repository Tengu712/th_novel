using Skydogs.SkdApp.GraphicsObject;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.Scene;

class LoadScene : IScene
{
    private int _cnt;
    private readonly IRefManagers _managers;

    public LoadScene(IRefManagers managers)
    {
        _cnt = 0;
        _managers = managers;
    }

    void IScene.Update()
    {
        if (_cnt == 0)
        {
            _managers.GraphicsManager.Load("red");
        }
        _managers.GraphicsManager.AddGraphicsObject(new ImageObject("red", 0.0f, 0.0f, 640.0f, 640.0f));
        ++_cnt;
    }
}