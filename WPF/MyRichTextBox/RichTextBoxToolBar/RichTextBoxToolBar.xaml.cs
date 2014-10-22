using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RichTextBoxToolBar
{
    /// <summary>
    /// Interaction logic for RichTextBoxToolBar.xaml
    /// </summary>
    public partial class RichTextBoxToolBar : ToolBar
    {
        #region Public properties

        public static readonly DependencyProperty TargetEditorProperty =
            DependencyProperty.Register("TargetEditor",
            typeof(RichTextBox), typeof(RichTextBoxToolBar),
            new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.None,
                new PropertyChangedCallback(TargetEditorChanged)));

        public RichTextBox TargetEditor
        {
            get { return (RichTextBox) GetValue(TargetEditorProperty); }
            set { SetValue(TargetEditorProperty, value); }
        }

        public static readonly DependencyProperty LeftEnabledWhenEditorFocusLostProperty =
            DependencyProperty.Register("LeftEnabledWhenEditorFocusLost",
            typeof(Boolean), typeof(RichTextBoxToolBar),
            new FrameworkPropertyMetadata(false,
                FrameworkPropertyMetadataOptions.None,
                UpdateToolItemsWhenPropertyChangedCallback));

        public Boolean LeftEnabledWhenEditorFocusLost
        {
            get { return (Boolean)GetValue(LeftEnabledWhenEditorFocusLostProperty); }
            set { SetValue(LeftEnabledWhenEditorFocusLostProperty, value); }
        }

        private static readonly DependencyPropertyKey CurrentEditorPropertyKey
            = DependencyProperty.RegisterReadOnly("CurrentEditor",
            typeof(RichTextBox), typeof(RichTextBoxToolBar),
            new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.None,
                UpdateToolItemsWhenPropertyChangedCallback));

        public static readonly DependencyProperty CurrentEditorProperty
            = CurrentEditorPropertyKey.DependencyProperty;

        public RichTextBox CurrentEditor
        {
            get { return (RichTextBox)GetValue(CurrentEditorProperty); }
            protected set { SetValue(CurrentEditorPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey IsEnabledToolBarItemsPropertyKey
            = DependencyProperty.RegisterReadOnly("IsEnabledToolBarItems",
            typeof(Boolean), typeof(RichTextBoxToolBar),
            new FrameworkPropertyMetadata(false,
                FrameworkPropertyMetadataOptions.None));

        public static readonly DependencyProperty IsEnabledToolBarItemsProperty
            = IsEnabledToolBarItemsPropertyKey.DependencyProperty;

        public Boolean IsEnabledToolBarItems
        {
            get { return (Boolean)GetValue(IsEnabledToolBarItemsProperty); }
            protected set { SetValue(IsEnabledToolBarItemsPropertyKey, value); }
        }

        #endregion // Public properties

        #region Update dependency properties
        
        private static void TargetEditorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RichTextBoxToolBar) d).DoTargetEditorChanged(
                e.OldValue as RichTextBox,
                e.NewValue as RichTextBox);
        }

        private static void UpdateToolItemsWhenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RichTextBoxToolBar)d).UpdateToolItems();
        }

        #endregion // Update dependency properties

        #region Initialization

        public RichTextBoxToolBar()
        {
            InitializeComponent();

            this._focusedRichTextBoxTracker = new KeyboardFocusTrackingElement<RichTextBox>(
                this, true,
                new FrameworkElement[] {this});

            this.EditorSelectionChangedEventHandler =
                delegate(Object sender, RoutedEventArgs e)
                {
                    this.UpdateToolItems();
                };

            this.BindingElementFocusChangedEventHandler =
                delegate(Object sender, KeyboardFocusChangedEventArgs e)
                {
                    this.DoBindingElementFocusChanged(
                        e.OldFocus as RichTextBox,
                        e.NewFocus as RichTextBox);
                };
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.Loaded += new RoutedEventHandler(OnLoaded);
            this.Unloaded += new RoutedEventHandler(OnUnloaded);

            this._radioButtonsManager = new RadioButtonsWithoutChecksManager(
                new RadioButton[]
                {
                    this._tgbtnBullets, this._tgbtnNumbering,
                    this._tgbtnSubscript, this._tgbtnSuperscript
                });
        }

        private void OnLoaded(Object sender, RoutedEventArgs e)
        {
            this._focusedRichTextBoxTracker.KeyboardFocusChanged +=
                this.BindingElementFocusChangedEventHandler;
            this._focusedRichTextBoxTracker.Bind();
            this._radioButtonsManager.Bind();
        }

        private void OnUnloaded(Object sender, RoutedEventArgs e)
        {
            this._radioButtonsManager.Unbind();
            this._focusedRichTextBoxTracker.KeyboardFocusChanged -=
                this.BindingElementFocusChangedEventHandler;
            this._focusedRichTextBoxTracker.Unbind();
        }

        #endregion // Initialization

        #region Update tool items

        public void UpdateToolItems()
        {
            var selection = this.GetCurrentEditorSelection();

            try 
            {
                // Disable circular updates by the recall tool bar items commands
                this.IsEnabledToolBarItems = false;

                RichTextBoxToolBarHelper.UpdateSelectionFontFamily(
                    this._cbFontFamily, selection);
                RichTextBoxToolBarHelper.UpdateSelectionFontSize(
                    this._cbFontSize, selection);

                RichTextBoxToolBarHelper.UpdateSelectionItemCheckedState(
                    this._tgbtnBold, selection,
                    TextElement.FontWeightProperty, FontWeights.Bold);
                RichTextBoxToolBarHelper.UpdateSelectionItemCheckedState(
                    this._tgbtnItalic, selection,
                    TextElement.FontStyleProperty, FontStyles.Italic);
                RichTextBoxToolBarHelper.UpdateSelectionItemCheckedState(
                    this._tgbtnUnderline, selection,
                    Inline.TextDecorationsProperty, TextDecorations.Underline);
                RichTextBoxToolBarHelper.UpdateSelectionItemCheckedState(
                    this._tgbtnStrikeout, selection,
                    Inline.TextDecorationsProperty, TextDecorations.Strikethrough);

                RichTextBoxToolBarHelper.UpdateSelectionItemCheckedState(
                    this._tgbtnAlignLeft, selection,
                    Paragraph.TextAlignmentProperty, TextAlignment.Left);
                RichTextBoxToolBarHelper.UpdateSelectionItemCheckedState(
                    this._tgbtnAlignCenter, selection,
                    Paragraph.TextAlignmentProperty, TextAlignment.Center);
                RichTextBoxToolBarHelper.UpdateSelectionItemCheckedState(
                    this._tgbtnAlignRight, selection,
                    Paragraph.TextAlignmentProperty, TextAlignment.Right);
                RichTextBoxToolBarHelper.UpdateSelectionItemCheckedState(
                    this._tgbtnAlignJustify, selection,
                    Paragraph.TextAlignmentProperty, TextAlignment.Justify);


                RichTextBoxToolBarHelper.UpdateSelectionColor(
                    this._cbBackgroundColors, selection,
                    TextElement.BackgroundProperty, Colors.White);
                RichTextBoxToolBarHelper.UpdateSelectionColor(
                    this._cbForegroundColors, selection,
                    TextElement.ForegroundProperty, Colors.Black);

                RichTextBoxToolBarHelper.UpdateSelectionListType(
                    this._tgbtnBullets, selection,
                    TextMarkerStyle.Disc);
                RichTextBoxToolBarHelper.UpdateSelectionListType(
                    this._tgbtnNumbering, selection,
                    TextMarkerStyle.Decimal);

                RichTextBoxToolBarHelper.UpdateSelectionItemCheckedState(
                    this._tgbtnSubscript, selection,
                    Inline.BaselineAlignmentProperty, BaselineAlignment.Subscript);
                RichTextBoxToolBarHelper.UpdateSelectionItemCheckedState(
                    this._tgbtnSuperscript, selection,
                    Inline.BaselineAlignmentProperty, BaselineAlignment.Superscript);
            }
            finally
            {
                this.IsEnabledToolBarItems = selection != null;
            }
        }

        #endregion // Update tool items

        #region Events

        private void _cbFontFamily_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;

            if (cb.IsEnabled)
                SetFontFamily(cb.SelectedValue as FontFamily);
        }

        private void _cbFontSize_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;

            if (cb.IsEnabled)
                SetFontSize(cb.SelectedValue);
        }

        private void _cbFontSize_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;

            if (cb.IsEnabled && e.Key == Key.Return)
            {
                e.Handled = SetFontSize(cb.Text);
            }
        }

        private void _cbBackgroundColors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;

            if (cb.IsEnabled)
                SetColor(cb.SelectedValue,
                    EditingCommandsEx.SelectBackgroundColor);
        }

        private void _cbForegroundColors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;

            if (cb.IsEnabled)
                SetColor(cb.SelectedValue,
                    EditingCommandsEx.SelectForegroundColor);
        }

        #endregion // Events

        #region Private methods

        private void DoTargetEditorChanged(RichTextBox oldEditor, RichTextBox newEditor)
        {
            if (oldEditor == newEditor)
                return;

            if (oldEditor != null)
            {
                oldEditor.SelectionChanged -= this.EditorSelectionChangedEventHandler;
            }

            if (newEditor != null)
            {
                this._focusedRichTextBoxTracker.Unbind();
                newEditor.SelectionChanged += this.EditorSelectionChangedEventHandler;
                this._focusedRichTextBoxTracker.CurrentElement = newEditor;
                this._focusedRichTextBoxTracker.BindingToRoot = false;
                this._focusedRichTextBoxTracker.Bind();
            }
            else
            {
                this._focusedRichTextBoxTracker.Unbind();
                this._focusedRichTextBoxTracker.CurrentElement = this;
                this._focusedRichTextBoxTracker.BindingToRoot = true;
                this._focusedRichTextBoxTracker.Bind();
            }

            this.CurrentEditor = newEditor;
        }

        private void DoBindingElementFocusChanged(RichTextBox oldEditor, RichTextBox newEditor)
        {
            if (this.TargetEditor == null && oldEditor != newEditor)
            {
                if (oldEditor != null)
                {
                    oldEditor.SelectionChanged -= this.EditorSelectionChangedEventHandler;
                }

                if (newEditor != null)
                {
                    newEditor.SelectionChanged += this.EditorSelectionChangedEventHandler;
                }
            }

            if (newEditor != null || !this.LeftEnabledWhenEditorFocusLost)
                this.CurrentEditor = newEditor;

            this.UpdateToolItems();
        }

        private TextSelection GetCurrentEditorSelection()
        {
            RichTextBox editor = this.CurrentEditor;

            if (editor != null)
                return editor.Selection;

            return null;
        }

        private Boolean SetFontFamily(FontFamily fontFamily)
        {
            RichTextBox editor = this.CurrentEditor;

            if (editor != null && fontFamily != null)
            {
                EditingCommandsEx.SelectFontFamily.Execute(
                    fontFamily, editor);

                editor.Focus();
                this.IsOverflowOpen = false;

                return true;
            }

            return false;
        }

        private Boolean SetFontSize(Object fontSize)
        {
            RichTextBox editor = this.CurrentEditor;
            Double? newSize = RichTextBoxToolBarHelper.GetSelectionFontSize(fontSize);

            if (editor != null && newSize != null)
            {
                EditingCommandsEx.SelectFontSize.Execute(
                    (Double)newSize, editor);

                editor.Focus();
                this.IsOverflowOpen = false;

                return true;
            }

            return false;
        }

        private Boolean SetColor(Object color, RoutedCommand cmd)
        {
            RichTextBox editor = this.CurrentEditor;

            Color? newColor = null;

            if (color is PropertyInfo)
            {
                newColor = ((PropertyInfo)color).GetValue(null) as Color?;
            }
            else if (color is Color)
            {
                newColor = (Color)color;
            }
            else
            {
                throw new ArgumentException("color");
            }

            if (cmd != null && editor != null && newColor != null)
            {
                cmd.Execute(
                    (Color)newColor, editor);

                editor.Focus();
                this.IsOverflowOpen = false;

                return true;
            }

            return false;
        }

        #endregion // Private methods

        #region Private properties and fields

        private static readonly PropertyChangedCallback UpdateToolItemsWhenPropertyChangedCallback =
            new PropertyChangedCallback(UpdateToolItemsWhenPropertyChanged);

        private readonly RoutedEventHandler EditorSelectionChangedEventHandler;
        private readonly KeyboardFocusChangedEventHandler BindingElementFocusChangedEventHandler;

        private readonly KeyboardFocusTrackingElement<RichTextBox> _focusedRichTextBoxTracker;

        private RadioButtonsWithoutChecksManager _radioButtonsManager;

        #endregion // Private properties and fields
    }
}
