using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Vision.Motion;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private FilterInfoCollection cihaz; //Cihazları comboBox da görüntüleyen nesne.
        private VideoCaptureDevice kamera; //Bilgisayarda bulunan kamera vb. görüntü algılama cihazlarının seçileceği nesne.
        MotionDetector Hareket_Yakala;  // Görüntüde hareketi algılama fonksiyonu (Aforge)
        float Algi_Duzeyi;  //Hareketen eden cisimlerin belirleneceği ve atanacağı değişken

        private void Form1_Load(object sender, EventArgs e)
        {

            Hareket_Yakala= new MotionDetector(new TwoFramesDifferenceDetector(),new MotionBorderHighlighting());
            Algi_Duzeyi = 0;
            cihaz = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo x in cihaz)
            {
                comboBox1.Items.Add(x.Name);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kamera = new VideoCaptureDevice(cihaz[comboBox1.SelectedIndex].MonikerString);
            videoSourcePlayer1.VideoSource = kamera;
            videoSourcePlayer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            videoSourcePlayer1.SignalToStop();
        }

        private void videoSourcePlayer1_NewFrame(object sender, ref Bitmap image)
        {
            Algi_Duzeyi = Hareket_Yakala.ProcessFrame(image);
        }
    }
}
