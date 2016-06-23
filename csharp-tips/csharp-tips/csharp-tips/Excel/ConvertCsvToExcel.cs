using System;
using System.IO;
using System.Text;
using NFile.Framework;
using NUnit.Framework;
using OfficeOpenXml;

namespace csharp_tips.Excel
{
    [TestFixture]
    class ConvertCsvToExcel
    {
        [Test]
        public void ConvertCsvToExcel_EEPlus()
        {
            using (TemporaryFile csvFile = new TemporaryFile(".csv"))
            using (TemporaryFile xlsxFile = new TemporaryFile(".xlsx"))
            {
                StringBuilder content = new StringBuilder();
                content.AppendLine("param1\tparam2\tstatus");
                content.AppendLine("0.5\t10\tpassed");
                content.AppendLine("10\t20\tfail");
                File.AppendAllText(csvFile.FileName, content.ToString());
                Console.WriteLine("created file {0}", csvFile.FileName);

                var format = new ExcelTextFormat {Delimiter = '\t', EOL = "\r"};
                // format.TextQualifier = '"';

                FileInfo excelFileInfo = new FileInfo(xlsxFile.FileName);
                using (ExcelPackage package = new ExcelPackage(excelFileInfo))
                {
                    const string WORKSHEETS_NAME = "test";
                    const bool FIRST_ROW_IS_HEADER = true;
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(WORKSHEETS_NAME);
                    worksheet.Cells["A1"].LoadFromText(new FileInfo(csvFile.FileName), format, OfficeOpenXml.Table.TableStyles.Medium27, FIRST_ROW_IS_HEADER);
                    package.Save();
                }
                Console.WriteLine("created file {0}", xlsxFile.FileName);
            }
        }
    }
}
