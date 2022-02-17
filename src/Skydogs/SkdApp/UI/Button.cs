using Skydogs.SkdApp.GraphicsObject;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.UI;

class Button : AUIObject
{
    private readonly StringObject _strObject;

    public Button(IRefManagers managers, int x, int y, int w, int h, StringObject strObject)
        : base(managers, x, y, w, h)
    {
        _strObject = strObject;
    }

    public override void Draw()
    {
        _managers.GraphicsManager.AddGraphicsObject(_strObject);
    }
}