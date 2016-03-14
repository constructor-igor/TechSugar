using System.Windows.Forms;

namespace TaskInUI.BigSmallWindowsSample
{
    public class BigSmallWindowsService
    {
        public void Run()
        {
            BigWindowCommand command;
            do {
                using (BigModalWindow bigModalWindow = new BigModalWindow())
                {
                    DialogResult result = bigModalWindow.ShowDialog();
                    command = bigModalWindow.Command;
                }

                switch (command)
                {
                    case BigWindowCommand.CreateSmall:
                        using (SmallModalWindow smallModalWindow = new SmallModalWindow())
                        {
                            smallModalWindow.ShowDialog();
                        }
                        break;
                }
            } while (command == BigWindowCommand.CreateSmall);
        }
    }
}