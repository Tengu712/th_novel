using Skydogs.SkdApp.GameObject.UI;
using Skydogs.SkdApp.GraphicsObject;
using Skydogs.SkdApp.Manager;
using Skydogs.SkdApp.Resource;

namespace Skydogs.SkdApp.Scene;

class TitleScene : IScene
{
    private readonly IRefManagers _managers;
    private readonly Button _buttonPlay;

    public TitleScene(IRefManagers managers)
    {
        _managers = managers;
        _buttonPlay = new Button(managers, 600, 500, 80, 60,
            new StringObject { String = "Play", PosY = 500, Alignment = StringObjectAlignment.Center });
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
            new ImageObject { ImageName = "img.title", SqSize = 1280.0f, IsCenter = false });
        _buttonPlay.Draw();
    }
}
