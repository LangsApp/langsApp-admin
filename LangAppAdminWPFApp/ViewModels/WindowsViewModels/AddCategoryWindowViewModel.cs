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

namespace LangApp.Admin.WPF.ViewModels.WindowsViewModels
{
    public class AddCategoryWindowViewModel : INotifyPropertyChanged
    {
        private readonly ICategoryService _categoryService;  
        private ICommand? _closeAddCategoryWindowCommand;
        private ICommand? _addCategory;
        private CancellationTokenSource _cancellationTokenSource;
        private Category _newCategory = new();

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? CloseAddCategoryWindowRequested;

        public AddCategoryWindowViewModel(ICategoryService categoryService, CancellationTokenSource cancellationTokenSource)
        {
            _categoryService = categoryService;
            _cancellationTokenSource = cancellationTokenSource;
        }

        public ICommand CloseAddCategoryWindowCommand => 
            _closeAddCategoryWindowCommand ??= new RelayCommand(CloseAddCategoryWindow);
        public ICommand AddCategoryCommand => _addCategory ??= new AsyncRelayCommand(AddNewCategoryAsync);

        
        public async Task AddNewCategoryAsync(object? param)
        {
            if(_newCategory != null && !string.IsNullOrWhiteSpace(_newCategory.Name))
            {
                await _categoryService.AddNewCategoryAsync(_newCategory.Name, _cancellationTokenSource.Token);
            }
        }


        public Category NewCategory
        {
            get => _newCategory;
            set
            {
                if(_newCategory != value)
                {
                    _newCategory = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public void CloseAddCategoryWindow(object? _)
        {
            CloseAddCategoryWindowRequested?.Invoke(this, EventArgs.Empty);
        }
        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
