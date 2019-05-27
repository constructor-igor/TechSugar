using System;
using System.ComponentModel;
using System.Windows;

namespace LocalizationMarkupExtensionExample.Extensions
{

public class TranslationData : IWeakEventListener,
        INotifyPropertyChanged, IDisposable
    {
        private string _key;

        public TranslationData(string key)
        {
            _key = key;
            LanguageChangedEventManager.AddListener(
                TranslationManager.Instance, this);
        }

        ~TranslationData()
        {
            Dispose(false);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                LanguageChangedEventManager.RemoveListener(
                    TranslationManager.Instance, this);
            }
        }


        public object Value
        {
            get
            {
                return TranslationManager.Instance.Translate(_key);
            }
        }

        public bool ReceiveWeakEvent(Type managerType,
            object sender, EventArgs e)
        {
            if (managerType == typeof(LanguageChangedEventManager))
            {
                OnLanguageChanged(sender, e);
                return true;
            }
            return false;
        }

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Value"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
