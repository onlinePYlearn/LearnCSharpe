using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace learnImages
{
    public partial class Form1 : Form
    {
        Bitmap image1;
        public Form1()
        {
            InitializeComponent();
        }
    
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve the image.
                image1 = new Bitmap(@"E:\data\CSharpe\LearnCSharpe\learnImages\"+
                    @"learnImages\sample\violet.jpg", true);

                int x, y;

                // Loop throught the images pixels to reset color.
                for (x=0; x<image1.Width; x++)
                {
                    for (y=0; y<image1.Height; y++)
                    {
                        Color pixelColor = image1.GetPixel(x, y);
                        Color newColor = Color.FromArgb(pixelColor.R, 0, 0);
                        image1.SetPixel(x, y, newColor);
                    }
                }

                // Set the PictureBox to display the image.
                pictureBox1.Image = image1;

                // Display the pixel format in Label1.
                label1.Text = "Pixel format: " + image1.PixelFormat.ToString();

            }
            catch(ArgumentException)
            {
                MessageBox.Show("There was an error." +
                    "Check the path to the image file.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (image1 != null)
            {
                string fname = @"E:\data\CSharpe\LearnCSharpe\learnImages\" +
                    @"learnImages\sample\violet_result.jpg";
                image1.Save(fname, ImageFormat.Jpeg);
            }
        }
    }
}
