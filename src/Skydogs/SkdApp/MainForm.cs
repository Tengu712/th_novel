using System;
using System.Drawing;
using System.Windows.Forms;

namespace Skydogs.SkdApp;

class MainForm : Form
{
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
    }

    public void UpdateForm()
    {
    }

    protected override void OnPaint(PaintEventArgs e)
    {
	}
}
