using System;

namespace PexDemo
{
    public class PexDemoClass
    {
        public void ConvertMe(int[] a)
        {
            if (a == null)
                return;
            if (a.Length>0)
                if (a[3] == 1234)
                    throw new Exception("bug");
        }
    }
}
