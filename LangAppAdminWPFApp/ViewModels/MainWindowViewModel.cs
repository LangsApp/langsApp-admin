using LangApp.Admin.WPF.Infrastructure;
using LangApp.Admin.WPF.Views.PagesViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace LangApp.Admin.WPF.ViewModels
{
    public sealed class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ICommand? _openLanguagesPageCommand;
        private ICommand? _openStagePageCommand;
        private ICommand? _openWordsPageCommand;
        private ICommand? _openTranslationsPageCommand;

        private readonly LanguagesPage _languagesPage;
        private readonly StagesPage _stagesPage;
        private readonly WordsPage _wordsPage;
        private readonly TranslationsPage _translationPage;

        public MainWindowViewModel(
            LanguagesPage languagesPage, 
            StagesPage stagesPage,
            WordsPage wordsPage,
            TranslationsPage translationsPage)
        {
            _languagesPage = languagesPage;
            _stagesPage = stagesPage;
            _wordsPage = wordsPage;
            _translationPage = translationsPage;
        }

        public ICommand OpenLanguagesPageCommand => _openLanguagesPageCommand ??= new RelayCommand(OpenLanguagesPage);
        public ICommand OpenStagePageCommand => _openStagePageCommand ??= new RelayCommand(OpenStagePage);
        public ICommand OpenWordsPageCommand => _openWordsPageCommand ??= new RelayCommand(OpenWordsPage);
        public ICommand OpenTranslationsPageCommand => _openTranslationsPageCommand ??= new RelayCommand(OpenTranslationsPage);

        public void OpenLanguagesPage(object? parameter)
        {
            if (parameter is Frame fr)
            {
                var langPage = _languagesPage;
                fr.Navigate(langPage);
            }
        }

        public void OpenStagePage(object? parameter)
        {
            if (parameter is Frame fr)
            {
                var stagePage = _stagesPage;
                fr.Navigate(stagePage);
            }
        }

        public void OpenWordsPage(object? parameter)
        {
            if (parameter is Frame fr)
            {
                var wordsPage = _wordsPage;
                fr.Navigate(wordsPage);
            }
        }

        public void OpenTranslationsPage(object? parameter)
        {
            if (parameter is Frame fr)
            {
                var translationPage = _translationPage;
                fr.Navigate(translationPage);
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
