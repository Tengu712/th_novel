namespace Skydogs.SkdApp.Manager;

interface ICtrManagers
{
    ICtrGraphicsManager GraphicsManager { get; }
    ICtrSceneManager SceneManager { get; }
    ICtrUIManager UIManager { get; }
}

interface IRefManagers
{
    IRefGraphicsManager GraphicsManager { get; }
    IRefSceneManager SceneManager { get; }
    IRefUIManager UIManager { get; }
}

class Managers : ICtrManagers, IRefManagers
{
    private GraphicsManager _graphicsManager;
    private SceneManager _sceneManager;
    private UIManager _uiManager;

    public Managers()
    {
        _graphicsManager = new GraphicsManager();
        _sceneManager = new SceneManager(this);
        _uiManager = new UIManager();
    }

    ICtrGraphicsManager ICtrManagers.GraphicsManager { get { return _graphicsManager; } }
    ICtrSceneManager ICtrManagers.SceneManager { get { return _sceneManager; } }
    ICtrUIManager ICtrManagers.UIManager { get { return _uiManager; } }

    IRefGraphicsManager IRefManagers.GraphicsManager { get { return _graphicsManager; } }
    IRefSceneManager IRefManagers.SceneManager { get { return _sceneManager; } }
    IRefUIManager IRefManagers.UIManager { get { return _uiManager; } }
}
