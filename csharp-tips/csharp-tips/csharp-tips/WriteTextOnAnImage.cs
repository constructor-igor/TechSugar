using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using NUnit.Framework;

/*

	References:
	- http://stackoverflow.com/questions/6826921/write-text-on-an-image-in-c-sharp

*/

namespace csharp_tips
{
    [TestFixture]
    public class WriteTextOnAnImage
    {
        [TestCase("nazarov.bmp")]
        [TestCase("nazarov.png")]
        [TestCase("Dark-Glow-of-the-Mountains3.jpg")]
        [TestCase("test.tif")]
        public void WriteTextOnAnBitmap(string imageFileName)
        {
            List<TextDescriptor> text = new List<TextDescriptor>
            {
                new TextDescriptor("Hello", new PointF(10f, 10f), Brushes.Red)
            };
            WriteText(@"..\..\data\Images\" + imageFileName, text);
        }

        void WriteText(string imageFilePath, List<TextDescriptor> text)
        {
            Bitmap bitmap = (Bitmap)Image.FromFile(imageFilePath);                  //load the image file

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Font arialFont = new Font("Verdana", 14))
                {
                    foreach (TextDescriptor textDescriptor in text)
                    {
                        graphics.DrawString(textDescriptor.Text, arialFont, textDescriptor.Color, textDescriptor.Location);
                    }
                }
            }

            bitmap.Save(imageFilePath + ".target."+Path.GetExtension(imageFilePath));   //save the image file
        }
    }

    [TestFixture]
    public class WriteTextOnTifImage
    {
        [TestCase("test.tif")]
        public void WriteTextOnAnTifImage(string imageFileName)
        {
            List<TextDescriptor> text = new List<TextDescriptor>
            {
                new TextDescriptor("Hello", new PointF(10f, 10f), Brushes.Red)
            };
            WriteText(@"..\..\data\Images\" + imageFileName, text);
        }

        void WriteText(string imageFilePath, List<TextDescriptor> text)
        {
            Image originalBmp = Image.FromFile(imageFilePath);
            byte[] imgData = File.ReadAllBytes(imageFilePath);
            using (Image img = Image.FromStream(new MemoryStream(imgData)))
            using (Font drawFont = new Font("Arial", 16))
            {
                Bitmap bitmapImage = new Bitmap(new Bitmap(img)); //, originalBmp.Width, originalBmp.Height
                using (Graphics g = Graphics.FromImage(bitmapImage))
                {
                    foreach (TextDescriptor descriptor in text)
                    {
                        StringFormat drawFormat = new StringFormat {FormatFlags = StringFormatFlags.DirectionVertical};
                        g.DrawString(descriptor.Text, drawFont, descriptor.Color, descriptor.Location.X,
                            descriptor.Location.Y, drawFormat);
                    }
                }
                string targetFilePath = imageFilePath + ".target." + Path.GetExtension(imageFilePath);
                bitmapImage.Save(targetFilePath, ImageFormat.Tiff); 
            }
        }
    }

    public class TextDescriptor
    {
        public string Text;
        public PointF Location;
        public Brush Color;

        public TextDescriptor(string text, PointF location, Brush color)
        {
            Text = text;
            Location = location;
            Color = color;
        }
    }
}