using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
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

    [TestFixture]
    public class HtmlToPdf_iTextSharp
    {
        [TestCase("BookIndex")]
        public void DemoTest(string htmlFileName)
        {
            string sourceHtmlFile = String.Format(@"..\..\htmlSamples\{0}.html", htmlFileName);
            string targetPdfFile = String.Format(@"..\..\htmlSamples\{0}.itextsharp.pdf", htmlFileName);
            File.WriteAllBytes(targetPdfFile, CreatePDF(File.ReadAllText(sourceHtmlFile)).ToArray());
        }
//        [Test]
//        public void ProductionTest(string htmlFileName)
//        {
//            string sourceHtmlFile = @"<source html>";
//            string targetPdfFile = @"<target.pdf>";
//            File.WriteAllBytes(targetPdfFile, CreatePDF(File.ReadAllText(sourceHtmlFile)).ToArray());
//        }

        private MemoryStream CreatePDF(string html)
        {
            MemoryStream msOutput = new MemoryStream();
            TextReader reader = new StringReader(html);

            // step 1: creation of a document-object
            Document document = new Document(PageSize.A4, 30, 30, 30, 30);

            // step 2:
            // we create a writer that listens to the document
            // and directs a XML-stream to a file
            PdfWriter writer = PdfWriter.GetInstance(document, msOutput);

            // step 3: we create a worker parse the document
            HTMLWorker worker = new HTMLWorker(document);

            // step 4: we open document and start the worker on the document
            document.Open();
            worker.StartDocument();

            // step 5: parse the html into the document
            worker.Parse(reader);

            // step 6: close the document and the worker
            worker.EndDocument();
            worker.Close();
            document.Close();

            return msOutput;
        }
    }
}