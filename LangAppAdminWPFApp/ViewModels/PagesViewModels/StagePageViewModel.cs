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
        private CancellationTokenSource? _loadCancellationTokenSource;
        private const int PageSize = 5;
        private List<Stage> _allStages = [];
        private PaginateViewModel<Stage> _paginateViewModel = new(0, 1, PageSize);

        public int CurrentPage => _paginateViewModel.PaginateNumber;
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<Stage> Stages => _paginateViewModel.PageCollection;

        public StagePageViewModel(IStageService stageService)
        {
            _paginateViewModel.PageChanged += OnPageChanged;
            _stageService = stageService;

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


        public async Task LoadStagesAsync()
        {
            _loadCancellationTokenSource?.Cancel();
            _loadCancellationTokenSource?.Dispose();
            _loadCancellationTokenSource = new CancellationTokenSource();

            _allStages = await _stageService.GetStagesAsync(_loadCancellationTokenSource.Token);

            _paginateViewModel.GetCollection(_allStages);
            _paginateViewModel.ShowPage(1);
        }

        private void OnPageChanged(object? sender, EventArgs e)
        {
            NotifyPropertyChanged(nameof(CurrentPage));
            NotifyPropertyChanged(nameof(PageInfo));

            CommandManager.InvalidateRequerySuggested();
        }
        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
