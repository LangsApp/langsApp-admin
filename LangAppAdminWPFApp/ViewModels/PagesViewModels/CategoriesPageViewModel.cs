using LangApp.Admin.WPF.Infrastructure;
using LangApp.Admin.WPF.Models;
using LangApp.Admin.WPF.Services;
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
        private readonly IDialogService _dialogService;
        private CancellationTokenSource? _loadCancellationTokenSource;
        private const int PageSize = 5;
        private List<Category> _allCategories = [];
        private PaginateViewModel<Category> _pagenateViewModel = new(0, 1, PageSize);
        private ICommand? _openAddCategoryCommand;
        private ICommand? _openEditCategoryCommand;
        private ICommand? _deleteCategoryCommand;

        public int CurrentPage => _pagenateViewModel.PaginateNumber;
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<Category> Categories => _pagenateViewModel.PageCollection;


        public CategoriesPageViewModel(
            ICategoryService categoryService, 
            IDialogService dialogService)
        {
            _pagenateViewModel.PageChanged += OnPageChanged;

            _categoryService = categoryService;
            _dialogService = dialogService;

            PreviousPageCommand = new RelayCommand(
            _ => _pagenateViewModel.ShowPage(CurrentPage - 1),
            _ => _pagenateViewModel.HasPreviousPage);

            NextPageCommand = new RelayCommand(
                _ => _pagenateViewModel.ShowPage(CurrentPage + 1),
                _ => _pagenateViewModel.HasNextPage);
        }

        public ICommand OpenAddCategoryCommand => _openAddCategoryCommand ??= new RelayCommand(OpenAddCategoryWindow);
        public ICommand OpenEditCategoryCommand => _openEditCategoryCommand 
            ??= new RelayCommand(OpenEditCategoryWindow, CanOpenEditCategoryWindow);
        public ICommand DeleteCategoryCommand => _deleteCategoryCommand 
            ??= new RelayCommand(DeleteCategory, CanDeleteCategory);

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
            foreach(var category in _allCategories)
            {
                if(category.Published)
                {
                    category.CanChangePublished = false;
                }
                if(category.Reviewed)
                {
                    category.CanChangeReviewed = false;
                }
            }
            _pagenateViewModel.GetCollection(_allCategories);
            _pagenateViewModel.ShowPage(1);

            //ShowPage(1);
        }

        
        
        private bool CanOpenEditCategoryWindow(object? param)
        {
            return param is Category category && !category.Published;
        }
        private bool CanDeleteCategory(object? param)
        {
            return param is Category category && !category.Published;
        }

        private void OpenAddCategoryWindow(object? _)
        {
            _dialogService.OpenAddCategoryDialog();
        }

        private void OpenEditCategoryWindow(object? param)
        {
            if (param is not Category category)
            {
                return;
            }
            _dialogService.OpenEditCategoryDialog(category);
        }

        private void DeleteCategory(object? param)
        {
            if (param is not Category category)
            {
                return;
            }
            var result = _dialogService.ConfirmAction("Are you sure to delete ", category.Name, "Confirm deletion");

            if(result)
            {
                _categoryService.DeleteCategoryAsync(category.Id);
                _allCategories.Remove(category);
                _pagenateViewModel.GetCollection(_allCategories);
                _pagenateViewModel.ShowPage(CurrentPage);
            }
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
