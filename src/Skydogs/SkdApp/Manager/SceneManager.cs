using Skydogs.SkdApp.Scene;

namespace Skydogs.SkdApp.Manager;

interface ICtrSceneManager
{
    void Update();
}

interface IRefSceneManager
{
    void ChangeScene(SceneID id);
}

class SceneManager : ICtrSceneManager, IRefSceneManager
{
    private readonly IRefManagers _managers;
    private IScene _scene;

    public SceneManager(IRefManagers managers)
    {
        _managers = managers;
        _scene = new LoadScene(_managers);
    }

    void ICtrSceneManager.Update()
    {
        _scene.Update();
    }

    void IRefSceneManager.ChangeScene(SceneID id)
    {
        switch (id)
        {
            case SceneID.Load:
                _scene = new LoadScene(_managers);
                break;
            case SceneID.Title:
            default:
                _scene = new TitleScene(_managers);
                break;
        }
        System.GC.Collect();
    }
}
