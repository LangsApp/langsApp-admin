using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LangApp.Admin.WPF.Services
{
    public interface IAppNavigationService
    {
        void Initialize(Frame frame);
        void NavigateToLanguagesPage();
        void NavigateToStagesPage();
        void NavigateToWordsPage();
        void NavigateToTranslationsPage();
    }
}
