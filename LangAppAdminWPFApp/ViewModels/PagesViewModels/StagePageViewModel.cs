using LangApp.Admin.WPF.Infrastructure;
using LangApp.Admin.WPF.Models;
using LangApp.Admin.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LangApp.Admin.WPF.ViewModels.PagesViewModels
{
    public class StagePageViewModel : INotifyPropertyChanged
    {
        private readonly IStageService _stageService;
        private readonly IDialogService _dialogService;
        private CancellationTokenSource? _loadCancellationTokenSource;
        private const int PageSize = 5;
        private List<Stage> _allStages = [];
        private PaginateViewModel<Stage> _paginateViewModel = new(0, 1, PageSize);
        private ICommand? _openAddStageCommand;
        private ICommand? _openEditStageCommand;
        private ICommand? _deleteStageCommand;

        public int CurrentPage => _paginateViewModel.PaginateNumber;
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<Stage> Stages => _paginateViewModel.PageCollection;

        public StagePageViewModel(IStageService stageService, IDialogService dialogService)
        {
            _paginateViewModel.PageChanged += OnPageChanged;
            _stageService = stageService;
            _dialogService = dialogService;

            PreviousPageCommand = new RelayCommand(
                _ => _paginateViewModel.ShowPage(CurrentPage - 1),
                _ => _paginateViewModel.HasPreviousPage);
            
            NextPageCommand = new RelayCommand(
                _ => _paginateViewModel.ShowPage(CurrentPage + 1),
                _ => _paginateViewModel.HasNextPage);
        }

        public string PageInfo
        {
            get
            {
                return _paginateViewModel.PageInfo("stages");
            }
        }

        public ICommand PreviousPageCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand OpenAddStageCommand => _openAddStageCommand ??= new RelayCommand(OpenAddStageWindow);
        public ICommand OpenEditStageCommand => _openEditStageCommand
            ??= new RelayCommand(OpenEditStageWindow, CanOpenEditStageWindow);
        public ICommand DeleteStageCommand => _deleteStageCommand
            ??= new RelayCommand(DeleteStage, CanDeleteStage);

        public async Task LoadStagesAsync()
        {
            _loadCancellationTokenSource?.Cancel();
            _loadCancellationTokenSource?.Dispose();
            _loadCancellationTokenSource = new CancellationTokenSource();

            _allStages = await _stageService.GetStagesAsync(_loadCancellationTokenSource.Token);
            foreach (var stage in _allStages)
            {
                if (stage.Published)
                {
                    stage.CanChangePublished = false;
                }
                if (stage.Reviewed)
                {
                    stage.CanChangeReviewed = false;
                }
            }

            _paginateViewModel.GetCollection(_allStages);
            _paginateViewModel.ShowPage(1);
        }

        public void OpenAddStageWindow(object? _)
        {
            _dialogService.OpenAddStageDialog();
        }

        private void OnPageChanged(object? sender, EventArgs e)
        {
            NotifyPropertyChanged(nameof(CurrentPage));
            NotifyPropertyChanged(nameof(PageInfo));

            CommandManager.InvalidateRequerySuggested();
        }

        private bool CanOpenEditStageWindow(object? param)
        {
            return param is Stage stage && !stage.Published;
        }
        private bool CanDeleteStage(object? param)
        {
            return param is Stage stage && !stage.Published;
        }

        private void OpenEditStageWindow(object? param)
        {
            if(param is not Stage selectedStage)
            {
                return;
            }
            _dialogService.OpenEditStageDialog(selectedStage);
        }

        private void DeleteStage(object? param)
        {
            if (param is not Stage stage)
            {
                return;
            }
            var result = _dialogService.ConfirmAction("Are you sure to delete ",
                stage.Name, "Confirm deletion");

            if(result)
            {
                _stageService.DeleteStageAsync(stage.Id);
                _allStages.Remove(stage);
                _paginateViewModel.GetCollection(_allStages);
                _paginateViewModel.ShowPage(CurrentPage);
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
