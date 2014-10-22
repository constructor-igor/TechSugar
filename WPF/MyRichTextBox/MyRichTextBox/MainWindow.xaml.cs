using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyRichTextBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //this._rttb1.TargetEditor = this._rtb;
            test.Text = "Test";
            tracker = new KeyboardFocusTrackingElement<TextBox>(this, true);
            tracker.KeyboardFocusChanged += tracker_KeyboardFocusChanged;
            tracker.Bind();

            tracker.CurrentElement = testTB;
            tracker.BindingToRoot = false;
            tracker.HiddenElements = new FrameworkElement[] { testTB1, _rttb2 };
        }

        void tracker_KeyboardFocusChanged(object sender, KeyboardFocusChangedEventArgs e)
        {
            String s;
            if (e.NewFocus == null)
                s = "<NO>" + " - " + (++counter).ToString();
            else
                s = ((TextBox)e.NewFocus).Text + " - " + (++counter).ToString();

            test.Items.Add(s);
            test.Text = s;
        }

        private int counter = 0;
        private KeyboardFocusTrackingElement<TextBox> tracker;
    }
}
