using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using flyingRats.Interfaces;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace MapEditor
{
    public partial class frmEditor : Form
    {
        #region Parametros
        //int w = 32;
        //int h = 15;
        //int d = 30;
        int w = 16;
        int h = 7;
        int d = 60;
        int width, height;
        #endregion

        LevelData LD = new LevelData();
        OpenFileDialog ofd = new OpenFileDialog() { Multiselect = false };

        ImageList imageListCenario = new ImageList();
        ListView lvCenario = new ListView();
        Panel pnlCenario;
        Bitmap bmpCenario;

        ImageList imageListEventos = new ImageList();
        ListView lvEventos = new ListView();
        Panel pnlEventos;
        Bitmap bmpEventos;

        Bitmap bmpEmpty = new Bitmap(48, 48);
        Pen blackPen = new Pen(Color.Black);

        public frmEditor()
        {
            InitializeComponent();

            //============================||============================\\
            //w = 16;
            //h = 7;
            d = 48;
            //d = 48;
            Width = 980;
            //============================||============================\\

            width = w * d;
            height = h * d;

            LD.Altura = h;
            LD.Largura = w;
            LD.TileSize = d;

            LD.MatrixTiles = new Int32[w, h];
            LD.MatrixEventos = new Int32[w, h];

            LoadTiles();

            int lvw = d + 86;
            listViewCenario.Width = lvw;//176
            listViewEventos.Width = lvw;

            int delta = listViewCenario.Left + listViewCenario.Width + 10;
            Rectangle rect = new Rectangle(delta, 20, w * d + 1, h * d + 1);
            pnlCenario = new Panel() { Bounds = rect };//Location = rect.Location, Size = rect.Size, 
            pnlEventos = new Panel() { Bounds = rect, Visible = false };
            bmpCenario = new Bitmap(width, height);
            bmpEventos = new Bitmap(width, height);
            //using (bmpEventos.graphics)
            using (Graphics G = Graphics.FromImage(bmpEventos))
            {
                var brush = new System.Drawing.SolidBrush(Color.FromArgb(0, 255, 255, 255));
                //Color.FromArgb(255, 255, 0, 0
                //G.FillRectangle(, rect);
                G.FillRectangle(brush, rect);
            }
            //populatePanels();
            this.Controls.Add(pnlCenario);
            this.Controls.Add(pnlEventos);
            pnlCenario.Paint += new PaintEventHandler(pnlCenario_Paint);
            pnlEventos.Paint += new PaintEventHandler(pnlEventos_Paint);
            //pnlCenario.MouseClick += new MouseEventHandler(pnlCenario_MouseClick);
            //pnlEventos.MouseClick += new MouseEventHandler(pnlEventos_MouseClick);
            pnlCenario.MouseClick += new MouseEventHandler(pnl_MouseClick);
            pnlEventos.MouseClick += new MouseEventHandler(pnl_MouseClick);

            //pnlCenario.BorderStyle = BorderStyle.FixedSingle;
            pnlCenario.SendToBack();
            this.Width = pnlCenario.Left + pnlCenario.Width + 26;

            var cinza = Color.FromArgb(255, 160, 160, 160);
            this.BackColor = cinza;
            foreach (Control ctrl in this.Controls)
            {
                ctrl.BackColor = cinza;
            }
        }

        private void LoadTiles()
        {
            string pathLocal = Path.GetDirectoryName(Application.ExecutablePath);
            try
            {
                string pathMaps = Path.Combine(pathLocal, "Maps");
                imageListCenario = LoadTiles(pathMaps, listViewCenario);
                listViewCenario.LargeImageList = imageListCenario;
                List<string> lista = Directory.GetFiles(pathMaps).OrderBy(f => f).ToList<string>();

                LD.NomesTiles.Clear();
                foreach(string s in lista)
                {
                    LD.NomesTiles.Add(Path.GetFileName(s));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro carregando os tiles.");
            }


            try
            {
                string pathEventos = Path.Combine(pathLocal, "Path");
                imageListEventos = LoadTiles(pathEventos, listViewEventos);
                listViewEventos.LargeImageList = imageListEventos;
                List<string> lista = Directory.GetFiles(pathEventos).OrderBy(f => f).ToList<string>();

                LD.NomesEventos.Clear();
                foreach (string s in lista)
                {
                    LD.NomesEventos.Add(Path.GetFileName(s));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro carregando os tiles.");
            }
        }

        private ImageList LoadTiles(string imgPath, ListView listView)
        {

            if (!Directory.Exists(imgPath))
            {
                if (fbdPasta.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imgPath = fbdPasta.SelectedPath;
                }
            }

            ImageList imgList = new ImageList();
            imgList.ImageSize = new System.Drawing.Size(d, d);
            var files = Directory.EnumerateFiles(imgPath).OrderBy(f => f);

            //listView1.Items.Add(
            int counter = 0;
            foreach (var file in files)
            {
                FileInfo fi = new FileInfo(file);
                //LD.NomesTiles.Add(fi.Name);
                var img = Image.FromFile(file);
                imgList.Images.Add(img);
                var lvi = new ListViewItem(fi.Name.Replace(fi.Extension, ""), counter++);
                listView.Items.Add(lvi);
            }
            return imgList;
        }

        void pnlEventos_Paint(object sender, PaintEventArgs e)
        {
            //if (!chkEventos.Visible) return;
            //if (!pnlEventos.Visible) return;
            //e.Graphics.DrawImage(bmpEventos, Point.Empty);

            ImageAttributes attr = new ImageAttributes();

            // Set the transparency color key based on the upper-left pixel 
            // of the image.
            // Uncomment the following line to make all black pixels transparent:
            //attr.SetColorKey(Color.White, Color.White);
            //e.Graphics.DrawImage(bmpEventos, Point.Empty);

            //e.Graphics.DrawImage(bmpEventos, e.ClipRectangle,
            //    e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width, e.ClipRectangle.Height,
            //    GraphicsUnit.Pixel, attr);

            e.Graphics.DrawImage(bmpCenario, Point.Empty);
            e.Graphics.DrawImage(bmpEventos, Point.Empty);
            //e.Graphics.DrawImage(bmpEventos, e.ClipRectangle);
            if (chkEventos.Checked)
            {
                for (int i = 0; i <= w; i++)
                {
                    e.Graphics.DrawLine(blackPen, new Point(i * d, 0), new Point(i * d, height));
                }
                for (int i = 0; i <= h; i++)
                {
                    e.Graphics.DrawLine(blackPen, new Point(0, i * d), new Point(width, i * d));
                }
            }
        }

        void pnlCenario_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(bmpCenario, Point.Empty);
            //e.Graphics.DrawImage(bmpEventos, Point.Empty);

            //e.Graphics.DrawImage(bmpEventos, e.ClipRectangle,
            //    e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width, e.ClipRectangle.Height,
            //    GraphicsUnit.Pixel);

            //e.Graphics.DrawImage(bmpCenario, e.ClipRectangle, );
            if (chkEventos.Checked)
            {
                for (int i = 0; i <= w; i++)
                {
                    e.Graphics.DrawLine(blackPen, new Point(i * d, 0), new Point(i * d, height));
                }
                for (int i = 0; i <= h; i++)
                {
                    e.Graphics.DrawLine(blackPen, new Point(0, i * d), new Point(width, i * d));
                }
            }
        }

        //private void populatePanels()
        //{
        //    int qtdTiles = w * d;
        //    Padding pad = new Padding(0);
        //    var img = Image.FromFile(@"C:\Users\Rodrigo\Dropbox\GGJ2013\FlyingRats\Tiles\Basico.png");
        //    for (int i = 0; i < qtdTiles; i++)
        //    {
        //        //var pbx = new PictureBox() { Width = w, Height = h, BackColor = Color.Black };
        //        var pbx = new PictureBox() { Width = w, Height = h };
        //        pbx.Image = img;
        //        //pbx.Margin.All = 0;
        //        pbx.Margin = pad;
        //        pnlCenario.Controls.Add(pbx);
        //    }
        //}

        //void c_Paint(object sender, PaintEventArgs e)
        //{
        //    Control ctrl = sender as Control;
        //    e.Graphics.DrawRectangle(new Pen(Color.Black), 0, 0, ctrl.Width, ctrl.Height);
        //}

        private void pnlCenario_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewItem lvi = null;
            if (listViewCenario.SelectedItems.Count > 0)
                lvi = listViewCenario.SelectedItems[0];
            if (lvi == null) return;

            int localX = (e.X / d);
            int localY = (e.Y / d);
            int x = localX * d;
            int y = localY * d;

            LD.MatrixTiles[localX, localY] = listViewCenario.SelectedIndices[0] + 1;

            Point p = new Point(x, y);
            Rectangle rect = new Rectangle(p, new Size(d, d));
            var img = lvi.ImageList.Images[lvi.ImageIndex];
            using (Graphics bufferGrph = Graphics.FromImage(bmpCenario))
            {
                //bufferGrph.DrawImageUnscaled(buffer, Point.Empty);
                bufferGrph.DrawImage(img, p);
            }
            //pnlCenario.Refresh();
            pnlCenario.Invalidate(rect);
        }
        private void pnlEventos_MouseClick(object sender, MouseEventArgs e)
        {
            int localX = (e.X / d);
            int localY = (e.Y / d);
            int x = localX * d;
            int y = localY * d;
            Point p = new Point(x, y);
            Rectangle rect = new Rectangle(p, new Size(d, d));

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                LD.MatrixEventos[localX, localY] = 0;

                var newBmp = new Bitmap(width, height);
                using (Graphics newGraph = Graphics.FromImage(newBmp))
                {
                    //bufferGrph.DrawImage(img, p);
                    //bufferGrph.Clear(Color.FromArgb(0, 0, 0, 0));
                    //bufferGrph.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 0, 0)), rect);
                    //bufferGrph.DrawImage(bmpEmpty, p);

                    x = 0;
                    y = 0;
                    foreach (var item in LD.MatrixEventos)
                    {
                        if (item != 0)
                        {
                            p = new Point(x * d, y * d);
                            var img2 = imageListEventos.Images[item - 1];
                            newGraph.DrawImage(img2, p);
                        }
                        y++;
                        if (y == LD.Altura)
                        {
                            x++;
                            y = 0;
                        }
                    }
                }
                bmpEventos = newBmp;
                //using (Graphics bufferGrph = Graphics.FromImage(bmpEventos))
                //{
                //    bufferGrph.DrawImage(newBmp, Point.Empty);
                //}
                pnlEventos.Invalidate(rect);
                return;
            }
            ListViewItem lvi = null;
            if (listViewEventos.SelectedItems.Count > 0)
                lvi = listViewEventos.SelectedItems[0];
            if (lvi == null) return;


            LD.MatrixEventos[localX, localY] = listViewEventos.SelectedIndices[0] + 1;

            var img = lvi.ImageList.Images[lvi.ImageIndex];
            using (Graphics bufferGrph = Graphics.FromImage(bmpEventos))
            {
                //bufferGrph.DrawImageUnscaled(buffer, Point.Empty);
                bufferGrph.DrawImage(img, p);
            }
            //pnlEventos.Refresh();
            pnlEventos.Invalidate(rect);
        }

        private void pnl_MouseClick(object sender, MouseEventArgs e)
        {
            int localX = (e.X / d);
            int localY = (e.Y / d);
            int x = localX * d;
            int y = localY * d;
            Point p = new Point(x, y);
            Rectangle rect = new Rectangle(p, new Size(d, d));

            int[,] matrix = null;
            ImageList localImageList = null;
            ListView localListView = null;
            Bitmap localBmp = null;
            Panel localPnl = null;
            bool isEventos = false;
            if (sender == pnlCenario)
            {
                matrix = LD.MatrixTiles;
                localImageList = imageListCenario;
                localListView = listViewCenario;
                localBmp = bmpCenario;
                localPnl = pnlCenario;
                isEventos = false;
            }
            else if (sender == pnlEventos)
            {
                matrix = LD.MatrixEventos;
                localImageList = imageListEventos;
                localListView = listViewEventos;
                localBmp = bmpEventos;
                localPnl = pnlEventos;
                isEventos = true;
            }
            else //if (matrix == null)
            {
                MessageBox.Show("Erro genérico de banco de dados");
                this.Enabled = false;
                Application.Exit();
                return;
            }

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                matrix[localX, localY] = 0;

                var newBmp = new Bitmap(width, height);
                using (Graphics newGraph = Graphics.FromImage(newBmp))
                {
                    x = 0;
                    y = 0;
                    foreach (var item in matrix)
                    {
                        if (item != 0)
                        {
                            p = new Point(x * d, y * d);
                            var img2 = localImageList.Images[item - 1];
                            newGraph.DrawImage(img2, p);
                        }
                        y++;
                        if (y == LD.Altura)
                        {
                            x++;
                            y = 0;
                        }
                    }
                }

                if (isEventos)
                {
                    bmpEventos = newBmp;
                }
                else
                {
                    bmpCenario = newBmp;
                }
                localPnl.Invalidate(rect);
                return;
            }
            
            ListViewItem lvi = null;
            if (localListView.SelectedItems.Count > 0)
                lvi = localListView.SelectedItems[0];
            if (lvi == null) return;


            matrix[localX, localY] = localListView.SelectedIndices[0] + 1;

            var img = lvi.ImageList.Images[lvi.ImageIndex];
            using (Graphics bufferGrph = Graphics.FromImage(localBmp))
            {
                bufferGrph.DrawImage(img, p);
            }
            //pnlEventos.Refresh();
            localPnl.Invalidate(rect);
        }

        private void rbCenario_CheckedChanged(object sender, EventArgs e)
        {
            //pnlCenario.Refresh();
            //if (rbCenario.Checked)
            //{
            //    //listView1.Items.Clear();
            //    listViewCenario.LargeImageList = imageListCenario;
            //}
        }

        private void rbEventos_CheckedChanged(object sender, EventArgs e)
        {
            pnlEventos.Visible = rbEventos.Checked;
            listViewEventos.Visible = rbEventos.Checked;
            //pnlEventos.Visible = true;
            //listViewEventos.Visible = true;
            //if (rbEventos.Checked)
            //{
            //    //listView1.Items.Clear();
            //    listViewCenario.LargeImageList = imageListEventos;
            //}
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            //if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            if (sfdGravar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //LD.SalvarDataToFile("teste.txt");
                if (string.IsNullOrEmpty(sfdGravar.FileName))
                {
                    MessageBox.Show("Nao pode salvar arquivo com nome vazio.");
                    return;
                }
                LD.SalvarDataToFile(sfdGravar.FileName);
            }
        }

        private void btnCarregar_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileInfo fi = new FileInfo(ofd.FileName);
                LD.CarregarDataFromFile(ofd.FileName);
                this.Text = String.Format("MHA Editor {0}", fi.Name.Replace(fi.Extension, ""));
                LoadLevel(LD);
            }
        }

        private void LoadLevel(LevelData LD)
        {
            h = LD.Altura;
            w = LD.Largura;
            d = LD.TileSize;
            width = w * d;
            height = h * d;

            bmpCenario = new Bitmap(width, height);
            bmpEventos = new Bitmap(width, height);
            //LD.MatrixTiles = new Int32[w, h];
            //LD.MatrixEventos = new Int32[w, h];

            //foreach (var item in LD.MatrixTiles) { }
            int x = 0,
                y = 0;
            using (Graphics bufferGrph = Graphics.FromImage(bmpCenario))
            {
                foreach (var item in LD.MatrixTiles)
                {
                    if (item != 0)
                    {
                        //pintar item na Position x,y
                        Point p = new Point(x * d, y * d);
                        Rectangle rect = new Rectangle(p, new Size(d, d));
                        var img = imageListCenario.Images[item - 1];
                        bufferGrph.DrawImage(img, p);
                    }
                    y++;
                    if (y == LD.Altura)
                    {
                        x++;
                        y = 0;
                    }
                }
            }
            pnlCenario.Invalidate();

            x = 0;
            y = 0;
            using (Graphics bufferGrph = Graphics.FromImage(bmpEventos))
            {
                foreach (var item in LD.MatrixEventos)
                {
                    if (item != 0)
                    {
                        //pintar item na Position x,y
                        Point p = new Point(x * d, y * d);
                        Rectangle rect = new Rectangle(p, new Size(d, d));
                        var img = imageListEventos.Images[item - 1];
                        bufferGrph.DrawImage(img, p);
                    }
                    y++;
                    if (y == LD.Altura)
                    {
                        x++;
                        y = 0;
                    }
                }
            }
            rbCenario.Checked = true;
        }

        private void btnLimparEventos_Click(object sender, EventArgs e)
        {
            LD.MatrixEventos = new Int32[LD.Largura, LD.Altura];
            bmpEventos = new Bitmap(width, height);
            pnlEventos.Invalidate();
        }

        private void chkEventos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCenario.Checked)
            {
                pnlCenario.Refresh();
            }
            else
            {
                pnlEventos.Refresh();
            }
        }
    }
}
