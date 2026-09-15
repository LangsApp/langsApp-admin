using LangApp.Admin.WPF.DTOs.Requests;
using LangApp.Admin.WPF.Infrastructure;
using LangApp.Admin.WPF.Models;
using LangApp.Admin.WPF.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LangApp.Admin.WPF.ViewModels.WindowsViewModels
{
    public class AddLanguageWindowViewModel : INotifyPropertyChanged
    {
        private readonly ILanguageService _languageService;
        private ICommand? _closeAddLanguageWindowCommand;
        private ICommand? _addLanguage;
        private CancellationTokenSource? _cancellationTokenSource;
        private CreateLanguageDTO _newLanguage = new();

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? CloseAddLanguageWindowRequested;

        public AddLanguageWindowViewModel(ILanguageService languageService) 
        {
            _languageService = languageService;
        }

        public ICommand CloseAddLanguageWindowCommand =>
            _closeAddLanguageWindowCommand ??= new RelayCommand(CloseAddLanguageWindow);

        public ICommand AddLanguageCommand => _addLanguage ??= new AsyncRelayCommand(AddNewLanguageAsync);

        public async Task AddNewLanguageAsync(object? param)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            if (_newLanguage != null && !string.IsNullOrWhiteSpace(_newLanguage.Name) 
                && !string.IsNullOrWhiteSpace(_newLanguage.LangCode))
            {
                await _languageService.AddNewLanguageAsync(_newLanguage, _cancellationTokenSource.Token);
            }
        }

        public CreateLanguageDTO NewLanguage
        {
            get => _newLanguage;
            set
            {
                if(_newLanguage != value)
                {
                    _newLanguage = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public void CloseAddLanguageWindow(object? _)
        {
            CloseAddLanguageWindowRequested?.Invoke(this, EventArgs.Empty);
        }

        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
