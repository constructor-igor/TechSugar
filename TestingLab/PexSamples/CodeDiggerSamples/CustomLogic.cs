
//
//
//  References:
//  - http://research.microsoft.com/en-us/projects/codedigger/gettingstarted.aspx
//  - http://research.microsoft.com/en-us/projects/pex/
//

using System;

namespace CodeDiggerSamples
{
    public class CustomLogic
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
        public void ConverMe(int[] a)
        {
            if (a == null)
                return;
            if (a.Length > 0)
                if (a[3] == 12345)
                    throw new Exception("bug");
        }
    }
}
