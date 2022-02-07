namespace Skydogs.SkdApp.Manager;

interface ICtrManagers
{
    ICtrGraphicsManager GraphicsManager { get; }
    ICtrSceneManager SceneManager { get; }
}

interface IRefManagers
{
    IRefGraphicsManager GraphicsManager { get; }
    IRefSceneManager SceneManager { get; }
}

class Managers : ICtrManagers, IRefManagers
{
    private GraphicsManager _graphicsManager;
    private SceneManager _sceneManager;

    public Managers()
    {
        _graphicsManager = new GraphicsManager();
        _sceneManager = new SceneManager(this);
    }

    ICtrGraphicsManager ICtrManagers.GraphicsManager { get { return _graphicsManager; } }
    ICtrSceneManager ICtrManagers.SceneManager { get { return _sceneManager; } }

    IRefGraphicsManager IRefManagers.GraphicsManager { get { return _graphicsManager; } }
    IRefSceneManager IRefManagers.SceneManager { get { return _sceneManager; } }
}
