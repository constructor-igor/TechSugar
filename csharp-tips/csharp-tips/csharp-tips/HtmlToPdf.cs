using System;
using System.IO;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class HtmlToPdf
    {
        [Test]
        public void Test()
        {
            string sourceHtmlFile = @"..\..\htmlSamples\BookIndex.html";
            string targetPdfFile = @"..\..\htmlSamples\BookIndex.pdf";
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