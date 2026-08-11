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
        private readonly LanguagesPage _languagesPage;

        public MainWindowViewModel(LanguagesPage languagesPage)
        {
            _languagesPage = languagesPage;
        }

        public ICommand OpenLanguagesPageCommand => _openLanguagesPageCommand ??= new RelayCommand(OpenLanguagesPage);

        public void OpenLanguagesPage(object? parameter)
        {
            if (parameter is Frame fr)
            {
                var langPage = _languagesPage;
                fr.Navigate(langPage);
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
