using System;
using System.Drawing;
using System.Windows.Forms;

using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp;

class MainForm : Form
{
    private GameManager _gmanager = null;

    public MainForm()
    {
        this.Text = "Novel";
        this.ClientSize = new Size(1280, 720);
        this.MaximumSize = this.Size;
        this.MinimumSize = this.Size;
        this.MaximizeBox = false;
        this.SetStyle(ControlStyles.DoubleBuffer, true);
        this.SetStyle(ControlStyles.UserPaint, true);
        this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        this._gmanager = new GameManager();
    }

    public void UpdateForm()
    {
        _gmanager.Update();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
	}
}
