using LangApp.Admin.WPF.Infrastructure;
using LangApp.Admin.WPF.Services;
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
        private ICommand? _openCategoriesPageCommand;

        private readonly IAppNavigationService _navigationService;

        public MainWindowViewModel(
            IAppNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ICommand OpenLanguagesPageCommand => _openLanguagesPageCommand ??= new RelayCommand(OpenLanguagesPage);
        public ICommand OpenStagePageCommand => _openStagePageCommand ??= new RelayCommand(OpenStagePage);
        public ICommand OpenWordsPageCommand => _openWordsPageCommand ??= new RelayCommand(OpenWordsPage);
        public ICommand OpenTranslationsPageCommand => _openTranslationsPageCommand ??= new RelayCommand(OpenTranslationsPage);
        public ICommand OpenCategoriesPageCommand => _openCategoriesPageCommand ??= new RelayCommand(OpenCategoriesPage);

        public void OpenLanguagesPage(object? _)
        {
            _navigationService.NavigateToLanguagesPage();
        }

        public void OpenStagePage(object? _)
        {
            _navigationService.NavigateToStagesPage();
        }

        public void OpenWordsPage(object? _)
        {
            _navigationService.NavigateToWordsPage();
        }

        public void OpenTranslationsPage(object? _)
        {
            _navigationService.NavigateToTranslationsPage();
        }

        public void OpenCategoriesPage(object? _)
        {
            _navigationService.NavigateToCategoriesPage();
        }

        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
