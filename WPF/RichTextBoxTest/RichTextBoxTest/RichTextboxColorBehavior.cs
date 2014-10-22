using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Interactivity;

namespace RichTextBoxTest
{
    public class RichTextboxColorBehavior : Behavior<RichTextBox>
    {
        #region DependencyProperty

        public SymbolsFunctionsProvider Provider
        {
            get { return (SymbolsFunctionsProvider)GetValue(ProviderProperty); }
            set { SetValue(ProviderProperty, value); }
        }

        public static readonly DependencyProperty ProviderProperty =
            DependencyProperty.Register("Provider", typeof(SymbolsFunctionsProvider),
                                                    typeof(RichTextboxColorBehavior),
                                                    new PropertyMetadata(default(SymbolsFunctionsProvider)));

        public TextPointer CaretPositionPointer
        {
            get { return (TextPointer)GetValue(CaretPositionPointerProperty); }
            set { SetValue(CaretPositionPointerProperty, value); }
        }

        public static readonly DependencyProperty CaretPositionPointerProperty =
            DependencyProperty.Register("CaretPositionPointer", typeof(TextPointer), typeof(RichTextboxColorBehavior), new PropertyMetadata(default(TextPointer)));


        #endregion

        private void AssociatedObject_SelectionChanged(object sender, RoutedEventArgs e)
        {
            RichTextBox t = sender as RichTextBox;
            if (t != null)
            {
                t.AppendText("");
                CaretPositionPointer = t.CaretPosition;
            }
        }

        protected override void OnAttached()
        {
            AssociatedObject.TextChanged += AssociatedObject_TextChanged;
            AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
            AssociatedObject.Loaded += AssociatedObjectOnLoaded;
        }

        private void AssociatedObjectOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            AssociatedObject.Focus();
            CaretPositionPointer = AssociatedObject.CaretPosition;
        }

        private void AssociatedObject_TextChanged(object sender, TextChangedEventArgs e)
        {
            RichTextBox t = sender as RichTextBox;
            if (t == null || t.Document == null) return;
            CaretPositionPointer = t.CaretPosition;
            TextRange textRange = new TextRange(t.Document.ContentStart, t.Document.ContentEnd);

            if (!string.IsNullOrWhiteSpace(textRange.Text) && Provider != null)
            {
                t.TextChanged -= AssociatedObject_TextChanged;
                textRange.ClearAllProperties();
                Provider.ParseText(textRange);
                t.TextChanged += AssociatedObject_TextChanged;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (AssociatedObject != null)
            {
                AssociatedObject.TextChanged -= AssociatedObject_TextChanged;
                AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
                AssociatedObject.Loaded -= AssociatedObjectOnLoaded;
            }
        }
    }
}