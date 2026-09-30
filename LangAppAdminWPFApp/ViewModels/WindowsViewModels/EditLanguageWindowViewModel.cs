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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LangApp.Admin.WPF.ViewModels.WindowsViewModels
{
    public class EditLanguageWindowViewModel : INotifyPropertyChanged
    {
        private readonly ILanguageService _languageService;
        private ICommand? _closeEditLanguageWindowCommand;
        private ICommand? _saveEditedLanguageCommand;
        private CancellationTokenSource? _cancellationTokenSource;
        private EditLanguageDTO _editLanguageDTO = new();
        private string _newName = string.Empty;
        private string _newLangCode = string.Empty;


        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? CloseEditLanguageWindowRequested;

        public EditLanguageWindowViewModel(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        public ICommand CloseEditLanguageWindowCommand =>
            _closeEditLanguageWindowCommand ??= new RelayCommand(CloseEditLanguageWindow);

        public ICommand SaveEditedLanguageCommand =>
            _saveEditedLanguageCommand ??= new AsyncRelayCommand(SaveEditedLanguageAsync);

        public async Task SaveEditedLanguageAsync(object? _)
        {
            if (CheckToSave(_newLangCode, _newName, LanguageToEdit))
            {
                _cancellationTokenSource = new CancellationTokenSource();
                _editLanguageDTO = new EditLanguageDTO
                {
                    LangCodeToEdit = LanguageToEdit!.LangCode,
                    EditedLangCode = string.IsNullOrWhiteSpace(_newLangCode)
                        ? LanguageToEdit.LangCode
                        : _newLangCode,

                    EditedName = string.IsNullOrWhiteSpace(_newName)
                        ? LanguageToEdit.Name
                        : _newName
                };

                await _languageService.EditLanguageAsync(_editLanguageDTO, _cancellationTokenSource.Token);
            }
            else
            {// improve
                return;
            }
        }

        public string NewName
        {
            get => _newName;

            set
            {
                if (_newName != value)
                {
                    _newName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string NewLangCode
        {
            get => _newLangCode;

            set
            {
                if (_newLangCode != value)
                {
                    _newLangCode = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Language? LanguageToEdit { get; set; }

        public void CloseEditLanguageWindow(object? _)
        {
            CloseEditLanguageWindowRequested?.Invoke(this, EventArgs.Empty);
        }
        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private bool CheckToSave(string newLangCode, string newName, Language? language)
        {
            if(language == null)
            {
                return false;
            }
            else if(string.IsNullOrWhiteSpace(newLangCode) && string.IsNullOrWhiteSpace(newName))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
