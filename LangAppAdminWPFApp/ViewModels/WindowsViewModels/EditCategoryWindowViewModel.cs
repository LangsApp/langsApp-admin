using LangApp.Admin.WPF.Infrastructure;
using LangApp.Admin.WPF.Models;
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
        private ICommand? _closeEditCategoryWindowCommand;
        private Category? _categoryToEdit;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? CloseEditCategoryWindowRequested;


        public ICommand CloseEditCategoryWindowCommand =>
            _closeEditCategoryWindowCommand ??= new RelayCommand(CloseEditCategoryWindow);





        public Category? CategoryToEdit { get; set; }

        public void Initialize(Category category)
        {
            _categoryToEdit = category;
        }
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
