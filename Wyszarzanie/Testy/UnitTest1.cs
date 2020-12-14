using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace Testy
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGrayscaleSync()
        {
            Bitmap before = new Bitmap(2, 2);
            Bitmap after;
            Bitmap expected = new Bitmap(2, 2);

            before.SetPixel(0, 0, Color.FromArgb(100, 100, 100));
            before.SetPixel(0, 1, Color.FromArgb(200, 50, 100));
            before.SetPixel(1, 0, Color.FromArgb(10, 50, 30));
            before.SetPixel(1, 1, Color.FromArgb(0, 0, 0));

            expected.SetPixel(0, 0, Color.FromArgb(100, 100, 100));
            expected.SetPixel(0, 1, Color.FromArgb(85, 85, 85));
            expected.SetPixel(1, 0, Color.FromArgb(40, 40, 40));
            expected.SetPixel(1, 1, Color.FromArgb(0, 0, 0));

            after = Biblioteka.ImageProcessing.GrayscaleSync(before);

            Assert.IsTrue(ImageCompare(after, expected));
        }

        [TestMethod]
        public void TestGrayscaleAsync()
        {
            Bitmap before = new Bitmap(2, 2);
            Bitmap after;
            Bitmap expected = new Bitmap(2, 2);

            before.SetPixel(0, 0, Color.FromArgb(100, 100, 100));
            before.SetPixel(0, 1, Color.FromArgb(200, 50, 100));
            before.SetPixel(1, 0, Color.FromArgb(10, 50, 30));
            before.SetPixel(1, 1, Color.FromArgb(0, 0, 0));

            expected.SetPixel(0, 0, Color.FromArgb(100, 100, 100));
            expected.SetPixel(0, 1, Color.FromArgb(85, 85, 85));
            expected.SetPixel(1, 0, Color.FromArgb(40, 40, 40));
            expected.SetPixel(1, 1, Color.FromArgb(0, 0, 0));

            after = Biblioteka.ImageProcessing.GrayscaleAsync(before).Result;

            Assert.IsTrue(ImageCompare(after, expected));
        }
        
        [TestMethod]
        public void TestGrayscaleSyncOpt()
        {
            Bitmap before = new Bitmap(2, 2);
            Bitmap after;
            Bitmap expected = new Bitmap(2, 2);

            before.SetPixel(0, 0, Color.FromArgb(100, 100, 100));
            before.SetPixel(0, 1, Color.FromArgb(200, 50, 100));
            before.SetPixel(1, 0, Color.FromArgb(10, 50, 30));
            before.SetPixel(1, 1, Color.FromArgb(0, 0, 0));

            expected.SetPixel(0, 0, Color.FromArgb(100, 100, 100));
            expected.SetPixel(0, 1, Color.FromArgb(85, 85, 85));
            expected.SetPixel(1, 0, Color.FromArgb(40, 40, 40));
            expected.SetPixel(1, 1, Color.FromArgb(0, 0, 0));

            after = Biblioteka.ImageProcessing.GrayscaleSyncOpt(before);

            Assert.IsTrue(ImageCompare(after, expected));
        }


        public bool ImageCompare(Bitmap a, Bitmap b)
        {
            if (a.Width != b.Width) return false;
            if (a.Height != b.Height) return false;
            for (int y = 0; y < a.Height; y++)
            {
                for (int x = 0; x < a.Width; x++)
                {
                    Color colA = a.GetPixel(x, y);
                    Color colB = b.GetPixel(x, y);
                    if (colA.R != colB.R) return false;
                    if (colA.G != colB.G) return false;
                    if (colA.B != colB.B) return false;
                }
            }
            return true;
        }
    }
}
