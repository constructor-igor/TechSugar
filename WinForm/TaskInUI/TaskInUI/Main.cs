using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskInUI
{
    public partial class MainForm : Form
    {
        private CancellationTokenSource m_cts;
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Service service = new Service();
            int expectedDuration = 5;
            ToOutput("[Started] Worker ({0} seconds)", expectedDuration);

            button1.Enabled = false;
            Task.Factory.StartNew(() => service.Do(expectedDuration))
                .ContinueWith(data =>
                {
                    ToOutput("[Completed] Worker");
                    button1.Enabled = true;
                }, CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void ToOutput(string text, params object[] arguments)
        {
            outputTextBox.AppendText(String.Format(text, arguments) + Environment.NewLine);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Service service = new Service();
            int expectedDuration = 10;            
            ToOutput("[Started] Worker with progress ({0} seconds)", expectedDuration);

            m_cts = new CancellationTokenSource();            

            button2.Enabled = false;
            Task.Factory.StartNew(() => service.DoWithProgress(expectedDuration,
                progress =>
                {
                    Invoke((Action) delegate
                    {
                        ToOutput("[Progress] Worker with progress ({0})", progress);
                    });
                }, m_cts.Token), m_cts.Token)
                .ContinueWith(data =>
                {
                    if (m_cts.IsCancellationRequested)
                        ToOutput("[Cancelled] Worker with progress");
                    ToOutput("[Completed] Worker with progress");
                    m_cts.Dispose();
                    m_cts = null;
                    button2.Enabled = true;
                }, CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (m_cts != null)
                m_cts.Cancel();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string textFileContent = Resource.TextFileSample;
            ToOutput(textFileContent);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Service service = new Service();
            int expectedDuration = 15;
            ToOutput("[Started] Worker without any progress/events ({0} seconds)", expectedDuration);
            button6.Enabled = false;

            m_cts = new CancellationTokenSource();

            Task workingTask = Task.Factory.StartNew(() => service.DoWithoutProgress(expectedDuration));

            Task progressTask = Task.Factory.StartNew(() => service.DoLoopedProgress(
                procent => { Invoke((Action) delegate { ToOutput("[Progress] Working process ({0}%)", procent); }); },
                workingTask,
                m_cts.Token
                )).ContinueWith(data =>
                {
                    m_cts.Dispose();
                    m_cts = null;
                    button6.Enabled = true;        
                }, CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (m_cts != null)
                m_cts.Cancel();
        }
    }
}
