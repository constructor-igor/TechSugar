using System;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using WpfUserControlLibrary;

namespace WpfInWinFormApp
{
    //
    //https://stackoverflow.com/questions/9088495/how-to-use-caliburn-micro-in-a-winforms-app-with-one-wpf-form
    //
    public partial class WinMainForm : Form
    {
        public WinMainForm()
        {
            InitializeComponent();
        }

        private void WinMainForm_Load(object sender, EventArgs e)
        {
            ElementHost ctrlHost = new ElementHost {Dock = DockStyle.Fill};
            panel1.Controls.Add(ctrlHost);

            new Bootstraper(ctrlHost);

//            UserControl1 wpfUserControl = new UserControl1();
//            wpfUserControl.InitializeComponent();
//            ctrlHost.Child = wpfUserControl;
        }
    }
}
