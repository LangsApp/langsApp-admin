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
    public class EditStageWindowViewModel : INotifyPropertyChanged
    {
        private readonly IStageService _stageService;
        private ICommand? _closeEditStageWindowCommand;
        private ICommand? _saveEditedStageCommand;
        private CancellationTokenSource? _cancellationTokenSource;
        private EditStageDTO _editStageDTO = new();
        private string _newName = string.Empty;
        private string _newOrder = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? CloseEditStageWindowRequested;

        public EditStageWindowViewModel(IStageService stageService)
        {
            _stageService = stageService;
        }

        public ICommand CloseEditStageWindowCommand =>
            _closeEditStageWindowCommand ??= new RelayCommand(CloseEditStageWindow);
        public ICommand SaveEditedStageCommand =>
            _saveEditedStageCommand ??= new AsyncRelayCommand(SaveEditedStageAsync);

        public Stage? StageToEdit { get; set; }
        public string NewName
        {
            get => _newName;
            set
            {
                if(_newName != value)
                {
                    _newName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string NewOrder
        {
            get => _newOrder;
            set
            {
                if(_newOrder != value)
                {
                    _newOrder = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public async Task SaveEditedStageAsync(object? _)
        {
            if(CheckToSave(_newName, _newOrder, StageToEdit))
            {
                int orderDTO;
                if(string.IsNullOrWhiteSpace(_newOrder))
                {
                    orderDTO = StageToEdit!.Order;
                }
                else
                {
                    if(!int.TryParse(_newOrder, out orderDTO))
                    {
                        orderDTO = StageToEdit!.Order;
                    }
                }

                    _cancellationTokenSource = new CancellationTokenSource();
                _editStageDTO = new EditStageDTO
                {
                    Id = StageToEdit!.Id, 
                    NameToEdit = StageToEdit!.Name,
                    EditedOrder = orderDTO,
                    EditedName = string.IsNullOrWhiteSpace(_newName)
                        ? StageToEdit.Name
                        : _newName
                };

                await _stageService.EditStageAsync(_editStageDTO,  _cancellationTokenSource.Token);
            }
            else
            {// improve
                return;
            }

        }

        public void CloseEditStageWindow(object? _)
        {
            CloseEditStageWindowRequested?.Invoke(this, EventArgs.Empty);
        }

        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private bool CheckToSave(string newName, string newOrder, Stage? selectedStage)
        {
            if(selectedStage == null)
            {
                return false;
            }
            else if (string.IsNullOrWhiteSpace(newName) && string.IsNullOrWhiteSpace(newOrder))
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
