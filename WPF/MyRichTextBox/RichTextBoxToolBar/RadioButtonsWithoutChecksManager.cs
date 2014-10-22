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
    internal class RadioButtonsWithoutChecksManager
    {
        public RadioButtonsWithoutChecksManager(IEnumerable<RadioButton> radioButtons)
        {
            if (radioButtons == null)
                throw new ArgumentNullException("radioButtons");

            this._radioButtons = radioButtons;
        }

        public void Bind()
        {
            BindEventHandlers();
        }

        public void Unbind()
        {
            UnbindEventHandlers();
        }

        private void BindEventHandlers()
        {
            foreach (RadioButton button in this._radioButtons)
            {
                Delegate clickEventHandler =
                    button.IsChecked == true
                    ? CheckedRadioButtonEventHandler
                    : UncheckedRadioButtonEventHandler;

                button.AddHandler(RadioButton.ClickEvent,
                    clickEventHandler);
                button.Tag = clickEventHandler;
            }
        }

        private void UnbindEventHandlers()
        {
            foreach (RadioButton button in this._radioButtons)
            {
                button.RemoveHandler(RadioButton.ClickEvent,
                    button.IsChecked == true
                    ? CheckedRadioButtonEventHandler
                    : UncheckedRadioButtonEventHandler);
            }
        }

        private static void UncheckedRadioButtonEvent(Object sender, RoutedEventArgs e)
        {
            ToggleButton button = (ToggleButton)sender;

            button.RemoveHandler(
                RadioButton.ClickEvent, UncheckedRadioButtonEventHandler);
            button.AddHandler(
                RadioButton.ClickEvent, CheckedRadioButtonEventHandler);

            button.IsChecked = true;
        }

        private static void CheckedRadioButtonEvent(Object sender, RoutedEventArgs e)
        {
            ToggleButton button = (ToggleButton)sender;


            button.RemoveHandler(
                RadioButton.ClickEvent, CheckedRadioButtonEventHandler);
            button.AddHandler(
                RadioButton.ClickEvent, UncheckedRadioButtonEventHandler);

            button.IsChecked = false;
        }

        private static readonly RoutedEventHandler UncheckedRadioButtonEventHandler =
            new RoutedEventHandler(UncheckedRadioButtonEvent);
        private static readonly RoutedEventHandler CheckedRadioButtonEventHandler =
            new RoutedEventHandler(CheckedRadioButtonEvent);

        private readonly IEnumerable<RadioButton> _radioButtons;
    }
}
