namespace MapEditor
{
    partial class frmEditor
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
            this.gbInfo = new System.Windows.Forms.GroupBox();
            this.chkEventos = new System.Windows.Forms.CheckBox();
            this.rbEventos = new System.Windows.Forms.RadioButton();
            this.rbCenario = new System.Windows.Forms.RadioButton();
            this.listViewCenario = new System.Windows.Forms.ListView();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCarregar = new System.Windows.Forms.Button();
            this.listViewEventos = new System.Windows.Forms.ListView();
            this.ofdAbrir = new System.Windows.Forms.OpenFileDialog();
            this.sfdGravar = new System.Windows.Forms.SaveFileDialog();
            this.fbdPasta = new System.Windows.Forms.FolderBrowserDialog();
            this.btnLimparEventos = new System.Windows.Forms.Button();
            this.gbInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbInfo
            // 
            this.gbInfo.Controls.Add(this.chkEventos);
            this.gbInfo.Controls.Add(this.rbEventos);
            this.gbInfo.Controls.Add(this.rbCenario);
            this.gbInfo.Location = new System.Drawing.Point(12, 12);
            this.gbInfo.Name = "gbInfo";
            this.gbInfo.Size = new System.Drawing.Size(134, 93);
            this.gbInfo.TabIndex = 2;
            this.gbInfo.TabStop = false;
            this.gbInfo.Text = "Info";
            // 
            // chkEventos
            // 
            this.chkEventos.AutoSize = true;
            this.chkEventos.Checked = true;
            this.chkEventos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEventos.Location = new System.Drawing.Point(6, 65);
            this.chkEventos.Name = "chkEventos";
            this.chkEventos.Size = new System.Drawing.Size(83, 17);
            this.chkEventos.TabIndex = 4;
            this.chkEventos.Text = "Mostrar Grid";
            this.chkEventos.UseVisualStyleBackColor = true;
            this.chkEventos.CheckedChanged += new System.EventHandler(this.chkEventos_CheckedChanged);
            // 
            // rbEventos
            // 
            this.rbEventos.AutoSize = true;
            this.rbEventos.Location = new System.Drawing.Point(6, 42);
            this.rbEventos.Name = "rbEventos";
            this.rbEventos.Size = new System.Drawing.Size(64, 17);
            this.rbEventos.TabIndex = 3;
            this.rbEventos.TabStop = true;
            this.rbEventos.Text = "Eventos";
            this.rbEventos.UseVisualStyleBackColor = true;
            this.rbEventos.CheckedChanged += new System.EventHandler(this.rbEventos_CheckedChanged);
            // 
            // rbCenario
            // 
            this.rbCenario.AutoSize = true;
            this.rbCenario.Location = new System.Drawing.Point(6, 19);
            this.rbCenario.Name = "rbCenario";
            this.rbCenario.Size = new System.Drawing.Size(61, 17);
            this.rbCenario.TabIndex = 2;
            this.rbCenario.TabStop = true;
            this.rbCenario.Text = "Cenario";
            this.rbCenario.UseVisualStyleBackColor = true;
            this.rbCenario.CheckedChanged += new System.EventHandler(this.rbCenario_CheckedChanged);
            // 
            // listViewCenario
            // 
            this.listViewCenario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewCenario.Location = new System.Drawing.Point(12, 122);
            this.listViewCenario.Margin = new System.Windows.Forms.Padding(0);
            this.listViewCenario.MultiSelect = false;
            this.listViewCenario.Name = "listViewCenario";
            this.listViewCenario.Size = new System.Drawing.Size(135, 431);
            this.listViewCenario.TabIndex = 3;
            this.listViewCenario.UseCompatibleStateImageBehavior = false;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSalvar.Location = new System.Drawing.Point(169, 520);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 23);
            this.btnSalvar.TabIndex = 4;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnCarregar
            // 
            this.btnCarregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCarregar.Location = new System.Drawing.Point(250, 520);
            this.btnCarregar.Name = "btnCarregar";
            this.btnCarregar.Size = new System.Drawing.Size(75, 23);
            this.btnCarregar.TabIndex = 5;
            this.btnCarregar.Text = "Carregar";
            this.btnCarregar.UseVisualStyleBackColor = true;
            this.btnCarregar.Click += new System.EventHandler(this.btnCarregar_Click);
            // 
            // listViewEventos
            // 
            this.listViewEventos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewEventos.Location = new System.Drawing.Point(12, 122);
            this.listViewEventos.Margin = new System.Windows.Forms.Padding(0);
            this.listViewEventos.MultiSelect = false;
            this.listViewEventos.Name = "listViewEventos";
            this.listViewEventos.Size = new System.Drawing.Size(135, 431);
            this.listViewEventos.TabIndex = 6;
            this.listViewEventos.UseCompatibleStateImageBehavior = false;
            this.listViewEventos.View = System.Windows.Forms.View.Tile;
            this.listViewEventos.Visible = false;
            // 
            // ofdAbrir
            // 
            this.ofdAbrir.FileName = "openFileDialog1";
            // 
            // btnLimparEventos
            // 
            this.btnLimparEventos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLimparEventos.Location = new System.Drawing.Point(360, 505);
            this.btnLimparEventos.Name = "btnLimparEventos";
            this.btnLimparEventos.Size = new System.Drawing.Size(75, 52);
            this.btnLimparEventos.TabIndex = 7;
            this.btnLimparEventos.Text = "Limpar Eventos";
            this.btnLimparEventos.UseVisualStyleBackColor = true;
            this.btnLimparEventos.Click += new System.EventHandler(this.btnLimparEventos_Click);
            // 
            // frmEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1104, 562);
            this.Controls.Add(this.btnLimparEventos);
            this.Controls.Add(this.btnCarregar);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.gbInfo);
            this.Controls.Add(this.listViewEventos);
            this.Controls.Add(this.listViewCenario);
            this.Name = "frmEditor";
            this.Text = "MHA Editor";
            this.gbInfo.ResumeLayout(false);
            this.gbInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbInfo;
        private System.Windows.Forms.RadioButton rbCenario;
        private System.Windows.Forms.CheckBox chkEventos;
        private System.Windows.Forms.RadioButton rbEventos;
        private System.Windows.Forms.ListView listViewCenario;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCarregar;
        private System.Windows.Forms.ListView listViewEventos;
        private System.Windows.Forms.OpenFileDialog ofdAbrir;
        private System.Windows.Forms.SaveFileDialog sfdGravar;
        private System.Windows.Forms.FolderBrowserDialog fbdPasta;
        private System.Windows.Forms.Button btnLimparEventos;
    }
}

