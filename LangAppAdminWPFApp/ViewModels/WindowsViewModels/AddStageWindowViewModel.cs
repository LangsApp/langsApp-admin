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
    public class AddStageWindowViewModel : INotifyPropertyChanged
    {
        private readonly IStageService _stageService;
        private ICommand? _closeAddStageWindowCommand;
        private ICommand? _addStageCommand;
        private CancellationTokenSource? _cancellationTokenSource;
        private CreateStageDTO _newStage = new();

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? CloseAddStageWindowRequested;

        public AddStageWindowViewModel(IStageService stageService)
        {
            _stageService = stageService;
        }

        public ICommand CloseAddStageWindowCommand =>
            _closeAddStageWindowCommand ??= new RelayCommand(CloseAddStageWindow);

        public ICommand AddStageCommand => _addStageCommand ??= new AsyncRelayCommand(AddNewStageAsync);

        public async Task AddNewStageAsync(object? param)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            if (_newStage != null && !string.IsNullOrWhiteSpace(_newStage.Name)
                && !int.IsNegative(_newStage.Order))
            {
                await _stageService.AddNewStageAsync(_newStage, _cancellationTokenSource.Token);
            }
        }

        public CreateStageDTO NewStage
        {
            get => _newStage;
            set
            {
                if(_newStage != value)
                {
                    _newStage = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public void CloseAddStageWindow(object? _)
        {
            CloseAddStageWindowRequested?.Invoke(this, EventArgs.Empty);
        }

        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
