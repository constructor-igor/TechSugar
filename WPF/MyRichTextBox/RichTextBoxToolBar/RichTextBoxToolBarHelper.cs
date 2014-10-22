using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace RichTextBoxToolBar
{
    static class RichTextBoxToolBarHelper
    {
        #region Applying new style

        internal static void ToggleSelectionFormattingProperty<T>(
            TextSelection selection, DependencyProperty formattingProperty,
            T valueOn, T valueOff, Func<T, T, Boolean> equals)
        {
            Object propertyValue = GetSelectionPropertyValue(selection, formattingProperty);

            if (propertyValue is T)
            {
                propertyValue =
                    equals((T)propertyValue, valueOn)
                    ? valueOff
                    : valueOn;
            }
            else if (propertyValue == null
                || propertyValue == DependencyProperty.UnsetValue)
            {
                propertyValue = valueOn;
            }
            else
            {
                throw new InvalidOperationException();
            }

            selection.ApplyPropertyValue(formattingProperty, propertyValue);
        }

        internal static void ApplyNewValueToFormattingProperty<T>(
            TextSelection selection, DependencyProperty formattingProperty,
            T newValue, Func<T, T, Boolean> equals)
        {
            Object propertyValue = GetSelectionPropertyValue(selection, formattingProperty);

            Boolean shouldUpdate;
            if (propertyValue == null)
                shouldUpdate = (Object)newValue != null;
            else if (propertyValue == DependencyProperty.UnsetValue)
                shouldUpdate = (Object)newValue != DependencyProperty.UnsetValue;
            else if (propertyValue is T)
                shouldUpdate = !equals((T) propertyValue, newValue);
            else
                throw new InvalidOperationException();

            if (shouldUpdate)
                selection.ApplyPropertyValue(formattingProperty, newValue);
        }

        #endregion // Applying new style

        #region Update state

        internal static void UpdateSelectionFontFamily(
            ComboBox fontFamilyItems, TextSelection selection)
        {
            Object propertyValue = GetSelectionPropertyValue(
                selection, Paragraph.FontFamilyProperty);

            if (propertyValue == null
                || propertyValue == DependencyProperty.UnsetValue)
                fontFamilyItems.Text = String.Empty;
            else
                fontFamilyItems.SelectedIndex =
                    fontFamilyItems.Items.IndexOf(propertyValue);
        }

        internal static void UpdateSelectionFontSize(
            ComboBox fontSizeItems, TextSelection selection)
        {
            Object propertyValue = GetSelectionPropertyValue(
                selection, Paragraph.FontSizeProperty);

            fontSizeItems.Text = (propertyValue == null
                || propertyValue == DependencyProperty.UnsetValue)
                ? String.Empty
                : propertyValue.ToString();
        }

        internal static void UpdateSelectionItemCheckedState(
            ToggleButton button, TextSelection selection,
            DependencyProperty formattingProperty, object expectedValue)
        {
            Object propertyValue = GetSelectionPropertyValue(
                selection, formattingProperty); ;

            button.IsChecked = propertyValue != null
                && propertyValue != DependencyProperty.UnsetValue
                && propertyValue != DependencyProperty.UnsetValue
                && propertyValue.Equals(expectedValue);
        }

        internal static void UpdateSelectionColor(
            ComboBox source, TextSelection selection,
            DependencyProperty property, Color defaultColor)
        {

            Color? currentColor = selection == null ? null
                : GetSelectionColor(
                GetSelectionPropertyValue(selection, property),
                defaultColor);

            if (currentColor != null)
            {
                PropertyInfo selectedColor = source.ItemsSource
                    .Cast<PropertyInfo>()
                    .FirstOrDefault(item => (Color)item.GetValue(null) == (Color)currentColor);

                if (selectedColor != null)
                {
                    source.SelectedIndex = source.Items
                        .IndexOf(selectedColor);
                    return;
                }
            }
            source.Text = String.Empty;
        }

        internal static void UpdateSelectionListType(ToggleButton button, TextSelection selection, TextMarkerStyle expectedValue)
        {
            Paragraph startParagraph, endParagraph;

            if (selection != null)
            {
                startParagraph = selection.Start.Paragraph;
                endParagraph = selection.End.Paragraph;
            }
            else
            {
                startParagraph = endParagraph = null;
            }

            if (startParagraph != null && endParagraph != null
                && (startParagraph.Parent is ListItem)
                && (endParagraph.Parent is ListItem)
                && Object.ReferenceEquals(((ListItem)startParagraph.Parent).List, ((ListItem)endParagraph.Parent).List))
            {
                TextMarkerStyle markerStyle = ((ListItem) startParagraph.Parent).List.MarkerStyle;

                button.IsChecked = markerStyle == expectedValue;

                return;
            }

            button.IsChecked = false;
        }

        #endregion // Update state

        #region Tools

        internal static Object GetSelectionPropertyValue(TextSelection selection, DependencyProperty formattingProperty)
        {
            // This is necessary due to the emergence of unexpected errors 
            // when pasting text from other editors, such as a Microsoft Word

            Object propertyValue;
            try
            {
                propertyValue = selection.GetPropertyValue(formattingProperty);
            }
            catch (Exception) // including NullReferenceException if selection is null
            {
                propertyValue = null;
            }

            return propertyValue;
        }

        internal static Color? GetSelectionColor(Object o, Color defaultColor)
        {
            if (o == null)
                return defaultColor;

            if (o == DependencyProperty.UnsetValue)
                return null;

            if (o is SolidColorBrush)
                return ((SolidColorBrush)o).Color;

            return o as Color?;
        }

        internal static Double? GetSelectionFontSize(Object o)
        {
            Double value = Double.NaN;

            if (o is Double)
            {
                value = (Double)o;
            }
            else if (o is String)
            {
                if (!Double.TryParse((String)o, out value))
                    value = Double.NaN;
            }

            if (value > 0 && value < Double.MaxValue)
                return value;

            return null;
        }

        #endregion // Tools
    }
}
