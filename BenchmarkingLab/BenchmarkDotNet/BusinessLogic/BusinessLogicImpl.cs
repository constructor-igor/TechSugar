using System;

namespace BusinessLogic
{
    public class BusinessLogicImpl
    {
        private const string TEXT = "the test shows benchmark samples";
        private const string TERMIN = "benchmark";

        public static void IndexOfSample()
        {
            bool contains = TEXT.IndexOf(TERMIN, StringComparison.Ordinal) >= 0;
        }
        public static void ContainsSample()
        {
            bool contains = TEXT.Contains(TERMIN);
        }
    }
}
