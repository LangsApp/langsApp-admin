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
    public class CategoriesPageViewModel : INotifyPropertyChanged
    {
        private readonly ICategoryService _categoryService;
        private CancellationTokenSource? _loadCancellationTokenSource;
        private const int PageSize = 5;
        private List<Category> _allCategories = [];
        private PaginateViewModel<Category> _pagenateViewModel = new(0, 1, PageSize);

        public int CurrentPage => _pagenateViewModel.PaginateNumber;
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<Category> Categories => _pagenateViewModel.PageCollection;

        public CategoriesPageViewModel(ICategoryService categoryService)
        {
            _pagenateViewModel.PageChanged += OnPageChanged;

            _categoryService = categoryService;

            PreviousPageCommand = new RelayCommand(
            _ => _pagenateViewModel.ShowPage(CurrentPage - 1),
            _ => _pagenateViewModel.HasPreviousPage);

            NextPageCommand = new RelayCommand(
                _ => _pagenateViewModel.ShowPage(CurrentPage + 1),
                _ => _pagenateViewModel.HasNextPage);
        }

        public string PageInfo
        {
            get
            {
                return _pagenateViewModel.PageInfo("categories");
            }
        }
        public ICommand PreviousPageCommand { get; }
        public ICommand NextPageCommand { get; }

        public async Task LoadCategoriesAsync()
        {
            _loadCancellationTokenSource?.Cancel();
            _loadCancellationTokenSource?.Dispose();
            _loadCancellationTokenSource = new CancellationTokenSource();

            _allCategories = await _categoryService.GetCategoriesAsync(_loadCancellationTokenSource.Token);

            _pagenateViewModel.GetCollection(_allCategories);
            _pagenateViewModel.ShowPage(1);

            //ShowPage(1);
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
