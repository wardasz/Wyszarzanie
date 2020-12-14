using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Biblioteka;

namespace Wyszarzanie
{
    public partial class Form1 : Form
    {
        Bitmap pic;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //wczytaj
        {
            pic = ImageProcessing.load();
            pictureBox1.Image = pic;
        }

        private void button2_Click(object sender, EventArgs e) //wyszarz synch
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            pic = ImageProcessing.GrayscaleSync(pic);
            timer.Stop();

            pictureBox1.Image = pic;
            TimeSpan time = timer.Elapsed;
            textBox1.Text = time.Seconds.ToString() + "," +  time.Milliseconds.ToString() + " seconds";

        }

        private void button5_Click(object sender, EventArgs e) //wyszarz synch, wersja zoptymalizowana
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            pic = ImageProcessing.GrayscaleSyncOpt(pic);
            timer.Stop();

            pictureBox1.Image = pic;
            TimeSpan time = timer.Elapsed;
            textBox1.Text = time.Seconds.ToString() + "," + time.Milliseconds.ToString() + " seconds";
        }

        private void button4_Click(object sender, EventArgs e) //wyszarz asynch
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            pic = ImageProcessing.GrayscaleAsync(pic).Result;
            timer.Stop();

            pictureBox1.Image = pic;
            TimeSpan time = timer.Elapsed;
            textBox1.Text = time.Seconds.ToString() + "," + time.Milliseconds.ToString() + " seconds";
        }

        private void button3_Click(object sender, EventArgs e) //zapisz
        {
            ImageProcessing.save(pic);
        }

        
    }
}
