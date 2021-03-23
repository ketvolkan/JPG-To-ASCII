using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace jpgtoascii
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyası |*.jpg;*.nef;*.png| Video|*.avi| Tüm Dosyalar |*.*";
            dosya.ShowDialog();
            string DosyaYolu = dosya.FileName;
            pictureBox1.ImageLocation = DosyaYolu;

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            cevir(); 
        }
        string[] _AsciiChars = { "▒","░","░", "@", "%", "=", "+", "*", ":", "-", "."};
        void cevir()
        {

            StringBuilder sb = new StringBuilder();
            Bitmap goruntu = new Bitmap(pictureBox1.Image,new Size(pictureBox1.Width, pictureBox1.Height));
            goruntu.RotateFlip(RotateFlipType.Rotate270FlipY);
            int s;
            for (int i = 0; i < goruntu.Width; i++)
            {
                for (int j = 0; j < goruntu.Height; j++)
                {
                    Color Pixel = goruntu.GetPixel(i, j);
                    int deger = Pixel.R + Pixel.G + Pixel.B;
                    s = deger / (765 / _AsciiChars.Length);
                    if (s!=0)
                    {
                        s = s - 1;
                    }
                    sb.Append(_AsciiChars[s]);
                }
                sb.AppendLine(" ");
            }
            dosyayaYaz(sb);
            System.Diagnostics.Process.Start(@"C:\not.txt");
            Thread.Sleep(1000);
            File.Delete(@"C:\not.txt");
        }
        private static void dosyayaYaz(StringBuilder s)
        {
            StreamWriter sw = new StreamWriter(@"C:\not.txt");        
            sw.WriteLine(s.ToString());
            sw.Flush();
            sw.Close();

        }
    }
}
