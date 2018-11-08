using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using NUnit.Framework;

/*
 *
 * https://stackoverflow.com/questions/2070365/how-to-generate-an-image-from-text-on-fly-at-runtime
 */

namespace csharp_tips
{
    [TestFixture]
    public class ImageGenerationSamples
    {
        [Test]
        public void Test()
        {
            string imageFileName = Path.GetTempFileName()+".png";
            using (Image image = DrawText("40", new Font(FontFamily.GenericSerif, 14), Color.Blue, Color.Azure))
            {
                image.Save(imageFileName, ImageFormat.Png);
            }
            File.Delete(imageFileName);
        }
        private Image DrawText(String text, Font font, Color textColor, Color backColor)
        {
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);

            drawing = Graphics.FromImage(img);

            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 0, 0);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;
        }
    }
}