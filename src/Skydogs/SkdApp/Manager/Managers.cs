namespace Skydogs.SkdApp.Manager;

interface ICtrManagers
{
    ICtrFpsManager FpsManager { get; }
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
    private FpsManager _fpsManager;
    private GraphicsManager _graphicsManager;
    private SceneManager _sceneManager;

    public Managers()
    {
        _fpsManager = new FpsManager(this);
        _graphicsManager = new GraphicsManager();
        _sceneManager = new SceneManager(this);
    }

    ICtrFpsManager ICtrManagers.FpsManager { get { return _fpsManager; } }
    ICtrGraphicsManager ICtrManagers.GraphicsManager { get { return _graphicsManager; } }
    ICtrSceneManager ICtrManagers.SceneManager { get { return _sceneManager; } }

    IRefGraphicsManager IRefManagers.GraphicsManager { get { return _graphicsManager; } }
    IRefSceneManager IRefManagers.SceneManager { get { return _sceneManager; } }
}
