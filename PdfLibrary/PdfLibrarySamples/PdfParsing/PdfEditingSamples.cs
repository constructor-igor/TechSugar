using System;
using System.IO;
using System.Reflection;
using iTextSharp.text;
using iTextSharp.text.pdf;
using NUnit.Framework;
using Path = System.IO.Path;

namespace PdfParsing
{
    [TestFixture]
    public class PdfEditingSamples
    {
        [TestCase(@"..\..\data\Spec.pdf")]
        [TestCase(@"..\..\data\fixedWordJTS.pdf")]
        [TestCase(@"..\..\data\fixedJTS.pdf")]
        [TestCase(@"..\..\data\JTS-2016TaxpayerAnnualLocalEITReturnACCTCD.pdf")]
        [TestCase(@"..\..\data\JTS-2016TaxpayerAnnualLocalEITReturnACCTCD-2.pdf")]
        public void CheckPdf(string inputPdfFile)
        {
            inputPdfFile = Path.Combine(GetDllPath(), inputPdfFile);
            using (PdfReader pdfReader = new PdfReader(inputPdfFile))
            using (var newFileStream = new FileStream("test.pdf", FileMode.Create))
            using (var stamper = new PdfStamper(pdfReader, newFileStream)) // PdfStamper, which will create
            {
                var form = stamper.AcroFields;
                bool xfaPresent = form.Xfa.XfaPresent;

                Console.WriteLine("file: {0}, xfaPresent: {1}, Keys Count: {2}", Path.GetFileName(inputPdfFile), xfaPresent, form.Fields.Count);
            }
        }
        [Test]
        public void ChangeText()
        {
            string existingPdfFile = Path.Combine(GetDllPath(), @"..\..\data\Spec.pdf");
            string newPdfFile = Path.Combine(GetDllPath(), @"..\..\data\newSpec.pdf");
            Assert.That(File.Exists(existingPdfFile), Is.True);

            using (var newFileStream = new FileStream(newPdfFile, FileMode.Create))
            using (PdfReader pdfReader = new PdfReader(existingPdfFile))
            using (var stamper = new PdfStamper(pdfReader, newFileStream)) // PdfStamper, which will create
            {
                var form = stamper.AcroFields;
                bool xfaPresent = form.Xfa.XfaPresent;
                var fieldKeys = form.Fields.Keys;
                foreach (string fieldKey in fieldKeys)
                {
                    form.SetField(fieldKey, "REPLACED!");
                }
                // "Flatten" the form so it wont be editable/usable anymore
                stamper.FormFlattening = true;

                // You can also specify fields to be flattened, which
                // leaves the rest of the form still be editable/usable
                stamper.PartialFormFlattening("field1");
            }
        }
        [Test]
        public void AddText()
        {
            string existingPdfFile = Path.Combine(GetDllPath(), @"..\..\data\JTS-2016TaxpayerAnnualLocalEITReturnACCTCD-9.pdf");
//            string existingPdfFile = Path.Combine(GetDllPath(), @"..\..\data\fixedJTS.pdf");
            string newPdfFile = Path.Combine(GetDllPath(), @"..\..\data\newJTS-9.pdf");
            Assert.That(File.Exists(existingPdfFile), Is.True);

            using (var newFileStream = new FileStream(newPdfFile, FileMode.Create))
            using (PdfReader pdfReader = new PdfReader(existingPdfFile))
            using (var stamper = new PdfStamper(pdfReader, newFileStream)) // PdfStamper, which will create
            {
                var form = stamper.AcroFields;
                bool xfaPresent = form.Xfa.XfaPresent;
                var fieldKeys = form.Fields.Keys;
                form.SetField("LAST NAME, FIRST NAME, MIDDLE INITIAL", "Smith Joe");
                foreach (string fieldKey in fieldKeys)
                {
                    form.SetField(fieldKey, "REPLACED!");
                    form.SetFieldProperty(fieldKey, "textcolor", BaseColor.BLUE, null);
                }

                form.SetField("name", "Smith Joe");
                form.SetField("street_address", "123  Main St");
                form.SetField("city_address", "Pittsburgh");
                form.SetField("state_address", "PA");
                form.SetField("zip_address", "15235");
                form.SetField("resident_code_1", "7");
                form.SetField("resident_code_2", "0");
                form.SetField("resident_code_3", "0");
                form.SetField("resident_code_4", "1");
                form.SetField("resident_code_5", "0");
                form.SetField("resident_code_6", "1");

                string[] states = form.GetAppearanceStates("single");
                form.SetField("single", "Yes");

                form.SetField("1_1", "55,000");
                form.SetField("2_1", "0");
                form.SetField("3_1", "0");
                form.SetField("4_1", "55,000");
                form.SetField("5_1", "0");
                form.SetField("6_1", "0");
                form.SetField("7_1", "0");
                form.SetField("8_1", "55,000");
                form.SetField("9_0", "3%");
                form.SetField("9_1", "1,650");
                form.SetField("10_1", "1,650");
                form.SetField("11_1", "0");
                form.SetField("12_1", "0");
                form.SetField("13_1", "1,650");
                form.SetField("14_1", "0");
                form.SetField("15_1", "0");
                form.SetField("16_1", "0");
                form.SetField("17_1", "0");
                form.SetField("18_1", "0");
                form.SetField("19_1", "0");

                Console.WriteLine("xfaPresent: {0}", xfaPresent);
                Console.WriteLine("Keys Count: {0}", form.Fields.Count);

                // "Flatten" the form so it wont be editable/usable anymore
                stamper.FormFlattening = true;

                // You can also specify fields to be flattened, which
                // leaves the rest of the form still be editable/usable
                stamper.PartialFormFlattening("field1");
            }
        }

        [Test]
        public void FixBroken()
        {
            string existingPdfFile = Path.Combine(GetDllPath(), @"..\..\data\JTS-2016TaxpayerAnnualLocalEITReturnACCTCD.pdf");
            string newPdfFile = Path.Combine(GetDllPath(), @"..\..\data\fixedJTS.pdf");
            FixinfPdfFile(existingPdfFile, newPdfFile);
        }

        string GetDllPath()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(PdfEditingSamples));
            return Path.GetDirectoryName(assembly.Location);
        }

        void FixinfPdfFile(string existingPdfFile, string newPdfFile)
        {
            using (PdfReader pdfReader = new PdfReader(existingPdfFile))
            {
                PdfDictionary root = pdfReader.Catalog;
                PdfDictionary form = root.GetAsDict(PdfName.ACROFORM);
                PdfArray fields = form.GetAsArray(PdfName.FIELDS);

                PdfDictionary page;
                PdfArray annots;
                for (int i = 1; i <= pdfReader.NumberOfPages; i++)
                {
                    page = pdfReader.GetPageN(i);
                    annots = page.GetAsArray(PdfName.ANNOTS);
                    if (annots != null)
                    {
                        for (int j = 0; j < annots.Size; j++)
                        {
                            fields.Add(annots.GetAsIndirectObject(j));
                        }
                    }
                }

                using (var newFileStream = new FileStream(newPdfFile, FileMode.Create))
                using (var stamper = new PdfStamper(pdfReader, newFileStream)) // PdfStamper, which will create
                {

                }
            }
        }
    }
}