namespace Skydogs.SkdApp.Manager;

interface ICtrManagers
{
    ICtrGraphicsManager GraphicsManager { get; }
    ICtrSceneManager SceneManager { get; }
    ICtrEventManager EventManager { get; }
}

interface IRefManagers
{
    IRefGraphicsManager GraphicsManager { get; }
    IRefSceneManager SceneManager { get; }
    IRefEventManager EventManager { get; }
}

class Managers : ICtrManagers, IRefManagers
{
    private GraphicsManager _graphicsManager;
    private SceneManager _sceneManager;
    private EventManager _eventManager;

    public Managers()
    {
        _graphicsManager = new GraphicsManager();
        _sceneManager = new SceneManager(this);
        _eventManager = new EventManager();
    }

    ICtrGraphicsManager ICtrManagers.GraphicsManager { get { return _graphicsManager; } }
    ICtrSceneManager ICtrManagers.SceneManager { get { return _sceneManager; } }
    ICtrEventManager ICtrManagers.EventManager { get { return _eventManager; } }

    IRefGraphicsManager IRefManagers.GraphicsManager { get { return _graphicsManager; } }
    IRefSceneManager IRefManagers.SceneManager { get { return _sceneManager; } }
    IRefEventManager IRefManagers.EventManager { get { return _eventManager; } }

    public void Update()
    {
        ((ICtrSceneManager)_sceneManager).Update();
        ((ICtrGraphicsManager)_graphicsManager).Draw();
        ((ICtrEventManager)_eventManager).Clear();
    }
}
