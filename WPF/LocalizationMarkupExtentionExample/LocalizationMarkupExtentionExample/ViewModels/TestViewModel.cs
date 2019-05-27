using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Caliburn.Micro;
using LocalizationMarkupExtensionExample.Extensions;

namespace LocalizationMarkupExtensionExample.ViewModels
{
    public class TestViewModel : Screen
    {
        private int _messageCount;
        private CultureInfo _selectedLanguage;
        public string Message { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;


        public TestViewModel()
        {
            SelectedLanguage = Languages.Last();
        }

        public void SendCommand()
        {

        }

        public CultureInfo SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                if (Equals(value, _selectedLanguage))
                {
                    return;
                }
                _selectedLanguage = value;
                TranslationManager.Instance.CurrentLanguage = value;
                NotifyOfPropertyChange();
            }
        }
        public IEnumerable<CultureInfo> Languages { get { return TranslationManager.Instance.Languages; } }
        public int MessageCount
        {
            get { return _messageCount; }
            set
            {
                if (value == _messageCount)
                {
                    return;
                }
                _messageCount = value;
                NotifyOfPropertyChange();
            }
        }

        private async void Poll()
        {
//            while (true)
//            {
//                await Task.Delay(100);
////                using (var client = new Service1Client())
////                {
////                    MessageCount = client.GetMessageCount();
////                }
//            }
        }
    }
}
