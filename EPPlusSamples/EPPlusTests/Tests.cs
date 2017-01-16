using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Xml;
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

        [Test]
        public void ConditionalFormating()
        {
            string assemblyDirectory = AssemblyDirectory;
            string csvFile = Path.Combine(assemblyDirectory, @"..\..\dataFile.csv");
            string excelFile = Path.Combine(assemblyDirectory, "ExcelWithConditionalFormating.xlsx");
            string sheetName = "formating";

            File.Delete(excelFile);
            FileInfo excelFileInfo = new FileInfo(excelFile);
            using (ExcelPackage package = new ExcelPackage(excelFileInfo))
            {
                ExcelWorksheets workSheets = package.Workbook.Worksheets;
                ExcelWorksheet dataWorkSheet = workSheets.Add(sheetName);
                var format = new ExcelTextFormat { Delimiter = '\t', EOL = "\r" };
                dataWorkSheet.Cells["A1"].LoadFromText(new FileInfo(csvFile), format);
                int rowsCount = dataWorkSheet.Dimension.End.Row - 1;

                // Add Conditional Formatting for the cells we just filled
                AddConditionalFormattingColorsRange(dataWorkSheet, "C1:C10", "FFF8696B", "FF63BE7B");

                AddConditionalFormatting(dataWorkSheet, "A2:A20");

                package.Save();
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

        public static void AddConditionalFormatting(ExcelWorksheet inWorksheet, string address)
        {
            //ExcelAddress _formatRangeAddress = new ExcelAddress("B3:B10,D3:D10,F3:F10,H3:H10:J3:J10");
            ExcelAddress _formatRangeAddress = new ExcelAddress(address);
            string _statement = "1.2";
            //string _statement = "IF(1, 1, 0)";
            var _cond4 = inWorksheet.ConditionalFormatting.AddEqual(_formatRangeAddress);
            _cond4.Style.Fill.PatternType = ExcelFillStyle.Solid;
            _cond4.Style.Fill.BackgroundColor.Color = Color.Green;
            _cond4.Formula = _statement;
        }

        /// <summary>
        /// Add Conditional Formatting to an EPPlus Worksheet, using the "colorScale" formatting option.
        /// </summary>
        /// <param name="inWorksheet">EPPlus Worksheet object</param>
        /// <param name="inSqref">Range of cells to format</param>
        /// <param name="inColor1">From Color</param>
        /// <param name="inColor2">To Color</param>
        public static void AddConditionalFormattingColorsRange(ExcelWorksheet inWorksheet, string inSqref, string inColor1, string inColor2)
        {
            // Example of a XML used to format A1:A10 cells, with a Color Scale:
            //
            //  <conditionalFormatting sqref="A1:A10">
            //    <cfRule type="colorScale" priority="1">
            //      <colorScale>
            //        <cfvo type="min"/>
            //        <cfvo type="max"/>
            //        <color rgb="FFF8696B"/>
            //        <color rgb="FF63BE7B"/>
            //      </colorScale>
            //    </cfRule>
            //  </conditionalFormatting>

            // The WorksheetXml
            XmlDocument xmlDoc = inWorksheet.WorksheetXml;

            // Root element of the WorksheetXml
            XmlElement root = xmlDoc.DocumentElement;

            // Generic element
            XmlElement xmlElement;

            // -----------------------------------------
            // <conditionalFormatting sqref="A1:A10">
            // -----------------------------------------
            XmlElement eleConditionalFormatting = xmlDoc.CreateElement("conditionalFormatting", root.NamespaceURI);
            AddAttribute(eleConditionalFormatting, "sqref", inSqref);

            // -----------------------------------------
            // <cfRule type="colorScale" priority="1">
            // -----------------------------------------
            XmlElement eleCfRule = xmlDoc.CreateElement("cfRule", root.NamespaceURI);
            AddAttribute(eleCfRule, "type", "colorScale");
            AddAttribute(eleCfRule, "priority", "1");

            // -----------------------------------------
            // <colorScale>
            // -----------------------------------------
            XmlElement eleColorScale = xmlDoc.CreateElement("colorScale", root.NamespaceURI);

            // -----------------------------------------
            // <cfvo type="min"/>
            // -----------------------------------------
            xmlElement = xmlDoc.CreateElement("cfvo", root.NamespaceURI);
            AddAttribute(xmlElement, "type", "min");
            eleColorScale.AppendChild(xmlElement);

            // -----------------------------------------
            // <cfvo type="max"/>
            // -----------------------------------------
            xmlElement = xmlDoc.CreateElement("cfvo", root.NamespaceURI);
            AddAttribute(xmlElement, "type", "max");
            eleColorScale.AppendChild(xmlElement);

            // -----------------------------------------
            // <color rgb="FFF8696B"/>
            // -----------------------------------------
            xmlElement = xmlDoc.CreateElement("color", root.NamespaceURI);
            AddAttribute(xmlElement, "rgb", inColor1);
            eleColorScale.AppendChild(xmlElement);

            // -----------------------------------------
            // <color rgb="FF63BE7B"/>
            // -----------------------------------------
            xmlElement = xmlDoc.CreateElement("color", root.NamespaceURI);
            AddAttribute(xmlElement, "rgb", inColor2);
            eleColorScale.AppendChild(xmlElement);

            // -----------------------------------------
            // </colorScale>
            // -----------------------------------------
            eleCfRule.AppendChild(eleColorScale);

            // -----------------------------------------
            // </cfRule>
            // -----------------------------------------
            eleConditionalFormatting.AppendChild(eleCfRule);

            // -----------------------------------------
            // </conditionalFormatting>
            // -----------------------------------------
            root.InsertAfter(eleConditionalFormatting, root.LastChild);
        }

        /// <summary>
        /// Add an attribute to a XML Element.
        /// </summary>
        /// <param name="inXmlElement">XML Element to add the attribute to</param>
        /// <param name="inAttributeName">Attribute name</param>
        /// <param name="inAttributeValue">Attribute value</param>
        public static void AddAttribute(XmlElement inXmlElement, string inAttributeName, string inAttributeValue)
        {
            XmlAttribute xmlAttribute = inXmlElement.OwnerDocument.CreateAttribute(inAttributeName);
            xmlAttribute.Value = inAttributeValue;
            inXmlElement.SetAttributeNode(xmlAttribute);
        }
    }
}
