using Skydogs.SkdApp.GraphicsObject;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.Scene;

class LoadScene : IScene
{
    private readonly IRefManagers _managers;
    private int _cnt = 0;

    public LoadScene(IRefManagers managers)
    {
        _managers = managers;
    }

    void IScene.Update()
    {
        if (_cnt == 0)
        {
            ResourceX.LoadImage("img.reimu");
            ResourceX.LoadCharacterImage("", 'a');
        }
        _managers.GraphicsManager.AddGraphicsObject(new ImageObject("img.reimu", 0.0f, 0.0f, 640.0f, 640.0f));
        _managers.GraphicsManager.AddGraphicsObject(new ImageObject("chr.a", 0.0f, 0.0f, 64.0f, 64.0f));
        ++_cnt;
    }
}
