using System;
using System.Windows.Forms;

namespace Aorus.X570.Pro.RgbController
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                panelCurrentColor.BackColor = colorDialog.Color;
                Motherboard.Instance.SetLedColor(colorDialog.Color);
                Motherboard.Instance.SetMemoryLedColor(colorDialog.Color);
            }
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
            }
        }

        public void ShowSystemTray() => notifyIcon.Visible = true;

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }
    }
}
