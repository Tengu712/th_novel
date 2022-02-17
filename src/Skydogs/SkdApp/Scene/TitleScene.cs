using Skydogs.SkdApp.GraphicsObject;
using Skydogs.SkdApp.Manager;
using Skydogs.SkdApp.Resource;
using Skydogs.SkdApp.UI;

namespace Skydogs.SkdApp.Scene;

class TitleScene : IScene
{
    private readonly IRefManagers _managers;
    private readonly Button _buttonPlay;

    public TitleScene(IRefManagers managers)
    {
        _managers = managers;
        _buttonPlay = new Button(managers, 100, 450, 80, 54,
            new StringObject { String = "Play", PosX = 100.0f, PosY = 450.0f, Size = 54.0f });
        var rqImage = new LoadImageRequest();
        rqImage.Add("img.title");
        ImageLoader.LoadTemp(rqImage);
    }

    void IScene.Update()
    {
        if (_buttonPlay.Clicked())
        {
            _managers.SceneManager.ChangeScene(SceneID.GamePlay);
        }
        _managers.GraphicsManager.AddGraphicsObject(
            new ImageObject { ImageName = "img.title", Width = 1280.0f, Height = 720.0f, IsCenter = false });
        _buttonPlay.Draw();
    }
}
