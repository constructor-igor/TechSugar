using System;
using System.IO;
using System.Reflection;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using NUnit.Framework;
using Path = System.IO.Path;

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
            pdfFilePath = Path.Combine(GetDllPath(), pdfFilePath);
            Assert.That(File.Exists(pdfFilePath), Is.True);

            using (PdfReader reader = new PdfReader(pdfFilePath))
            {
                Assert.That(reader.NumberOfPages, Is.EqualTo(1));

                string text = "";
                for (int page = 1; page <= reader.NumberOfPages; page++)
                {
                    text += PdfTextExtractor.GetTextFromPage(reader, page);
                }

                Assert.That(text, Does.StartWith("Console application"));
            }
        }

        string GetDllPath()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(PdfParsingSamples));
            return Path.GetDirectoryName(assembly.Location);
        }
    }
}
