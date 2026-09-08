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
    public class AddCategoryWindowViewModel : INotifyPropertyChanged
    {
        
        private ICommand? _closeAddCategoryWindowCommand;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? CloseAddCategoryWindowRequested;


        public ICommand CloseAddCategoryWindowCommand => 
            _closeAddCategoryWindowCommand ??= new RelayCommand(CloseAddCategoryWindow);

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
