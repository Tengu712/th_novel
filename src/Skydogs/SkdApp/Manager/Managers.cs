namespace Skydogs.SkdApp.Manager;

interface ICtrManagers
{
    ICtrGraphicsManager GetCtrGraphicsManager();
    ICtrSceneManager GetCtrSceneManager();
}

interface IRefManagers
{
    IRefGraphicsManager GetRefGraphicsManager();
    IRefSceneManager GetRefSceneManager();
}

class Managers : ICtrManagers, IRefManagers
{
    private GraphicsManager _graphicsManager = null;
    private SceneManager _sceneManager = null;

    public Managers()
    {
        this._sceneManager = new SceneManager(this);
        this._graphicsManager = new GraphicsManager();
    }

    public ICtrGraphicsManager GetCtrGraphicsManager() => _graphicsManager;
    public ICtrSceneManager GetCtrSceneManager() => _sceneManager;

    public IRefGraphicsManager GetRefGraphicsManager() => _graphicsManager;
    public IRefSceneManager GetRefSceneManager() => _sceneManager;
}
