using LangApp.Admin.WPF.Views.PagesViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LangApp.Admin.WPF.Services
{
    public class AppNavigationService : IAppNavigationService
    {
        private readonly LanguagesPage _languagesPage;
        private readonly StagesPage _stagesPage;
        private readonly WordsPage _wordsPage;
        private readonly TranslationsPage _translationsPage;
        private Frame? _frame;

        public AppNavigationService(
            LanguagesPage languagesPage, 
            StagesPage stagesPage, 
            WordsPage wordsPage, 
            TranslationsPage translationsPage)
        {
            _languagesPage = languagesPage;
            _stagesPage = stagesPage;
            _wordsPage = wordsPage;
            _translationsPage = translationsPage;
        }
        public void Initialize(Frame frame)
        {
            _frame = frame;
        }
        public void NavigateToLanguagesPage()
        {
            _frame?.Navigate(_languagesPage);
        }

        public void NavigateToStagesPage()
        {
            _frame?.Navigate(_stagesPage);
        }

        public void NavigateToTranslationsPage()
        {
            _frame?.Navigate(_translationsPage);
        }

        public void NavigateToWordsPage()
        {
            _frame?.Navigate(_wordsPage);
        }
    }
}
