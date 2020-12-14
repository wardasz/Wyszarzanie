using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace Biblioteka
{
    public class ImageProcessing
    {
              
        public static Bitmap load()
        {
            OpenFileDialog opener = new OpenFileDialog();
            opener.Filter = "Image files (*.jpg, *.jpeg, *.bmp, *.png) | *.jpg; *.jpeg; *.bmp; *.png";
            opener.InitialDirectory = "D:\\";
            opener.RestoreDirectory = true;
            Bitmap pic;

            if (opener.ShowDialog() == DialogResult.OK)
            {
                pic = new Bitmap(opener.FileName);
                return pic;
            }
            return null;
        }
        
        public static Bitmap GrayscaleSync(Bitmap pic)
        {
            int height = pic.Height;
            int width = pic.Width;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color old = pic.GetPixel(x, y);
                    int gray = (byte)((old.R * 0.21) + (old.G * 0.72) + (old.B * 0.07));
                    Color nev = Color.FromArgb(gray, gray, gray);
                    pic.SetPixel(x, y, nev);
                }
            }
            return pic;
        }

        public static unsafe Bitmap GrayscaleSyncOpt(Bitmap pic)
        {
            int height = pic.Height;
            int width = pic.Width;

            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData pic2 = pic.LockBits(rect, ImageLockMode.ReadWrite, pic.PixelFormat);

            int pixelSize = 3;
            byte* current = (byte*)(void*)pic2.Scan0;
            int nWidth = pic2.Width * pixelSize;
            int nHeight = pic2.Height;

            for (int y = 0; y < nHeight; y++)
            {
                for (int x = 0; x < nWidth; x++)
                {
                    if (x % pixelSize == 0 || x == 0)
                    {
                        byte gray = (byte)((current[2] * 0.21) + (current[1] * 0.72) + (current[0] * 0.07));
                        current[0] = gray;
                        current[1] = gray;
                        current[2] = gray;
                    }
                    current++;
                }
            }
            pic.UnlockBits(pic2);
            return pic;
        }

        public static async Task<Bitmap> GrayscaleAsync(Bitmap pic)
        {
            int height = pic.Height;
            int width = pic.Width;
            List<Task<Bitmap>> tasks = new List<Task<Bitmap>>();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    tasks.Add(GrayPoint(pic, x, y));
                }
            }
            await Task.WhenAll<Bitmap>(tasks);
            return pic;
        }

        public static Task<Bitmap> GrayPoint(Bitmap pic, int x, int y)
        {
            Color old = pic.GetPixel(x, y);
            int gray = (byte)((old.R * 0.21) + (old.G * 0.72) + (old.B * 0.07));
            Color nev = Color.FromArgb(gray, gray, gray);
            pic.SetPixel(x, y, nev);
            return Task.FromResult(pic);
        }

        public static void save(Bitmap pic)
        {
            SaveFileDialog saver = new SaveFileDialog();
            saver.Filter = "Image files (*.jpg, *.jpeg, *.bmp, *.png) | *.jpg; *.jpeg; *.bmp; *.png";
            saver.InitialDirectory = "D:\\";
            saver.RestoreDirectory = true;
            ImageFormat format = ImageFormat.Png;
            if (saver.ShowDialog() == DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(saver.FileName);
                switch (ext)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                }
                pic.Save(saver.FileName, format);
            }
        }
    }
}
