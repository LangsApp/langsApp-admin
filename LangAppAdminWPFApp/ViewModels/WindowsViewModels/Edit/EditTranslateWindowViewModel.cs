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
    public class EditTranslateWindowViewModel : INotifyPropertyChanged
    {
        private readonly ITranslatesService _translateService;
        private ICommand? _closeEditTranslateWindowCommand;
        private ICommand? _saveEditedTranslateCommand;
        private CancellationTokenSource? _cancellationTokenSource;
        private EditTranslateDTO _editTranslateDTO = new();
        private string _newTranslate = string.Empty;


        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? CloseEditTranslateRequested;

        public EditTranslateWindowViewModel(ITranslatesService translatesService)
        {
            _translateService = translatesService;
        }

        public ICommand CloseEditTranslateWindowCommand =>
            _closeEditTranslateWindowCommand ??= new RelayCommand(CloseEditTranslateWindow);

        public ICommand SaveEditedTranslateCommand =>
            _saveEditedTranslateCommand ??= new AsyncRelayCommand(SaveEditedTranslateAsync);

        public async Task SaveEditedTranslateAsync(object? _)
        {
            if(!string.IsNullOrWhiteSpace(_newTranslate) && TranslateToEdit != null)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                _editTranslateDTO = new EditTranslateDTO
                {
                    TranslateToEdit = TranslateToEdit.NormalizedTranslatedWord,
                    EditedTranslate = _newTranslate
                };
                await _translateService.UpdateTranslatesAsync(_editTranslateDTO, _cancellationTokenSource.Token);
            }
            else
            {
                return;
            }
        }

        public string NewTranslate
        {
            get => _newTranslate;
            set
            {
                if(_newTranslate != value)
                {
                    _newTranslate = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Translate? TranslateToEdit { get; set; }

        public void CloseEditTranslateWindow(object? _)
        {
            CloseEditTranslateRequested?.Invoke(this, new EventArgs());
        }

        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
