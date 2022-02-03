using Skydogs.SkdApp.Scene;

namespace Skydogs.SkdApp.Manager;

interface ICtrSceneManager
{
    void Update();
}

interface IRefSceneManager
{

}

class SceneManager : ICtrSceneManager, IRefSceneManager
{
    private IScene _scene = null;

    public SceneManager(IRefManagers managers)
    {
        _scene = new TitleScene(managers);
        _scene.Init();
    }

    public void Update()
    {
        _scene.Update();
    }
}