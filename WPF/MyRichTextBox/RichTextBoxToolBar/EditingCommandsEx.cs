using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace RichTextBoxToolBar
{
    public static class EditingCommandsEx
    {
        #region Routed commands

        public static readonly RoutedUICommand ToggleStrikethrough = new RoutedUICommand("Strikeout", "ToggleStrikeout", typeof(EditingCommandsEx));

        public static readonly RoutedUICommand SelectFontFamily = new RoutedUICommand("Select font family", "SelectFontFamily", typeof(EditingCommandsEx));
        public static readonly RoutedUICommand SelectFontSize = new RoutedUICommand("Select font size", "SelectFontSize", typeof(EditingCommandsEx));
        public static readonly RoutedUICommand SelectBackgroundColor = new RoutedUICommand("Select background color", "SelectBackgroundColor", typeof(EditingCommandsEx));
        public static readonly RoutedUICommand SelectForegroundColor = new RoutedUICommand("Select foreground color", "SelectForegroundColor", typeof(EditingCommandsEx));

        #endregion // Routed commands

        #region Initialization

        static EditingCommandsEx()
        {
            CommandManager.RegisterClassCommandBinding(typeof(RichTextBox),
                new CommandBinding(EditingCommandsEx.ToggleStrikethrough,
                    new ExecutedRoutedEventHandler(EditingCommandsEx_ToggleStrikethrough_Executed),
                    new CanExecuteRoutedEventHandler(EditingCommandsEx_CanExecute)));

            CommandManager.RegisterClassCommandBinding(typeof(RichTextBox),
                new CommandBinding(EditingCommandsEx.SelectFontFamily,
                    new ExecutedRoutedEventHandler(EditingCommandsEx_SelectFontFamily_Executed),
                    new CanExecuteRoutedEventHandler(EditingCommandsEx_CanExecute)));

            CommandManager.RegisterClassCommandBinding(typeof(RichTextBox),
                new CommandBinding(EditingCommandsEx.SelectFontSize,
                    new ExecutedRoutedEventHandler(EditingCommandsEx_SelectFontSize_Executed),
                    new CanExecuteRoutedEventHandler(EditingCommandsEx_CanExecute)));

            CommandManager.RegisterClassCommandBinding(typeof(RichTextBox),
                new CommandBinding(EditingCommandsEx.SelectForegroundColor,
                    new ExecutedRoutedEventHandler(EditingCommandsEx_SelectForegroundColor_Executed),
                    new CanExecuteRoutedEventHandler(EditingCommandsEx_CanExecute)));

            CommandManager.RegisterClassCommandBinding(typeof(RichTextBox),
                new CommandBinding(EditingCommandsEx.SelectBackgroundColor,
                    new ExecutedRoutedEventHandler(EditingCommandsEx_SelectBackgroundColor_Executed),
                    new CanExecuteRoutedEventHandler(EditingCommandsEx_CanExecute)));

            // Fix RichTextBox issues

            CommandManager.RegisterClassCommandBinding(typeof(RichTextBox),
                new CommandBinding(EditingCommands.ToggleSubscript,
                    new ExecutedRoutedEventHandler(EditingCommands_ToggleSubscript_Executed),
                    new CanExecuteRoutedEventHandler(EditingCommandsEx_CanExecute)));

            CommandManager.RegisterClassCommandBinding(typeof(RichTextBox),
                new CommandBinding(EditingCommands.ToggleSuperscript,
                    new ExecutedRoutedEventHandler(EditingCommands_ToggleSuperscript_Executed),
                    new CanExecuteRoutedEventHandler(EditingCommandsEx_CanExecute)));
        }

        #endregion // Initialization

        #region Command handlers implementation

        private static void EditingCommandsEx_CanExecute(Object sender, CanExecuteRoutedEventArgs e)
        {
            var editor = sender as RichTextBox;
            e.CanExecute = editor != null;
        }

        private static void EditingCommandsEx_ToggleStrikethrough_Executed(Object sender, ExecutedRoutedEventArgs e)
        {
            var editor = sender as RichTextBox;
            if (e.Handled = editor != null)
            {
                RichTextBoxToolBarHelper.ToggleSelectionFormattingProperty<TextDecorationCollection>(editor.Selection,
                    Inline.TextDecorationsProperty,
                    TextDecorations.Strikethrough, null,
                    (item1, item2) => TextDecorationCollection.Equals(item1, item2));
            }
        }

        private static void EditingCommandsEx_SelectFontFamily_Executed(Object sender, ExecutedRoutedEventArgs e)
        {
            var editor = sender as RichTextBox;
            if (e.Handled = (editor != null && e.Parameter is FontFamily))
            {
                RichTextBoxToolBarHelper.ApplyNewValueToFormattingProperty<FontFamily>(
                    editor.Selection, Paragraph.FontFamilyProperty, (FontFamily)e.Parameter,
                    (item1, item2) => FontFamily.Equals(item1, item2));
            }
        }

        private static void EditingCommandsEx_SelectFontSize_Executed(Object sender, ExecutedRoutedEventArgs e)
        {
            var editor = sender as RichTextBox;
            Double? size = RichTextBoxToolBarHelper.GetSelectionFontSize(e.Parameter);

            if (e.Handled = (editor != null && size != null))
            {
                RichTextBoxToolBarHelper.ApplyNewValueToFormattingProperty<Double>(
                                    editor.Selection, Paragraph.FontSizeProperty, (Double)size,
                                    (item1, item2) => Double.Equals(item1, item2));
            }
        }

        private static void EditingCommandsEx_SelectForegroundColor_Executed(Object sender, ExecutedRoutedEventArgs e)
        {
            var editor = sender as RichTextBox;
            Color? color = RichTextBoxToolBarHelper.GetSelectionColor(e.Parameter, Colors.Black);

            if (e.Handled = (editor != null && color != null))
            {
                RichTextBoxToolBarHelper.ApplyNewValueToFormattingProperty<SolidColorBrush>(
                    editor.Selection, TextElement.ForegroundProperty, new SolidColorBrush((Color)color),
                    (item1, item2) => SolidColorBrush.Equals(item1, item2));
            }
        }

        private static void EditingCommandsEx_SelectBackgroundColor_Executed(Object sender, ExecutedRoutedEventArgs e)
        {
            var editor = sender as RichTextBox;
            Color? color = RichTextBoxToolBarHelper.GetSelectionColor(e.Parameter, Colors.White);

            if (e.Handled = (editor != null && color != null))
            {
                RichTextBoxToolBarHelper.ApplyNewValueToFormattingProperty<SolidColorBrush>(
                    editor.Selection, TextElement.BackgroundProperty, new SolidColorBrush((Color) color),
                    (item1, item2) => SolidColorBrush.Equals(item1, item2));
            }
        }

        private static void EditingCommands_ToggleSubscript_Executed(Object sender, ExecutedRoutedEventArgs e)
        {
            var editor = sender as RichTextBox;
            if (e.Handled = editor != null)
            {
                RichTextBoxToolBarHelper.ToggleSelectionFormattingProperty<BaselineAlignment>(editor.Selection,
                    Inline.BaselineAlignmentProperty,
                    BaselineAlignment.Subscript, BaselineAlignment.Baseline,
                    (item1, item2) => item1 == item2);
            }
        }

        private static void EditingCommands_ToggleSuperscript_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var editor = sender as RichTextBox;
            if (e.Handled = editor != null)
            {
                RichTextBoxToolBarHelper.ToggleSelectionFormattingProperty<BaselineAlignment>(editor.Selection,
                    Inline.BaselineAlignmentProperty,
                    BaselineAlignment.Superscript, BaselineAlignment.Baseline,
                    (item1, item2) => item1 == item2);
            }
        }

        #endregion // Command handlers implementation
    } 

}
