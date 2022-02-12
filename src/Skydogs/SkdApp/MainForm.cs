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
        this.MouseClick += new MouseEventHandler(ClickedMouseButton);
    }

    public void UpdateForm()
    {
        _managers.SceneManager.Update();
        _managers.GraphicsManager.Draw();
    }

    private void ClickedMouseButton(object sender, MouseEventArgs e)
    {
        switch (e.Button)
        {
            case MouseButtons.Left:
                _managers.UIManager.Clicked(true, e.X, e.Y);
                break;
            case MouseButtons.Right:
                _managers.UIManager.Clicked(false, e.X, e.Y);
                break;
            default:
                break;
        }
    }
}
