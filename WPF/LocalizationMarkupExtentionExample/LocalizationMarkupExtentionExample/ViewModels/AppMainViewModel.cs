using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using LocalizationMarkupExtensionExample.Extensions;

namespace LocalizationMarkupExtensionExample.ViewModels
{
    public class AppMainViewModel : Screen
    {
        public TestViewModel TestViewModel { get; set; }
        public AppMainViewModel()
        {
            //TranslationManager.Instance.CurrentLanguage = CultureInfo.CurrentCulture;
            TestViewModel = new TestViewModel();
        }
    }
}
