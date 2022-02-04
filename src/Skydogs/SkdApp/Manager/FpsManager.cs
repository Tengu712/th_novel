using System.Runtime.InteropServices;
using Skydogs.SkdApp.GraphicsObject;

namespace Skydogs.SkdApp.Manager;

interface ICtrFpsManager
{
    void Measure();
}

class FpsManager : ICtrFpsManager
{
    private int _cnt;
    private double _fps;
    private long _lastCounter;
    private IRefManagers _managers;
    private StringObject _stringObject;

    public FpsManager(IRefManagers managers)
    {
        _cnt = 0;
        _fps = 0.0;
        _lastCounter = 0;
        _managers = managers;
        _stringObject = new StringObject("", 640.0f, 640.0f);
    }

    void ICtrFpsManager.Measure()
    {
        long currentCounter = 0;
        QueryPerformanceCounter(ref currentCounter);
        long frequency = 0;
        QueryPerformanceFrequency(ref frequency);
        var span = (double)(currentCounter - _lastCounter) / (double)frequency;
        if (span > 1.0)
        {
            _fps = (double)_cnt / span;
            _cnt = 0;
            _lastCounter = currentCounter;
            System.Console.WriteLine(_fps);
        }
        //fpsstr.String = _fps.ToString();
        //_managers.GraphicsManager.AddGraphicsObject(_stringObject);
        ++_cnt;
    }

    [DllImport("kernel32.dll")]
    private static extern bool QueryPerformanceCounter(ref long lpPerformanceCount);

    [DllImport("kernel32.dll")]
    private static extern bool QueryPerformanceFrequency(ref long lpFrequency);
}