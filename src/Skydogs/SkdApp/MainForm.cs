using System.Drawing;
using System.Windows.Forms;
using Skydogs.SkdApp.Manager;

namespace Skydogs.SkdApp;

class MainForm : Form
{
    private readonly ICtrManagers _managers;

    public MainForm(ICtrManagers managers)
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
        this.KeyDown += new KeyEventHandler(DownedKey);
        this.KeyUp += new KeyEventHandler(UppedKey);
        this._managers = managers;
    }

    private void ClickedMouseButton(object sender, MouseEventArgs e)
    {
        _managers.EventManager.Clicked(e);
    }

    private void DownedKey(object sender, KeyEventArgs e)
    {
        _managers.EventManager.DownedKey(e);
    }

    private void UppedKey(object sender, KeyEventArgs e)
    {
        _managers.EventManager.UppedKey(e);
    }
}
