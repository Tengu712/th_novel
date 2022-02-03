using System;
using System.Drawing;
using System.Collections.Generic;
using Skydogs.SkdApp.GraphicsObject;

namespace Skydogs.SkdApp.Manager;

interface IGraphicsManager
{
    Dictionary<string, Image> Images { get; }
}

interface ICtrGraphicsManager : IGraphicsManager
{
    void Load();
    void Draw(Graphics g);
}

interface IRefGraphicsManager : IGraphicsManager
{
    void AddGraphicsObject(IGraphicsObject obj);
}

class GraphicsManager : ICtrGraphicsManager, IRefGraphicsManager
{
    private const int MAX_GRAPHICS_OBJECTS = 500;
    private IGraphicsObject[] _objs = null;

    public Dictionary<string, Image> Images { get; } = null;

    public GraphicsManager()
    {
        _objs = new IGraphicsObject[MAX_GRAPHICS_OBJECTS];
        Images = new Dictionary<string, Image>();
        Load();
    }

    public void Load()
    {
        Images.Add("reimu", Image.FromFile("reimu.png"));
    }

    public void Draw(Graphics g)
    {
        for (int i = 0; i < MAX_GRAPHICS_OBJECTS; ++i)
        {
            if (_objs[i] == null)
                return;
            _objs[i].Draw(this, g);
            _objs[i] = null;
        }
    }

    public void AddGraphicsObject(IGraphicsObject obj)
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
