using System;
using System.Drawing;
using System.Windows.Forms;

namespace Skydogs.SkdApp;

class MainForm : Form
{
    private Game _game = null;

    public MainForm()
    {
        this.Text = "幻想異郷";
        this.ClientSize = new Size(1280, 720);
        this.MaximumSize = this.Size;
        this.MinimumSize = this.Size;
        this.MaximizeBox = false;
        this.SetStyle(ControlStyles.DoubleBuffer, true);
        this.SetStyle(ControlStyles.UserPaint, true);
        this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        this._game = new Game();
    }

    public void UpdateForm()
    {
        _game.Update();
        Refresh();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        _game.Draw(e.Graphics);
	}
}
