using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Media;

namespace RichTextBoxTest
{
    public interface IFormulaEditorGeneralParameter : IFormulaParameter
    {
        string Value { get; }
    }

    public interface IFormulaParameter
    {
        string Name { get; }
        string GeneratedFormulaPart { get; }
        string FullPart { get; }
    }

    public interface IFormulaEditorCategorizedParameter : IFormulaParameter
    {
        string Category { get; }
    }

    public interface IFormulaEditorFunction
    {
        event EventHandler<FunctionSelectedEventArgs> Selected;
        string FunctionName { get; }
    }



    public class FunctionSelectedEventArgs : EventArgs
    {
        public FunctionSelectedEventArgs(string function)
        {
            Function = function;
        }

        public string Function { get; private set; }
    }

    public class FormulaEditorFunction : IFormulaEditorFunction
    {
        #region Constructor

        public FormulaEditorFunction(string functionName)
        {
            FunctionName = functionName;

            FunctionSelectCommand = new ActionCommand(FunctionSelectCommandHandler);
        }

        #endregion

        #region Properties

        public string FunctionName { get; private set; }

        #endregion

        #region Events

        public event EventHandler<FunctionSelectedEventArgs> Selected;

        #endregion

        #region Commands

        public ICommand FunctionSelectCommand { get; private set; }

        #endregion

        #region Command handlers

        private void FunctionSelectCommandHandler()
        {
            var handler = Selected;
            if (handler == null) return;
            handler(this, new FunctionSelectedEventArgs(FunctionName));
        }

        #endregion
    }

    public class SymbolsFunctionsProvider
    {
        public enum WordType
        {
            None,
            Number,
            Parameter,
            Function,
            Symbol,
            OutputParameter,
            OpenSquareBracket,
            CloseSquareBracket,
            OpenParenthesis,
            CloseParenthesis
        }

        public struct Tag
        {
            public TextPointer StartPosition;
            public TextPointer EndPosition;
            public string TextPart;
            public SolidColorBrush Brush;
        }

        public class Word
        {
            public string WordInText { get; private set; }
            public int IndexInText { get; private set; }
            public WordType Type { get; private set; }

            public Word(string currentWord, int currentIndex)
            {
                WordInText = currentWord;
                IndexInText = currentIndex;
                Type = WordType.None;

            }

            public Word(string currentWord, int currentIndex, WordType wordType)
            {
                WordInText = currentWord;
                IndexInText = currentIndex;
                Type = wordType;
            }

            public void SetType(WordType wordType)
            {
                Type = wordType;
            }
        }

        public List<Tag> TextParts = new List<Tag>();
        public List<Word> Words = new List<Word>();

        private string allText;
        public string AllText
        {
            get { return allText; }
            set { allText = value; }
        }
        public static SolidColorBrush ParameterBrush = new SolidColorBrush(Colors.DarkBlue);
        public static SolidColorBrush FunctionBrush = new SolidColorBrush(Colors.Blue);
        public static SolidColorBrush SymbolBrush = new SolidColorBrush(Colors.DeepPink);
        public static SolidColorBrush NumberBrush = new SolidColorBrush(Colors.DeepPink);
        public static SolidColorBrush BlackBrush = new SolidColorBrush(Colors.Black);
        public static SolidColorBrush ParenthesesBrush = new SolidColorBrush(Colors.Green);

        public const string OPEN_SQUARE_BRACKET = "[";
        public const string CLOSE_SQUARE_BRACKET = "]";
        public const string OPEN_PARENTHESIS = "(";
        public const string CLOSE_PARENTHESIS = ")";


        static readonly List<string> symbols = new List<string> { "+", "-", "*", "/", ">", "<", "=" };
        static readonly List<string> abc = ("ABCDEFGHIJKLMNOPQRSTUVWXYZ_".ToCharArray()).Select(a => a.ToString()).ToList();
        static readonly List<string> numbers = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "." };

        private List<string> FunctionsList { get; set; }

        private List<string> ParametersList { get; set; }

        private List<string> OutptutParametersList { get; set; }

        public List<string> SymbolsList
        {
            get { return symbols; }
        }

