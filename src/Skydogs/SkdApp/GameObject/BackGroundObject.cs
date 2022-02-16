using Skydogs.SkdApp.GraphicsObject;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp.GameObject;

class BackGroundObject
{
    private readonly IRefManagers _managers;
    private readonly GameInformation _ginf;
    private readonly ImageObject _imageObject;
    private string _place = "";

    public BackGroundObject(IRefManagers managers, GameInformation ginf)
    {
        _managers = managers;
        _ginf = ginf;
        _imageObject = new ImageObject { SqSize = 1280.0f, IsCenter = false };
    }

    public void SetPlace(string place)
    {
        _place = place;
    }

    public void Draw()
    {
        if (420 <= _ginf.Time && _ginf.Time < 960)
            _imageObject.ImageName = $"img.{_place}.day";
        else if (960 <= _ginf.Time && _ginf.Time < 1080)
            _imageObject.ImageName = $"img.{_place}.evening";
        else
            _imageObject.ImageName = $"img.{_place}.night";
        _managers.GraphicsManager.AddGraphicsObject(_imageObject);
    }
}
