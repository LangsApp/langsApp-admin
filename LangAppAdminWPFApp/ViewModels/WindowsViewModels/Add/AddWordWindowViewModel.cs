using LangApp.Admin.WPF.DTOs.Requests;
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
using System.Windows;
using System.Windows.Input;

namespace LangApp.Admin.WPF.ViewModels.WindowsViewModels.Add
{
    public class AddWordWindowViewModel : INotifyPropertyChanged
    {
        private readonly IWordsService _wordService;
        private readonly ICategoryService _categoryService;
        private ICommand? _closeAddWordWindowCommand;
        private ICommand? _saveWordsCommand;
        private ICommand? _addWordsToList;
        private ICommand? _filterCommand;
        private CancellationTokenSource? _cancellationTokenSource;
        private ObservableCollection<string> newWordsToSave = new();
        private List<Category> _existingCategories = [];
        private string _category = string.Empty;
        private string newWordText = string.Empty;
        private CancellationTokenSource? _loadCancellationTokenSource;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? CloseAddWordWindowRequested;

        public AddWordWindowViewModel(IWordsService wordService, ICategoryService categoryService)
        {
            _wordService = wordService;
            _categoryService = categoryService;
        }

        public ICommand CloseAddWordWindowCommand =>
            _closeAddWordWindowCommand ??= new RelayCommand(CloseAddWordWindow);
        public ICommand AddWordsToListCommand => _addWordsToList ??= new RelayCommand(AddNewWordsToList);
        public ICommand SaveWordsCommand => _saveWordsCommand ??= new AsyncRelayCommand(AddNewWordsAsync);
        public ICommand FilterCommand => _filterCommand ??= new RelayCommand(FilterCategories);
        

        public void FilterCategories(object? param)
        {
            MessageBox.Show("Test");
        }
        public void AddNewWordsToList(object? param)
        {
            if (!string.IsNullOrWhiteSpace(NewWordText))
            {
                newWordsToSave.Add(NewWordText);
                NewWordText = string.Empty;
                NotifyPropertyChanged(nameof(NewWordsToSave));
            }
        }

        public async Task AddNewWordsAsync(object? param)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            if (!string.IsNullOrWhiteSpace(Category))
            {
                var request = new CreateBaseWordsByCategoryDTO
                {
                    CategoryName = Category,
                    Words = newWordsToSave.ToList()
                };

                await _wordService.AddNewBaseWordsByCategoryAsync(request, _cancellationTokenSource.Token);
            }
            else
            {
                var request = new CreateBaseWordDTO
                {
                    BaseWord = newWordsToSave.ToList()
                };

                await _wordService.AddNewBaseWordAsync(request, _cancellationTokenSource.Token);
            }
        }

        public List<Category> ExistCategories
        {
            get => _existingCategories;

        }

        public string Category
        {
            get => _category;
            set
            {
                if (_category != value)
                {
                    _category = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string NewWordText
        {
            get => newWordText;
            set
            {
                if (newWordText != value)
                {
                    newWordText = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> NewWordsToSave
        {
            get => newWordsToSave;
            set
            {
                if (newWordsToSave != value)
                {
                    newWordsToSave = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public async Task LoadCategoriesAsync()
        {
            _loadCancellationTokenSource?.Cancel();
            _loadCancellationTokenSource?.Dispose();
            _loadCancellationTokenSource = new CancellationTokenSource();

            _existingCategories = await _categoryService.GetPublishedCategoriesAsync(_loadCancellationTokenSource.Token);
            NotifyPropertyChanged(nameof(ExistCategories));
        }


        public void CloseAddWordWindow(object? _)
        {
            CloseAddWordWindowRequested?.Invoke(this, EventArgs.Empty);
        }

        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
