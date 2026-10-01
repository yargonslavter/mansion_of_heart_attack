using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SndLib;

namespace App
{
    public partial class Form1 : Form
    {
        private SndAudio _audio;

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            SndEngine.VaiSom();

            _audio = new SndAudio();
            _audio.CarregaAiDJ(@"C:\music.mp3");
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SndEngine.DeuDeSom();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _audio.SomNaCaixaDJ();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SndEngine.ContinuaTocandoPorra();
        }
    }
}
