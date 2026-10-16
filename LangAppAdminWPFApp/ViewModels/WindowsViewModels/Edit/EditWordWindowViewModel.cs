using LangApp.Admin.WPF.DTOs.Requests;
using LangApp.Admin.WPF.Infrastructure;
using LangApp.Admin.WPF.Models;
using LangApp.Admin.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LangApp.Admin.WPF.ViewModels.WindowsViewModels.Edit
{
    public class EditWordWindowViewModel : INotifyPropertyChanged
    {
        private readonly IWordsService _wordsService;
        private ICommand? _closeEditWordWindowCommand;
        private ICommand? _saveEditedWordCommand;
        private CancellationTokenSource? _cancellationTokenSource;
        private EditWordDTO _editWordDTO = new();
        private string _editedWord = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? CloseEditWordWindowRequested;

        public EditWordWindowViewModel(IWordsService wordsService)
        {
            _wordsService = wordsService;
        }



        public ICommand CloseEditWordWindowCommand =>
            _closeEditWordWindowCommand ??= new RelayCommand(CloseEditWordWindow);

        public ICommand SaveEditedWordCommand =>
            _saveEditedWordCommand ??= new AsyncRelayCommand(SaveEditedWordAsync);

        public async Task SaveEditedWordAsync(object? _)
        {
            if(CheckToSave(_editedWord, BaseWordToEdit))
            {
                _cancellationTokenSource = new CancellationTokenSource();
                _editWordDTO = new EditWordDTO
                {
                    Id = BaseWordToEdit!.Id,
                    WordToEdit = BaseWordToEdit!.NormalizedWord,
                    EditedWord = string.IsNullOrWhiteSpace(_editedWord)
                    ? BaseWordToEdit.NormalizedWord
                    : _editedWord,
                };
                await _wordsService.EditWordAsync(_editWordDTO, _cancellationTokenSource.Token);
            }
            else
            {
                //improve
                return;
            }
        }

        public string NewWord
        {
            get => _editedWord;
            set
            {
                if (_editedWord != value)
                {
                    _editedWord = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public BaseWord? BaseWordToEdit { get; set; }


        public void CloseEditWordWindow(object? _)
        {
            CloseEditWordWindowRequested?.Invoke(this, new EventArgs());
        }
        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private bool CheckToSave(string newWord, BaseWord? baseWord)
        {
            if (baseWord == null)
            {
                return false;
            }
            else if (string.IsNullOrWhiteSpace(newWord))
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
