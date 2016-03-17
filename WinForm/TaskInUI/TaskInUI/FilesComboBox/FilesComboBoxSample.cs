using System;
using System.IO;
using System.Windows.Forms;

namespace TaskInUI.FilesComboBox
{
    public partial class FilesComboBoxSample : Form
    {
        public FilesComboBoxSample()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(filesMaskTextBox.Text, maskTextBox.Text);
            
            filesComboBox.Items.Clear();
            foreach (string file in files)
            {
                filesComboBox.Items.Add(file);
            }
            filesComboBox.SelectedIndex = 0;
            filesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnOpenExplorer_Click(object sender, EventArgs e)
        {
            string selectedFilePath = (string) filesComboBox.SelectedItem;

            string argument = @"/select, " + selectedFilePath;
            System.Diagnostics.Process.Start("explorer.exe", argument);
        }
    }
}
