using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace TrimGap
{
    public partial class CCDTest : Form
    {
        private static ColorPalette cp;
        private static ColorPalette cp2;
        public CCDTest()
        {
            InitializeComponent();
            Bitmap bp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed);
            Bitmap bp2 = new Bitmap(1, 1, PixelFormat.Format24bppRgb);
            cp2 = bp2.Palette;
            cp = bp.Palette;
            for (int i = 0; i < 256; i++)
            {
                cp.Entries[i] = Color.FromArgb(255, i, i, i);
            }
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Common.camera.GetImage();
            Image tmp = ImageFromRawBgraArray(Common.camera.image, Common.camera.Width, Common.camera.Height, PixelFormat.Format8bppIndexed);
            tmp.Palette = cp;
            pictureBox1.Image = tmp;

            //pictureBox1
            //Common.TrimGapAnalysis.convert(Common.camera.image, Common.camera.Width, Common.camera.Height);
            if (sram.UserAuthority == permissionEnum.ad)
            {
                Common.TrimGapAnalysis.CalculateBlueTape(Common.camera.image, Common.camera.Width, Common.camera.Height, 1, true, fram.Analysis.BlueTapeThreshold, true, out AnalysisData.resultdata_blueW[0]);
            }
        }

        public Image ImageFromRawBgraArray(byte[] arr, int width, int height, PixelFormat pixelFormat)
        {
            var output = new Bitmap(width, height, pixelFormat);
            var rect = new Rectangle(0, 0, width, height);
            var bmpData = output.LockBits(rect, ImageLockMode.ReadWrite, output.PixelFormat);

            // Row-by-row copy
            var arrRowLength = width * Image.GetPixelFormatSize(output.PixelFormat) / 8;
            var ptr = bmpData.Scan0;
            if (Image.GetPixelFormatSize(output.PixelFormat) / 8 == 3)
            {

                for (var i = 0; i < height; i++)
                {
                    //Marshal.Copy(arr, i * arrRowLength, ptr, arrRowLength);
                    for(int j = 0; j< width; j++)
                    {
                        Marshal.Copy(arr, i * arrRowLength + 3 * j + 2, ptr + 3 * j, 1);
                        Marshal.Copy(arr, i * arrRowLength + 3 * j + 1, ptr + 3 * j + 1, 1);
                        Marshal.Copy(arr, i * arrRowLength + 3 * j, ptr + 3 * j + 2, 1);
                    }
                    ptr += bmpData.Stride;
                }
            }
            else
            {
                for (var i = 0; i < height; i++)
                {
                    Marshal.Copy(arr, i * arrRowLength, ptr, arrRowLength);
                    ptr += bmpData.Stride;
                }
            }

            output.UnlockBits(bmpData);
            return output;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Common.camera2.GetImage();
            Image tmp = ImageFromRawBgraArray(Common.camera2.image, Common.camera2.Width, Common.camera2.Height, PixelFormat.Format24bppRgb);
            //tmp.Palette = cp2;
            pictureBox2.Image = tmp;

            //pictureBox1
            //Common.TrimGapAnalysis.convert(Common.camera.image, Common.camera.Width, Common.camera.Height);
            if (sram.UserAuthority == permissionEnum.ad)
            {
                Common.TrimGapAnalysis.CalculateBlueTape(Common.camera2.image, Common.camera2.Width, Common.camera2.Height, 1, true, fram.Analysis.BlueTapeThreshold, true, out AnalysisData.resultdata_blueW[0]);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Common.LightCtrlForm.Show();
        }
    }
}