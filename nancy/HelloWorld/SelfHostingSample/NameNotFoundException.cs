using System;

namespace SelfHostingSample
{
    public class NameNotFoundException : Exception
    {
        public NameNotFoundException(string notFoundName) : base(String.Format("Not found '{0}'.", notFoundName))
        {
            
        }
    }
}