        public SymbolsFunctionsProvider(RangeObservableCollection<IFormulaEditorFunction> functions)
        {
            if (functions == null || functions.Count == 0)
            {
                //InitFunctionsList();
            }
            else
            {
                FunctionsList = new List<string>();
                foreach (IFormulaEditorFunction function in functions)
                {
                    FunctionsList.Add(function.FunctionName);
                }
            }
            ParametersList = new List<string>();
            OutptutParametersList = new List<string>();
        }

        private void InitFunctionsList()
        {
            FunctionsList = new List<string>();
            FunctionsList.Add("sin");
            FunctionsList.Add("cos");
            FunctionsList.Add("tan");
            FunctionsList.Add("asin");
            FunctionsList.Add("acos");
            FunctionsList.Add("atan");
            FunctionsList.Add("cosh");
            FunctionsList.Add("sinh");
            FunctionsList.Add("tanh");
            FunctionsList.Add("sqrt");
            FunctionsList.Add("abs");
            FunctionsList.Add("log");
            FunctionsList.Add("log10");
            FunctionsList.Add("exp");
            FunctionsList.Add("pi");
            FunctionsList.Add("min");
            FunctionsList.Add("max");
            FunctionsList.Add("atan2");
            FunctionsList.Add("power");
            FunctionsList.Add("neg");
            FunctionsList.Add("CD");
            FunctionsList.Add("LimitHit");
            FunctionsList.Add("HybridParameter");
            FunctionsList.Add("NaN");
        }

        public bool IsKnownFunction(string function)
        {
            return FunctionsList.Exists(s => s.ToLower().Equals(function.ToLower()));
        }

        public bool IsKnownSymbol(string symbol)
        {
            return SymbolsList.Exists(s => s.ToLower().Equals(symbol.ToLower()));
        }

        public bool IsKnownLetter(string letter)
        {
            return abc.Exists(s => s.ToLower().Equals(letter.ToLower()));
        }

        public void ParseText(TextRange textRange)
        {
            allText = textRange.Text;
            TextParts.Clear();
            Words.Clear();
            List<string> symbolsList = SplitTextByOneSymbol();
            SplitTextByWords(symbolsList);
            SetWordsTypes();
            AddWordsToColoredPartsList(textRange);
            ColorTextParts();
        }

        private void SetWordsTypes()
        {
            foreach (Word word in Words)
            {
                if (FunctionsList.Contains(word.WordInText))
                    word.SetType(WordType.Function);
                if (ParametersList.Contains(word.WordInText))
                    word.SetType(WordType.Parameter);
                if (OutptutParametersList.Contains(word.WordInText))
                    word.SetType(WordType.OutputParameter);
                if (word.WordInText.Equals(OPEN_SQUARE_BRACKET))
                    word.SetType(WordType.OpenSquareBracket);
                if (word.WordInText.Equals(CLOSE_SQUARE_BRACKET))
                    word.SetType(WordType.CloseSquareBracket);
                if (word.WordInText.Equals(OPEN_PARENTHESIS))
                    word.SetType(WordType.OpenParenthesis);
                if (word.WordInText.Equals(CLOSE_PARENTHESIS))
                    word.SetType(WordType.CloseParenthesis);
                if (SymbolsList.Contains(word.WordInText))
                    word.SetType(WordType.Symbol);
            }
        }

        public void AddFunction(string function)
        {
            FunctionsList.Add(function);
        }

