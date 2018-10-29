using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageAlgs
{
    public static class PictureExtensions
    {
        public static void ToConsole(this Picture picture)
        {
            int width = picture.Width;
            int height = picture.Height;
            byte[] data = picture.Data;

            StringBuilder content = new StringBuilder()
                .AppendLine($"{width} x {height}");
            List<string> rowsContent = Enumerable.Range(0, height).Select(rowIndex =>
            {
                int rowStartIndex = rowIndex * width;
                int rowSize = width;
                List<byte> rowData = new List<byte>();
                for (int i = rowStartIndex; i < rowStartIndex + rowSize; i++)
                {
                    rowData.Add(data[i]);
                }
                string rowContent = string.Join(",", rowData);
                return rowContent;
            }).ToList();
            foreach (string rowContent in rowsContent)
            {
                content.AppendLine(rowContent);
            }
            Console.WriteLine(content);
        }
    }
}