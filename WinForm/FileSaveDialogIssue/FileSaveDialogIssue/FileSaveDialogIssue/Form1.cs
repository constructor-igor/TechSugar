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
            this.saveFileDialog.SupportMultiDottedExtensions = false;
            this.saveFileDialog.CheckFileExists = false;
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            saveFileDialog.Title = @"Save calculated image";
            saveFileDialog.FileName = "noname.tiff";
            //saveFileDialog.Filter = @"My Files(*.BMP;*.MGM;*.PNG)|*.BMP;*.MGM;*.PNG|All files (*.*)|*.*";
            saveFileDialog.Filter = @"My Files(*.BMP;*.MGM;*.PNG)|*.BMP;*.MGM;*.PNG|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                MessageBox.Show(String.Format("File name {0}", saveFileDialog.FileName));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog.Title = @"Save calculated image";
            saveFileDialog.FileName = "noname.tiff";
            saveFileDialog.Filter = @"My Files(*.BMP;*.MGM;*.PNG)|*.bmp;*.mgm;*.png|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                MessageBox.Show(String.Format("File name {0}", saveFileDialog.FileName));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog.Title = @"Save calculated image";
            saveFileDialog.FileName = "noname.tiff";
            saveFileDialog.Filter = @"Bmp file(*.BMP)|*.bmp;|MGM file(*.MGM)|*.mgm;|PNG file(*.PNG)|*.png|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                MessageBox.Show(String.Format("File name {0}", saveFileDialog.FileName));
            }
        }
    }
}
