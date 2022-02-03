using System.Drawing;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp;

class Game
{
    private ICtrGameManager _gameManager = null;
    private ICtrGraphicsManager _graphicsManager = null;

    public Game()
    {
        this._gameManager = new GameManager();
        this._graphicsManager = new GraphicsManager();
    }

    public void Update()
    {
        _gameManager.Update();
    }

    public void Draw(Graphics g)
    {
        _graphicsManager.Draw(g);
    }
}
