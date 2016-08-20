using System;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;

namespace EPPlusSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            //const string csvFile = @"..\..\dataFile.csv";
            const string csvFile = @"..\..\realData.csv";
            const string excelFile = "Excel.xlsx";
            const string updatedExcelFile = "updatedExcel.xlsx";
            //const string columnName = "C";
            const string columnName = "AI";
            const string sheetName = "general data";

            File.Delete(excelFile);

            // create excel file with chart

            FileInfo excelFileInfo = new FileInfo(excelFile);
            using (ExcelPackage package = new ExcelPackage(excelFileInfo))
            {
                ExcelWorksheets workSheets = package.Workbook.Worksheets;
                ExcelWorksheet dataWorkSheet = workSheets.Add(sheetName);
                var format = new ExcelTextFormat { Delimiter = '\t', EOL = "\r" };
                dataWorkSheet.Cells["A1"].LoadFromText(new FileInfo(csvFile), format);

                ExcelWorksheet chartsWorksheet = workSheets.Add("Charts");
                int rowsCount = dataWorkSheet.Dimension.End.Row - 1;
                ExcelChart chart = chartsWorksheet.Drawings.AddChart("StdDev", eChartType.ColumnClustered);
                chart.Title.Text = "StdDev";
                chart.SetPosition(1, 0, 1, 0);
                chart.SetSize(800, 300);
                string yName = String.Format("'" + dataWorkSheet.Name + "'" + "!{0}2:{0}{1}", columnName, rowsCount);
                var series = chart.Series.Add(yName, "");
                series.Header = "StdDev / Configuration";

                package.Save();
            }

            // open and save
            File.Delete(updatedExcelFile);
            File.Copy(excelFile, updatedExcelFile);
            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFile)))
            {
                package.SaveAs(new FileInfo(updatedExcelFile));
            }

        }
    }
}
