using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using NUnit.Framework;

namespace PdfParsing
{
    /*
     * References:
     * - http://stackoverflow.com/questions/83152/reading-pdf-documents-in-net
     * 
     * */
    [TestFixture]
    public class PdfParsingSamples
    {
        [Test]
        public void ParsePdfSample()
        {
            string pdfFilePath = @"..\..\data\Spec.pdf";
            Assert.That(File.Exists(pdfFilePath), Is.True);

            using (PdfReader reader = new PdfReader(pdfFilePath))
            {
                Assert.That(reader.NumberOfPages, Is.EqualTo(1));

                string text = "";
                for (int page = 1; page <= reader.NumberOfPages; page++)
                {
                    text += PdfTextExtractor.GetTextFromPage(reader, page);
                }

                Assert.That(text, Is.StringStarting("Console application"));
            }
        }
    }
}
