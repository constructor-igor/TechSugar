using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
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
                    const bool FIRST_ROW_IS_HEADER = false;
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(WORKSHEETS_NAME);
                    worksheet.Cells["A1"].LoadFromText(new FileInfo(csvFile.FileName), format);
                    //worksheet.Cells["A1"].LoadFromText(new FileInfo(csvFile.FileName), format, OfficeOpenXml.Table.TableStyles.None, FIRST_ROW_IS_HEADER);

                    for (int i = 0; i < 3; i++)
                    {
                        ExcelRange cell = worksheet.Cells[i+1, 2];
                        int cellValue;
                        if (Int32.TryParse(cell.Text, out cellValue) && cellValue>10)
                        {
                            cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Green);
                        }
                    }

                    package.Save();
                }
                Console.WriteLine("created file {0}", xlsxFile.FileName);
            }
        }

        [Test, STAThread]
        [Category("failed, created empty excel file")]
        public void ConvertCsvToExcel_MicrosoftOfficeInteropExcel()
        {
            StringBuilder content = new StringBuilder();
            content.AppendLine("param1\tparam2\tstatus");
            content.AppendLine("0.5\t10\tpassed");
            content.AppendLine("10\t20\tfail");

            using (TemporaryFile xlsxFile = new TemporaryFile(".xlsx"))
            {
                Clipboard.SetText(content.ToString());
                Microsoft.Office.Interop.Excel.Application xlexcel;
                Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                xlexcel = new Microsoft.Office.Interop.Excel.Application();
                // for excel visibility
                //xlexcel.Visible = true;
                // Creating a new workbook
                xlWorkBook = xlexcel.Workbooks.Add(misValue);
                // Putting Sheet 1 as the sheet you want to put the data within
                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet) xlWorkBook.Worksheets.get_Item(1);
                // creating the range
                Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range) xlWorkSheet.Cells[1, 1];
                CR.Select();
                xlWorkSheet.Paste(CR, false);
                xlWorkSheet.SaveAs(xlsxFile.FileName);                
                xlexcel.Quit();
                Console.WriteLine("Created file {0}", xlsxFile.FileName);
            }
        }
    }
}
