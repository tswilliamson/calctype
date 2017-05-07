using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CalcTyper
{
    public class ImageHandler
    {
        private string _bitmapPath;
        private Bitmap _currentBitmap;
        private Bitmap _bitmapbeforeProcessing;

        public ImageHandler()
        {
        }

        public Bitmap CurrentBitmap
        {
            get
            {
                if (_currentBitmap == null)
                    _currentBitmap = new Bitmap(1, 1);
                return _currentBitmap;
            }
            set { _currentBitmap = value; }
        }

        public Bitmap BitmapBeforeProcessing
        {
            get { return _bitmapbeforeProcessing; }
            set { _bitmapbeforeProcessing = value; }
        }

        public string BitmapPath
        {
            get { return _bitmapPath; }
            set { _bitmapPath = value; }
        }
        
        public void SaveBitmap(string saveFilePath)
        {
            _bitmapPath = saveFilePath;
            if (System.IO.File.Exists(saveFilePath))
                System.IO.File.Delete(saveFilePath);
            _currentBitmap.Save(saveFilePath);
        }

        private double luminance(Color forColor)
        {
            return (forColor.R * 0.2126 + forColor.G * 0.7152 + forColor.B * 0.0722) / 255.0;
        }

        private static int clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        public void SubPixel(bool isVertical, bool isBGR)
        {

            Bitmap temp = (Bitmap)_currentBitmap;
            if (isVertical) {
                temp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }

            int newWidth = temp.Width / 3;
            int newHeight = temp.Height / 3;
            int maxWidth = temp.Width - 1;
            int maxHeight = temp.Height - 1;

            Bitmap bmap = new Bitmap(newWidth, newHeight, temp.PixelFormat);
            
            Color[,] colorMat = new Color[7,3];
            double[] lum = new double[7];

            for (int x = 0; x < bmap.Width; ++x)
            {
                for (int y = 0; y < bmap.Height; ++y)
                {
                    int srcX = x * 3;
                    int srcY = y * 3;

                    // determine local energy color matrix 
                    for (int sx = -2; sx < 5; sx++)
                    {
                        for (int sy = 0; sy < 3; sy++)
                        {
                            colorMat[sx+2, sy] = temp.GetPixel(clamp(srcX + sx, 0, maxWidth), clamp(srcY + sy, 0, maxHeight));
                        }
                    }

                    // average color for current pixel
                    double red = 0;
                    double green = 0;
                    double blue = 0;
                    for (int sx = 0; sx < 3; sx++)
                    {
                        for (int sy = 0; sy < 3; sy++)
                        {
                            red += colorMat[sx + 2, sy].R;
                            green += colorMat[sx + 2, sy].G;
                            blue += colorMat[sx + 2, sy].B;
                        }
                    }
                    red /= 9.0;
                    green /= 9.0;
                    blue /= 9.0;

                    // determine the luminance of each local energy column
                    for (int c = 0; c < 7; c++)
                    {
                        lum[c] = (luminance(colorMat[c,0]) + luminance(colorMat[c,1]) + luminance(colorMat[c,2])) / 3.0;
                    }
                    double avgLum = (lum[2] + lum[3] + lum[4]) / 3.0;

                    if (!isBGR)
                    {
                        red *= (lum[0] + lum[1] * 2 + lum[2] * 3 + lum[3] * 2 + lum[4]) / 9.0 / avgLum;
                        green *= (lum[1] + lum[2] * 2 + lum[3] * 3 + lum[4] * 2 + lum[5]) / 9.0 / avgLum;
                        blue *= (lum[2] + lum[3] * 2 + lum[4] * 3 + lum[5] * 2 + lum[6]) / 9.0 / avgLum;
                    }
                    else
                    {
                        red *= (lum[2] + lum[3] * 2 + lum[4] * 3 + lum[5] * 2 + lum[6]) / 9.0 / avgLum;
                        green *= (lum[1] + lum[2] * 2 + lum[3] * 3 + lum[4] * 2 + lum[5]) / 9.0 / avgLum;
                        blue *= (lum[0] + lum[1] * 2 + lum[2] * 3 + lum[3] * 2 + lum[4]) / 9.0 / avgLum;
                    }

                    bmap.SetPixel(x, y, System.Drawing.Color.FromArgb(255, (byte) red, (byte) green, (byte) blue));
                }
            }

            if (isVertical)
            {
                bmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
            _currentBitmap = (Bitmap)bmap.Clone();
        }
    }
}
