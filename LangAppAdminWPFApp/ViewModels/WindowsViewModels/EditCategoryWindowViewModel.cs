using LangApp.Admin.WPF.DTOs.Requests;
using LangApp.Admin.WPF.Infrastructure;
using LangApp.Admin.WPF.Models;
using LangApp.Admin.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LangApp.Admin.WPF.ViewModels.WindowsViewModels
{
    public class EditCategoryWindowViewModel : INotifyPropertyChanged
    {
        private readonly ICategoryService _categoryService;
        private ICommand? _closeEditCategoryWindowCommand;
        private ICommand? _saveEditedCategoryCommand;
        private CancellationTokenSource? _cancellationTokenSource;
        private EditCategoryDTO _editCategoryDTO = new();

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? CloseEditCategoryWindowRequested;

        public EditCategoryWindowViewModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ICommand CloseEditCategoryWindowCommand =>
            _closeEditCategoryWindowCommand ??= new RelayCommand(CloseEditCategoryWindow);
        public ICommand SaveEditedCategoryCommand =>
            _saveEditedCategoryCommand ??= new AsyncRelayCommand(SaveEditedCategoryAsync);


        public async Task SaveEditedCategoryAsync(object? _)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            if(string.IsNullOrWhiteSpace(_editCategoryDTO.EditedCategory))
            {
                await _categoryService.EditCategoryAsync(_editCategoryDTO, _cancellationTokenSource.Token);
            }
        }

        public EditCategoryDTO EditCategoryDTO
        {
            get => _editCategoryDTO;
            set
            {
                if(_editCategoryDTO != value)
                {
                    _editCategoryDTO = value;
                    NotifyPropertyChanged();
                }
            }
        }
        //public Category? CategoryToEdit { get; set; }
        //public string NewName
        //{
        //    get => _newName;

        //    set
        //    {
        //        if(_newName != value)
        //        {
        //            _newName = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}
        
        public void CloseEditCategoryWindow(object? _)
        {
            CloseEditCategoryWindowRequested?.Invoke(this, EventArgs.Empty);
        }
        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
