using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace flyingRats.RunSetup
{
	public partial class FrmSetup : Form
	{
		public FrmSetup()
		{
			InitializeComponent();
		}

		public RunParameters Execute()
		{
			if (this.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				int w, h;
				if (rdb1280x720.Checked)
				{
                    w = 1280; h = 720;
                }
                else if (rdb1920x1080.Checked)
				{
                    w = 1920; h = 1080;
                }
                else
				{
                    w = 960; h = 540;
                }

                return new RunParameters(w, h, chkFullscreen.Checked, Application.ProductVersion);
			}
			return null;
		}

        private void FrmSetup_Load(object sender, EventArgs e)
        {
            lblVersion.Text = string.Format("Version: {0}", Application.ProductVersion);

            pbxBanner.Image = Image.FromFile("banner.png");
        }
	}
}
