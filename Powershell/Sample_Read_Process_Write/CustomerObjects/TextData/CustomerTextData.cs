using System;
using Customer.Interfaces;

namespace Customer.TextData
{
    public class CustomerTextData : ITextData
    {
        private readonly string _text;
        #region ITextData
        public int TotalSymbols => _text.Length;

        public int GetNumberOf(string term)
        {
            int sum = 0;
            int startIndex = 0;
            while (_text.IndexOf(term, startIndex, StringComparison.Ordinal) != -1)
            {
                startIndex += term.Length;
                sum++;
            }
            return sum;
        }
        #endregion

        public CustomerTextData(string text)
        {
            _text = text;
        }
    }
}
