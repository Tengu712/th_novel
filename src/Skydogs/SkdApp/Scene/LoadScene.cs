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
            ResourceX.LoadImage("img.load");
            _managers.GraphicsManager.AddGraphicsObject(
                new ImageObject { ImageName = "img.load", SqSize = 1280.0f, IsCenter = false });
            return;
        }
        ++_cnt;
        ResourceX.LoadFonts("fnt.normal");
        ResourceX.LoadImage("img.reimu");
        ResourceX.LoadCharacterImage("fnt.normal", 'a');
        ResourceX.LoadCharacterImage("fnt.normal", 'd');
        _managers.SceneManager.ChangeScene(SceneID.Title);
    }
}
