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
            ++_cnt;
            return;
        }
        if (_cnt == 1)
        {
            ++_cnt;
            ResourceX.LoadFonts("fnt.normal");
            ResourceX.LoadImage("img.reimu");
            ResourceX.LoadCharacterImage("fnt.normal", 'a');
            ResourceX.LoadCharacterImage("fnt.normal", 'd');
            return;
        }
        //_managers.GraphicsManager.AddGraphicsObject(new ImageObject { ImageName = "img.reimu", SqSize = 640.0f });
        _managers.GraphicsManager.AddGraphicsObject(new StringObject { String = "abcdefghijk" });
        ++_cnt;
    }
}
