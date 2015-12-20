using System.Collections.Generic;
using System.Drawing;
using System.IO;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class WriteTextOnAnImage
    {
        [TestCase("nazarov.bmp")]
        [TestCase("nazarov.png")]
        [TestCase("Dark-Glow-of-the-Mountains3.jpg")]
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