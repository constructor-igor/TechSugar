using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;

namespace EPPlusTests
{
    [TestFixture]
    public class Tests
    {
        private static readonly Color EMPTY_COLUMN_COLOR = Color.Gainsboro;

        [Test]
        public void Test()
        {
            string assemblyDirectory = AssemblyDirectory;
            string csvFile = Path.Combine(assemblyDirectory, @"..\..\dataFile.csv");
            string excelFile = Path.Combine(assemblyDirectory, "Excel.xlsx");
            string updatedExcelFile = Path.Combine(assemblyDirectory, "updatedExcel.xlsx");
            const string columnName = "C";
            const string sheetName = "general data";

            CreateExcelFile(excelFile, sheetName, csvFile, columnName);

            // open and save
            OpenAndSave(updatedExcelFile, excelFile);
        }

        [Test]
        public void ProductionFile_OpenSave_CannotBeOpened()
        {
            string assemblyDirectory = AssemblyDirectory;
            string sourceProductionFile = Path.Combine(assemblyDirectory, @"..\..\sourceProductionFile.xlsx");
            string targetProductionFile = Path.Combine(assemblyDirectory, @"..\..\targetProductionFile.xlsx");

            File.Delete(targetProductionFile);
            using (ExcelPackage package = new ExcelPackage(new FileInfo(sourceProductionFile)))
            {
                package.SaveAs(new FileInfo(targetProductionFile));
            }
        }

        private static void OpenAndSave(string updatedExcelFile, string excelFile)
        {
            File.Delete(updatedExcelFile);
            File.Copy(excelFile, updatedExcelFile);
            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFile)))
            {
                package.SaveAs(new FileInfo(updatedExcelFile));
            }
        }

        private static void CreateExcelFile(string excelFile, string sheetName, string csvFile, string columnName)
        {
            File.Delete(excelFile);

            FileInfo excelFileInfo = new FileInfo(excelFile);
            using (ExcelPackage package = new ExcelPackage(excelFileInfo))
            {
                ExcelWorksheets workSheets = package.Workbook.Worksheets;
                ExcelWorksheet dataWorkSheet = workSheets.Add(sheetName);
                var format = new ExcelTextFormat { Delimiter = '\t', EOL = "\r" };
                dataWorkSheet.Cells["A1"].LoadFromText(new FileInfo(csvFile), format);
                int rowsCount = dataWorkSheet.Dimension.End.Row - 1;

                ExcelColumn preColumn = dataWorkSheet.Column(2);
                preColumn.Width = 2;
                ExcelColumn postColumn = dataWorkSheet.Column(17);
                postColumn.Width = 2;

                for (int row = 0; row < rowsCount; row++)
                {
                    SetColor(dataWorkSheet.Cells[row + 1, 2], EMPTY_COLUMN_COLOR);
                    SetColor(dataWorkSheet.Cells[row + 1, 17], EMPTY_COLUMN_COLOR);
                }

                ExcelWorksheet chartsWorksheet = workSheets.Add("Charts");
                ExcelChart chart = chartsWorksheet.Drawings.AddChart("StdDev", eChartType.ColumnClustered);
                chart.Title.Text = "StdDev";
                chart.SetPosition(1, 0, 1, 0);
                chart.SetSize(800, 300);
                string yName = String.Format("'" + dataWorkSheet.Name + "'" + "!{0}2:{0}{1}", columnName, rowsCount);
                var series = chart.Series.Add(yName, "");
                series.Header = "StdDev / Configuration";

                package.Save();
            }
        }

        private static void SetColor(ExcelRangeBase cell, Color color)
        {
            cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cell.Style.Fill.BackgroundColor.SetColor(color);
        }

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}
