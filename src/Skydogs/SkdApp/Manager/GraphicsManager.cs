using Skydogs.SkdApp.GraphicsObject;

namespace Skydogs.SkdApp.Manager;

interface ICtrGraphicsManager
{
    void Draw();
}

interface IRefGraphicsManager
{
    void AddGraphicsObject(IGraphicsObject obj);
}

class GraphicsManager : ICtrGraphicsManager, IRefGraphicsManager
{
    private const int MAX_GRAPHICS_OBJECTS = 500;
    private IGraphicsObject[] _objs = new IGraphicsObject[MAX_GRAPHICS_OBJECTS];

    public GraphicsManager() { }

    void ICtrGraphicsManager.Draw()
    {
        foreach (var i in _objs)
        {
            if (i == null)
                break;
            i.Draw();
        }
        _objs = new IGraphicsObject[MAX_GRAPHICS_OBJECTS];
    }

    void IRefGraphicsManager.AddGraphicsObject(IGraphicsObject obj)
    {
        for (int i = 0; i < MAX_GRAPHICS_OBJECTS; ++i)
        {
            if (_objs[i] != null)
                continue;
            _objs[i] = obj;
            return;
        }
    }
}
