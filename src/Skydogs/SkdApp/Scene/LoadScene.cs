using Skydogs.SkdApp.GraphicsObject;
using Skydogs.SkdApp.Manager;
using Skydogs.SkdApp.Resource;

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
            var rqTemp = new LoadImageRequest();
            rqTemp.Add("img.load");
            ImageLoader.LoadTemp(rqTemp);
            _managers.GraphicsManager.AddGraphicsObject(
                new ImageObject { ImageName = "img.load", SqSize = 1280.0f, IsCenter = false });
            ++_cnt;
            return;
        }
        ResourceX.LoadFont("fnt.normal");
        var rq = new LoadImageRequest();
        rq.AddCharacterKeysWithString("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", "fnt.normal");
        ImageLoader.Load(rq);
        _managers.SceneManager.ChangeScene(SceneID.Title);
    }
}
