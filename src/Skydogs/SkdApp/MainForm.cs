using System.Drawing;
using System.Windows.Forms;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp;

class MainForm : Form
{
    private ICtrManagers _managers = null;

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
        _managers = new Managers();
    }

    public void UpdateForm()
    {
        _managers.FpsManager.Measure();
        //_managers.SceneManager.Update();
    }
}
