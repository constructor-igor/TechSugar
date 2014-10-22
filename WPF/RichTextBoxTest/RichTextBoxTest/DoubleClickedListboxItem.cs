using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RichTextBoxTest
{
    public class DoubleClickedListboxItem : ListBoxItem
    {
        #region Commands

        public ICommand DoubleClickCommand
        {
            get { return (ICommand)GetValue(DoubleClickCommandProperty); }
            set { SetValue(DoubleClickCommandProperty, value); }
        }

        #endregion

        #region DependencyProperty

        public static readonly DependencyProperty DoubleClickCommandProperty = DependencyProperty.Register(
            "DoubleClickCommand",
            typeof(ICommand), typeof(DoubleClickedListboxItem),
            new PropertyMetadata(null));

        #endregion

        #region Constructors

        public DoubleClickedListboxItem()
        {
            PreviewMouseDoubleClick += OnPreviewDoubleClick;
        }

        #endregion

        #region Event Handlers

        private void OnPreviewDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (DoubleClickedListboxItem)sender;
            if (DoubleClickCommand != null)
            {
                DoubleClickCommand.Execute();
            }
        }

        #endregion
    }
}
