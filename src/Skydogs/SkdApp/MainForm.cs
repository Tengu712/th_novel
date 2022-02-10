using System.Drawing;
using System.Windows.Forms;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp;

class MainForm : Form
{
    private readonly ICtrManagers _managers = new Managers();

    public MainForm()
    {
        this.Text = GeneralInformation.TITLE;
        this.ClientSize = new Size(GeneralInformation.WIDTH, GeneralInformation.HEIGHT);
        this.MaximumSize = this.Size;
        this.MinimumSize = this.Size;
        this.MaximizeBox = false;
        this.SetStyle(ControlStyles.DoubleBuffer, true);
        this.SetStyle(ControlStyles.UserPaint, true);
        this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    }

    public void UpdateForm()
    {
        _managers.SceneManager.Update();
        _managers.GraphicsManager.Draw();
    }
}
