namespace flyingRats.RunSetup
{
	partial class FrmSetup
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSetup));
            this.chkFullscreen = new System.Windows.Forms.CheckBox();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdb960x540 = new System.Windows.Forms.RadioButton();
            this.rdb1280x720 = new System.Windows.Forms.RadioButton();
            this.rdb1920x1080 = new System.Windows.Forms.RadioButton();
            this.pbxBanner = new System.Windows.Forms.PictureBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // chkFullscreen
            // 
            this.chkFullscreen.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.chkFullscreen.AutoSize = true;
            this.chkFullscreen.Location = new System.Drawing.Point(15, 297);
            this.chkFullscreen.Name = "chkFullscreen";
            this.chkFullscreen.Size = new System.Drawing.Size(74, 17);
            this.chkFullscreen.TabIndex = 2;
            this.chkFullscreen.Text = "Fullscreen";
            this.chkFullscreen.UseVisualStyleBackColor = true;
            // 
            // btnPlay
            // 
            this.btnPlay.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnPlay.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnPlay.Location = new System.Drawing.Point(12, 324);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(103, 40);
            this.btnPlay.TabIndex = 4;
            this.btnPlay.Text = "PLAY!";
            this.btnPlay.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(462, 324);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 40);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(15, 317);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(547, 2);
            this.label1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.groupBox1.Controls.Add(this.rdb960x540);
            this.groupBox1.Controls.Add(this.rdb1280x720);
            this.groupBox1.Controls.Add(this.rdb1920x1080);
            this.groupBox1.Location = new System.Drawing.Point(15, 201);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(547, 90);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select the resolution";
            // 
            // rdb960x540
            // 
            this.rdb960x540.AutoSize = true;
            this.rdb960x540.Location = new System.Drawing.Point(6, 65);
            this.rdb960x540.Name = "rdb960x540";
            this.rdb960x540.Size = new System.Drawing.Size(66, 17);
            this.rdb960x540.TabIndex = 2;
            this.rdb960x540.Text = "960x540";
            this.rdb960x540.UseVisualStyleBackColor = true;
            // 
            // rdb1280x720
            // 
            this.rdb1280x720.AutoSize = true;
            this.rdb1280x720.Location = new System.Drawing.Point(6, 42);
            this.rdb1280x720.Name = "rdb1280x720";
            this.rdb1280x720.Size = new System.Drawing.Size(72, 17);
            this.rdb1280x720.TabIndex = 1;
            this.rdb1280x720.Text = "1280x720";
            this.rdb1280x720.UseVisualStyleBackColor = true;
            // 
            // rdb1920x1080
            // 
            this.rdb1920x1080.AutoSize = true;
            this.rdb1920x1080.Checked = true;
            this.rdb1920x1080.Location = new System.Drawing.Point(6, 19);
            this.rdb1920x1080.Name = "rdb1920x1080";
            this.rdb1920x1080.Size = new System.Drawing.Size(78, 17);
            this.rdb1920x1080.TabIndex = 0;
            this.rdb1920x1080.TabStop = true;
            this.rdb1920x1080.Text = "1920x1080";
            this.rdb1920x1080.UseVisualStyleBackColor = true;
            // 
            // pbxBanner
            // 
            this.pbxBanner.Location = new System.Drawing.Point(12, 12);
            this.pbxBanner.Name = "pbxBanner";
            this.pbxBanner.Size = new System.Drawing.Size(550, 183);
            this.pbxBanner.TabIndex = 7;
            this.pbxBanner.TabStop = false;
            // 
            // lblVersion
            // 
            this.lblVersion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblVersion.Location = new System.Drawing.Point(0, 354);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(574, 22);
            this.lblVersion.TabIndex = 8;
            this.lblVersion.Text = "Version: 1.3";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmSetup
            // 
            this.AcceptButton = this.btnPlay;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(574, 376);
            this.Controls.Add(this.pbxBanner);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.chkFullscreen);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.lblVersion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Run setup";
            this.Load += new System.EventHandler(this.FrmSetup_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxBanner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.CheckBox chkFullscreen;
		private System.Windows.Forms.Button btnPlay;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdb960x540;
        private System.Windows.Forms.RadioButton rdb1280x720;
        private System.Windows.Forms.RadioButton rdb1920x1080;
        private System.Windows.Forms.PictureBox pbxBanner;
        private System.Windows.Forms.Label lblVersion;
	}
}