using System;

namespace Gist
{
    public static class GistSampleExtensions
    {
        public static bool IsEmpty(this string value)
        {
            return String.IsNullOrEmpty(value);
        }
    }
}