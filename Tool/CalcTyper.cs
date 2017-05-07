/*
 * Based heavily on the ImageProcessing CodeProject tutorial: 
 * https://www.codeproject.com/Articles/33838/Image-Processing-using-C
 * 
 */
 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CalcTyper
{
    public partial class CalcTyper : Form
    {
        OpenFileDialog oDlg;
        SaveFileDialog sDlg;
        double zoomFactor = 1.0;
        private MenuItem cZoom;
        ImageHandler imageHandler = new ImageHandler();

        public CalcTyper()
        {
            InitializeComponent();
            oDlg = new OpenFileDialog(); // Open Dialog Initialization
            oDlg.RestoreDirectory = true;
            oDlg.FilterIndex = 1;
            oDlg.Filter = "Image Files (*.jpg,*.gif,*.png,*.bmp)|*.jpg;*.gif;*.png;*.bmp";
            /*************************/
            sDlg = new SaveFileDialog(); // Save Dialog Initialization
            sDlg.RestoreDirectory = true;
            sDlg.FilterIndex = 1;
            sDlg.Filter = "Image Files (*.jpg,*.gif,*.png,*.bmp)|*.jpg;*.gif;*.png;*.bmp";
            /*************************/
            cZoom = menuItemZoom100; // Current Zoom Percentage = 100%
        }

        private void ImageProcessing_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(imageHandler.CurrentBitmap, new Rectangle(this.AutoScrollPosition.X, this.AutoScrollPosition.Y, Convert.ToInt32(imageHandler.CurrentBitmap.Width * zoomFactor), Convert.ToInt32(imageHandler.CurrentBitmap.Height * zoomFactor)));
        }

        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == oDlg.ShowDialog())
            {
                imageHandler.CurrentBitmap = (Bitmap)Bitmap.FromFile(oDlg.FileName);
                imageHandler.BitmapPath = oDlg.FileName;
                this.AutoScroll = true;
                this.AutoScrollMinSize = new Size(Convert.ToInt32(imageHandler.CurrentBitmap.Width * zoomFactor), Convert.ToInt32(imageHandler.CurrentBitmap.Height * zoomFactor));
                this.Invalidate();
                //ImageInfo imgInfo = new ImageInfo(imageHandler);
                //imgInfo.Show();
            }
        }

        private void menuItemSave_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == sDlg.ShowDialog())
            {
                imageHandler.SaveBitmap(sDlg.FileName);
            }
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuItemZoom50_Click(object sender, EventArgs e)
        {
            zoomFactor = 0.5;
            cZoom.Checked = false;
            menuItemZoom50.Checked = true;
            cZoom = menuItemZoom50;
            this.AutoScrollMinSize = new Size(Convert.ToInt32(imageHandler.CurrentBitmap.Width * zoomFactor), Convert.ToInt32(imageHandler.CurrentBitmap.Height * zoomFactor));
            this.Invalidate();
        }

        private void menuItemZoom100_Click(object sender, EventArgs e)
        {
            zoomFactor = 1.0;
            cZoom.Checked = false;
            menuItemZoom100.Checked = true;
            cZoom = menuItemZoom100;
            this.AutoScrollMinSize = new Size(Convert.ToInt32(imageHandler.CurrentBitmap.Width * zoomFactor), Convert.ToInt32(imageHandler.CurrentBitmap.Height * zoomFactor));
            this.Invalidate();
        }

        private void menuItemZoom150_Click(object sender, EventArgs e)
        {
            zoomFactor = 1.5;
            cZoom.Checked = false;
            menuItemZoom150.Checked = true;
            cZoom = menuItemZoom150;
            this.AutoScrollMinSize = new Size(Convert.ToInt32(imageHandler.CurrentBitmap.Width * zoomFactor), Convert.ToInt32(imageHandler.CurrentBitmap.Height * zoomFactor));
            this.Invalidate();
        }

        private void menuItemZoom200_Click(object sender, EventArgs e)
        {
            zoomFactor = 2.0;
            cZoom.Checked = false;
            menuItemZoom200.Checked = true;
            cZoom = menuItemZoom200;
            this.AutoScrollMinSize = new Size(Convert.ToInt32(imageHandler.CurrentBitmap.Width * zoomFactor), Convert.ToInt32(imageHandler.CurrentBitmap.Height * zoomFactor));
            this.Invalidate();

        }

        private void menuItemZoom300_Click(object sender, EventArgs e)
        {
            zoomFactor = 3.0;
            cZoom.Checked = false;
            menuItemZoom300.Checked = true;
            cZoom = menuItemZoom300;
            this.AutoScrollMinSize = new Size(Convert.ToInt32(imageHandler.CurrentBitmap.Width * zoomFactor), Convert.ToInt32(imageHandler.CurrentBitmap.Height * zoomFactor));
            this.Invalidate();

        }

        private void menuItemZoom400_Click(object sender, EventArgs e)
        {
            zoomFactor = 4.0;
            cZoom.Checked = false;
            menuItemZoom400.Checked = true;
            cZoom = menuItemZoom400;
            this.AutoScrollMinSize = new Size(Convert.ToInt32(imageHandler.CurrentBitmap.Width * zoomFactor), Convert.ToInt32(imageHandler.CurrentBitmap.Height * zoomFactor));
            this.Invalidate();
        }

        private void menuItemZoom500_Click(object sender, EventArgs e)
        {
            zoomFactor = 5.0;
            cZoom.Checked = false;
            menuItemZoom500.Checked = true;
            cZoom = menuItemZoom500;
            this.AutoScrollMinSize = new Size(Convert.ToInt32(imageHandler.CurrentBitmap.Width * zoomFactor), Convert.ToInt32(imageHandler.CurrentBitmap.Height * zoomFactor));
            this.Invalidate();
        }

        private void menuItemRGBH_Click(object sender, EventArgs e)
        {
            imageHandler.SubPixel(false, false);
            this.AutoScroll = true;
            this.AutoScrollMinSize = new Size(Convert.ToInt32(imageHandler.CurrentBitmap.Width * zoomFactor), Convert.ToInt32(imageHandler.CurrentBitmap.Height * zoomFactor));
            this.Invalidate();
        }

        private void menuItemRGBV_Click(object sender, EventArgs e)
        {
            imageHandler.SubPixel(true, false);
            this.AutoScroll = true;
            this.AutoScrollMinSize = new Size(Convert.ToInt32(imageHandler.CurrentBitmap.Width * zoomFactor), Convert.ToInt32(imageHandler.CurrentBitmap.Height * zoomFactor));
            this.Invalidate();
        }
    }
}