using Skydogs.SkdApp.UI;

namespace Skydogs.SkdApp.Manager;

interface ICtrUIManager
{
    void Clicked(bool left, int x, int y);
}

interface IRefUIManager
{
    void AddUI(IUI ui);
}

class UIManager : ICtrUIManager, IRefUIManager
{
    private const int MAX_UIS = 20;
    private IUI[] _uis = new IUI[MAX_UIS];

    public UIManager() { }

    void ICtrUIManager.Clicked(bool left, int x, int y)
    {
        System.Console.WriteLine(left);
        int res = -1;
        for (int i = 0; i < MAX_UIS; ++i)
        {
            if (_uis[i] == null)
                continue;
            if (_uis[i].Clicked(left, x, y))
                res = i;
        }
        if (res != -1)
            _uis[res].Action();
        _uis = new IUI[MAX_UIS];
    }

    void IRefUIManager.AddUI(IUI ui)
    {
        for (int i = 0; i < MAX_UIS; ++i)
        {
            if (_uis[i] != null)
                continue;
            _uis[i] = ui;
            return;
        }
    }
}