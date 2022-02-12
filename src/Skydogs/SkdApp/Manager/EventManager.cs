using System.Windows.Forms;

namespace Skydogs.SkdApp.Manager;

interface ICtrEventManager
{
    void Clear();
    void Clicked(MouseEventArgs e);
    void DownedKey(KeyEventArgs e);
    void UppedKey(KeyEventArgs e);
}

interface IRefEventManager
{
    bool W { get; }
    bool A { get; }
    bool S { get; }
    bool D { get; }
    bool Shift { get; }
    bool ClickedMouseLeft(int x, int y, int w, int h);
    bool ClickedMouseRight(int x, int y, int w, int h);
}

class EventManager : ICtrEventManager, IRefEventManager
{
    private int _mouse_button = -1;
    private int _mouse_x = -1;
    private int _mouse_y = -1;

    public bool W { get; private set; } = false;
    public bool A { get; private set; } = false;
    public bool S { get; private set; } = false;
    public bool D { get; private set; } = false;
    public bool Shift { get; private set; } = false;

    public EventManager() { }

    void ICtrEventManager.Clear()
    {
        _mouse_button = -1;
        _mouse_x = -1;
        _mouse_y = -1;
    }

    void ICtrEventManager.Clicked(MouseEventArgs e)
    {
        switch (e.Button)
        {
            case MouseButtons.Left:
                _mouse_button = 0;
                _mouse_x = e.X;
                _mouse_y = e.Y;
                break;
            default:
                _mouse_button = -1;
                break;
        }
    }

    void ICtrEventManager.DownedKey(KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.W:
                W = true;
                break;
            case Keys.A:
                A = true;
                break;
            case Keys.S:
                S = true;
                break;
            case Keys.D:
                D = true;
                break;
            default:
                break;
        }
        if (e.Shift)
            Shift = true;
    }

    void ICtrEventManager.UppedKey(KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.W:
                W = false;
                break;
            case Keys.A:
                A = false;
                break;
            case Keys.S:
                S = false;
                break;
            case Keys.D:
                D = false;
                break;
            default:
                break;
        }
        if (e.Shift)
            Shift = false;
    }

    bool IRefEventManager.ClickedMouseLeft(int x, int y, int w, int h)
    {
        return _mouse_button == 0 && x <= _mouse_x && x + w >= _mouse_x && y <= _mouse_y && y + h >= _mouse_y;
    }

    bool IRefEventManager.ClickedMouseRight(int x, int y, int w, int h)
    {
        return _mouse_button == 1 && x <= _mouse_x && x + w >= _mouse_x && y <= _mouse_y && y + h >= _mouse_y;
    }
}
