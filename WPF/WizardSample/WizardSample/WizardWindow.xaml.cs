using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WizardSample
{
    /// <summary>
    /// Interaction logic for WizardWindow.xaml
    /// </summary>
    public partial class WizardWindow : Window
    {
        public WizardWindow()
        {
            InitializeComponent();
        }

        private void FirstNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Page1.CanSelectNextPage = !string.IsNullOrEmpty(FirstNameTextBox.Text);
        }

        private void LastNameTextBox_OnTextChangedNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Page1.CanSelectNextPage = !string.IsNullOrEmpty(LastNameTextBox.Text);
        }
    }
}