        private void SplitTextByWords(List<string> symbolsList)
        {
            int state = 0;
            string word = "";
            string number = "";
            int startIndex = 0;
            symbolsList.Add(" ");
            for (int i = 0; i < symbolsList.Count; i++)
            {
                string currentSymbol = symbolsList[i];
                switch (state)
                {
                    case 0:
                        if (abc.Contains(currentSymbol.ToUpper()))
                        {
                            word = currentSymbol;
                            state = 1;
                            startIndex = i;
                        }
                        else if (numbers.Contains(currentSymbol))
                        {
                            number = currentSymbol;
                            state = 2;
                            startIndex = i;
                        }
                        else
                            AddWord(currentSymbol, i);
                        break;
                    case 1:
                        if (abc.Contains(currentSymbol.ToUpper()))
                        {
                            word += currentSymbol;
                        }
                        else if (numbers.Contains(currentSymbol))
                        {
                            word += currentSymbol;
                        }
                        else
                        {
                            AddWord(word, startIndex);
                            AddWord(currentSymbol, i);
                            state = 0;
                        }
                        break;
                    case 2:
                        if (abc.Contains(currentSymbol.ToUpper()))
                        {
                            AddWord(number, startIndex, WordType.Number);
                            state = 1;
                            word = currentSymbol;
                        }
                        else if (numbers.Contains(currentSymbol))
                            number += currentSymbol;
                        else
                        {
                            AddWord(number, startIndex, WordType.Number);
                            AddWord(currentSymbol, i);
                            state = 0;
                        }
                        break;
                }
            }
            Words.RemoveAt(Words.Count - 1);
        }

        private void AddWord(string currentWord, int currentIndex)
        {
            Words.Add(new Word(currentWord, currentIndex));
        }

        private void AddWord(string currentWord, int currentIndex, WordType wordType)
        {
            Words.Add(new Word(currentWord, currentIndex, wordType));
        }

        public List<string> SplitTextByOneSymbol()
        {
            List<string> symbolList = new List<string>();
            string text = allText.Trim(new[] { ' ', '\n', '\r', '\t' });

            for (int i = 0; i < text.Length; i++)
            {
                int currentIndex = i;
                string oneSymbol = text.Substring(currentIndex, 1);
                symbolList.Add(oneSymbol);
            }
            return symbolList;
        }

        private void ColorTextParts()
        {
            foreach (Tag tag in TextParts)
            {
                TextRange selection = new TextRange(tag.StartPosition, tag.EndPosition);
                selection.ApplyPropertyValue(TextElement.ForegroundProperty, tag.Brush);
            }
        }

        private void AddWordsToColoredPartsList(TextRange textRange)
        {
            foreach (Word word in Words)
            {
                TextPointer selectionStart = textRange.Start.GetPositionAtOffset(word.IndexInText, LogicalDirection.Forward);
                TextPointer selectionEnd = textRange.Start.GetPositionAtOffset(word.IndexInText + word.WordInText.Length, LogicalDirection.Forward);

                switch (word.Type)
                {
                    case WordType.Function:
                        TextParts.Add(new Tag
                        {
                            StartPosition = selectionStart,
                            EndPosition = selectionEnd,
                            TextPart = word.WordInText,
                            Brush = FunctionBrush,
                        });
                        break;
                    case WordType.Parameter:
                    case WordType.OutputParameter:
                    case WordType.OpenSquareBracket:
                    case WordType.CloseSquareBracket:
                        TextParts.Add(new Tag
                        {
                            StartPosition = selectionStart,
                            EndPosition = selectionEnd,
                            TextPart = word.WordInText,
                            Brush = ParameterBrush,
                        });
                        break;
                    case WordType.Number:
                        TextParts.Add(new Tag
                        {
                            StartPosition = selectionStart,
                            EndPosition = selectionEnd,
                            TextPart = word.WordInText,
                            Brush = NumberBrush,
                        });
                        break;
                    case WordType.Symbol:
                        TextParts.Add(new Tag
                        {
                            StartPosition = selectionStart,
                            EndPosition = selectionEnd,
                            TextPart = word.WordInText,
                            Brush = SymbolBrush,
                        });
                        break;
                    case WordType.OpenParenthesis:
                    case WordType.CloseParenthesis:
                        TextParts.Add(new Tag
                        {
                            StartPosition = selectionStart,
                            EndPosition = selectionEnd,
                            TextPart = word.WordInText,
                            Brush = ParenthesesBrush,
                        });
                        break;
                    default:
                        TextParts.Add(new Tag
                        {
                            StartPosition = selectionStart,
                            EndPosition = selectionEnd,
                            TextPart = word.WordInText,
                            Brush = BlackBrush,
                        });
                        break;
                }
            }
        }

        public void AddFunctions(List<string> functions)
        {
            FunctionsList.AddRange(functions);
        }
    }
}