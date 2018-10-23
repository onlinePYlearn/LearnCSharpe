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

        private void button3_Click(object sender, EventArgs e)
        {

        }

        public static Bitmap RGB2Gray(Bitmap srcBitmap)
        {
            int wide = srcBitmap.Width;
            int height = srcBitmap.Height;
            Rectangle rect = new Rectangle(0, 0, wide, height);

            // 將srcBitmap鎖定到系統內的記憶體的某個區塊中，並將這個結果交給BitmapData類別的srcBimap
            BitmapData srcBmData = srcBitmap.LockBits(rect, ImageLockMode.ReadWrite,
                    PixelFormat.Format24bppRgb);

            // 將CreateGrayscaleImage灰階影像，並將這個結果交給Bitmap類別的dstBimap
            Bitmap dstBitmap = CreateGrayscaleImage(wide, height);

            // 將dstBitmap鎖定到系統內的記憶體的某個區塊中，並將這個結果交給BitmapData類別的dstBimap
            BitmapData dstBmData = dstBitmap.LockBits(rect, ImageLockMode.ReadWrite,
                    PixelFormat.Format8bppIndexed);

            // 位元圖中第一個像素數據的地址。它也可以看成是位圖中的第一個掃瞄行
            // 目的是設兩個起始旗標srcPtr、dstPtr，為srcBmData、dstBmData的掃瞄行的開始位置
            System.IntPtr srcPtr = srcBmData.Scan0;
            System.IntPtr dstPtr = dstBmData.Scan0;

            // 將Bitmap對象的訊息存放到byte中
            int src_bytes = srcBmData.Stride * height;
            byte[] srcValues = new byte[src_bytes];
            int dst_bytes = dstBmData.Stride * height;
            byte[] dstValues = new byte[dst_bytes];

            // 複製GRB信息到byte中
            System.Runtime.InteropServices.Marshal.Copy(srcPtr, srcValues, 0, src_bytes);
            System.Runtime.InteropServices.Marshal.Copy(dstPtr, dstValues, 0, dst_bytes);

            // 根據Y=0.299*R+0.114*G+0.587B,Y為亮度
            for (int i = 0; i < height; i++)
            for (int j = 0; j < wide; j++)
                {
                    //只處理每行中圖像像素數據,捨棄未用空間
                    //注意位圖結構中RGB按BGR的順序存儲
                    int k = 3 * j;
                    byte temp = (byte)
                    (srcValues​​[i * srcBmData.Stride + k + 2] * .299 +
                     srcValues​​[i * srcBmData.Stride + k + 1] * .587 +
                     srcValues​​[i * srcBmData.Stride + k] * .114);
                     dstValues​​[i * dstBmData.Stride + j] = temp;
                }
            System.Runtime.InteropServices.Marshal.Copy(dstValues​​, 0, dstPtr, dst_bytes);

            //解鎖位圖
            srcBitmap.UnlockBits(srcBmData);
            dstBitmap.UnlockBits(dstBmData);
            return dstBitmap;















        }
    }
}
