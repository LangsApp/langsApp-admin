using LangApp.Admin.WPF.DTOs.Requests;
using LangApp.Admin.WPF.Infrastructure;
using LangApp.Admin.WPF.Services.Interfaces;
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
    public class AddTranslateWindowViewModel : INotifyPropertyChanged
    {
        private readonly ITranslatesService _translateService;
        private ICommand? _closeAddTranslateWindowCommand;
        private ICommand? _addTranslate;
        private CancellationTokenSource _cancellationTokenSource;
        private CreateTranslateDTO _newTranslate = new();

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? CloseAddTranslateWindowRequested;

        public AddTranslateWindowViewModel(ITranslatesService translatesService)
        {
            _translateService = translatesService;
        }

        public ICommand CloseAddTranslateWindowCommand =>
            _closeAddTranslateWindowCommand ??= new RelayCommand(CloseAddTranslateWindow);

        public CreateTranslateDTO NewTranslate
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

        public void CloseAddTranslateWindow(object? _)
        {
            CloseAddTranslateWindowRequested?.Invoke(this, EventArgs.Empty);
        }

        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
