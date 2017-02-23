using System;
using System.Windows.Forms;

namespace FileSaveDialogIssue
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.saveFileDialog.AddExtension = false;
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            saveFileDialog.Title = @"Save calculated image";
            saveFileDialog.FileName = "noname.tiff";
            saveFileDialog.Filter = @"My Files(*.BMP;*.MGM;*.PNG)|*.BMP;*.MGM;*.PNG|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                MessageBox.Show(String.Format("File name {0}", saveFileDialog.FileName));
            }
        }
    }
}
