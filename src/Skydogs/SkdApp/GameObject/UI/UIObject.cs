using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.GameObject.UI;

abstract class AUIObject
{
    protected readonly IRefManagers _managers;
    protected readonly int _x;
    protected readonly int _y;
    protected readonly int _w;
    protected readonly int _h;

    public AUIObject(IRefManagers managers, int x, int y, int w, int h)
    {
        _managers = managers;
        _x = x;
        _y = y;
        _w = w;
        _h = h;
    }

    public bool Clicked() => _managers.EventManager.ClickedMouseLeft(_x, _y, _w, _h);
    public bool ClickedRight() => _managers.EventManager.ClickedMouseRight(_x, _y, _w, _h);

    public abstract void Draw();
}
