using System;
using System.IO;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class HtmlToPdf
    {
        [TestCase("BookIndex")]
        public void DemoTest(string htmlFileName)
        {
            string sourceHtmlFile = String.Format(@"..\..\htmlSamples\{0}.html", htmlFileName);
            string targetPdfFile = String.Format(@"..\..\htmlSamples\{0}.pdf", htmlFileName);
            File.WriteAllBytes(targetPdfFile, PdfSharpConvert(File.ReadAllText(sourceHtmlFile)));
        }
        public static Byte[] PdfSharpConvert(String html)
        {
            Byte[] res = null;
            using (MemoryStream ms = new MemoryStream())
            {
                var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
                pdf.Save(ms);
                res = ms.ToArray();
            }
            return res;
        }
    }
}