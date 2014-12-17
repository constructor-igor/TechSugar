using System;

namespace VS2015News
{
    //
    //
    //  References:
    //  - http://stackoverflow.com/questions/26896101/how-do-i-generate-smart-unit-tests-with-visual-studio-2015-preview
    //  - http://msdn.microsoft.com/library/dn823749%28v=vs.140%29.aspx
    //  - http://research.microsoft.com/en-us/projects/pex/
    //
    public class SmartUniteTestSample
    {
        public int Calc(int x, int y)
        {
            if (x < 0)
                return -1;
            if (y < 0)
                return -2;
            if (x == 0 && y == 0)
                return 0;
            return 1;
        }

        public int ReturnSizeOfArray(int[] data)
        {
            if (data == null)
                return 0;
            if (data.Length == 0)
                return 0;
            return data.Length;
        }

        public int ReturnMidItem(int[] data)
        {
            if (data==null)
                throw new NotImplementedException();
            if (data.Length==0)
                throw new NotImplementedException();
            return data[data.Length/2];
        }

        public string GetProductInfo(IProduct product)
        {
            if (product == null)
                throw new ArgumentNullException();
            return "name: \{product.Name}, price: \{product.Price}";
        }
    }

    public interface IProduct
    {
        string Name { get; }
        double Price { get; }
    }

    public class Camera : IProduct
    {
        public string Name { get; } = "Nikon";
        public double Price { get; } = 10;
    }
    public class Mobile : IProduct
    {
        public string Name { get; } = "Noname";
        public double Price { get; } = 100;
    }
}
