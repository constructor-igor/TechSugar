using System;

namespace SimpleMathSample
{
    public class Algorithm
    {
        public double Calc(CustomMath customMath, double x, double y, double z)
        {
            double xy = customMath.Calc(x, y);
            if (xy < 0)
                throw new AlgorithmNegativeCalcException();
            if (xy == 0)
                xy = (x+y)/10;
            double r = xy + z;            
            return r;
        }
    }

    public class AlgorithmNegativeCalcException : Exception
    {        
    }
}