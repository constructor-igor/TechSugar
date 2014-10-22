using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Windows.Documents;
using Caliburn.Micro;
using Action = System.Action;

namespace RichTextBoxTest
{
    public interface ICommand
    {
        void Execute();

        bool CanExecute(object parameter);
        event EventHandler CanExecuteChanged;
    }

    public class ActionCommand : ICommand
    {
        private readonly Action handler;

        public ActionCommand(Action execute)
        {
            handler = execute;
        }

        public void Execute()
        {
            if (handler != null)
                handler();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }

    public class FirstViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private TextPointer caretPointer;
        public TextPointer CaretPointer
        {
            get
            {
                return caretPointer;
            }
            set
            {
                caretPointer = value;
                NotifyOfPropertyChange(() => CaretPointer);
            }
        }

        private SymbolsFunctionsProvider symbolsFunctionsProvider;
        public SymbolsFunctionsProvider SymbolsFunctionsProvider
        {
            get { return symbolsFunctionsProvider; }
            set
            {
                symbolsFunctionsProvider = value;
                NotifyOfPropertyChange(() => Formula);
                NotifyOfPropertyChange(() => SymbolsFunctionsProvider);
            }
        }

        protected RangeObservableCollection<IFormulaEditorFunction> m_functions;
        public RangeObservableCollection<IFormulaEditorFunction> Functions
        {
            get { return m_functions; }
        }

        string formula;
        public string Formula
        {
            get
            {
                return formula;
            }
            set
            {
                formula = value;
                NotifyOfPropertyChange(() => Formula);
            }
        }

        public FirstViewModel()
        {
            Formula = "set from constractor";
            InitFunctionsList();
            SymbolsFunctionsProvider = new SymbolsFunctionsProvider(Functions);

        }

        private void InitFunctionsList()
        {
            m_functions = new RangeObservableCollection<IFormulaEditorFunction>();
            FormulaEditorFunction formulaEditorFunction = new FormulaEditorFunction("sin");
            formulaEditorFunction.Selected += formulaEditorFunction_Selected;
            m_functions.Add(formulaEditorFunction);
        }

        void formulaEditorFunction_Selected(object sender, FunctionSelectedEventArgs e)
        {
            AddTextToFormula(e.Function);
        }

        private void AddTextToFormula(string operation)
        {
            if (CaretPointer != null)
            {
                CaretPointer.GetInsertionPosition(LogicalDirection.Backward).InsertTextInRun(operation);
                string currentFormula = Formula;
            }
        }
    }
}
