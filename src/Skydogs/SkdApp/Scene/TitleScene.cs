using Skydogs.SkdApp.GraphicsObject;
using Skydogs.SkdApp.Manager;
using Skydogs.SkdApp.Resource;

namespace Skydogs.SkdApp.Scene;

class TitleScene : IScene
{
    private readonly IRefManagers _managers;

    public TitleScene(IRefManagers managers)
    {
        _managers = managers;
        var rqImage = new LoadImageRequest();
        rqImage.Add("img.title");
        ImageLoader.LoadTemp(rqImage);
    }

    void IScene.Update()
    {
        _managers.GraphicsManager.AddGraphicsObject(
                new ImageObject { ImageName = "img.title", SqSize = 1280.0f, IsCenter = false });
    }
}
