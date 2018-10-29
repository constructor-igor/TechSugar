using System.Collections.Generic;
using System.Linq;

namespace ImageAlgs
{
    public class Picture
    {
        public readonly byte[] Data;
        public readonly int Width;
        public readonly int Height;        

        public Picture(IEnumerable<byte> data, int width, int height)
        {
            Width = width;
            Height = height;
            Data = data.ToArray();
        }

        public byte GetPixel(int col, int row)
        {
            return Data[row * Width + col];
        }

        public Picture Cut(int col, int row, int width, int height)
        {
            byte[] cutData = new byte[width * height];
            int cutIndex = 0;
            for (int rowIndex = row; rowIndex < row + height; rowIndex++)
            for (int colIndex = col; colIndex < col + width; colIndex++)
            {
                cutData[cutIndex++] = GetPixel(colIndex, rowIndex);
            }
            return new Picture(cutData, width, height);
        }
    }
}
