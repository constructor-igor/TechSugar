using System;
using System.Windows.Forms;

namespace TaskInUI.BigSmallWindowsSample
{
    public enum BigWindowCommand { Close, CreateSmall }
    public partial class BigModalWindow : Form
    {
        public BigWindowCommand Command { get; set; }
        public BigModalWindow()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Command = BigWindowCommand.Close;
            Close();
        }

        private void buttonCreateSmall_Click(object sender, EventArgs e)
        {
            Command = BigWindowCommand.CreateSmall;            
            Close();
        }
    }
}
