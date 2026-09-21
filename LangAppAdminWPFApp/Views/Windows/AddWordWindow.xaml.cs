using LangApp.Admin.WPF.ViewModels.WindowsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LangApp.Admin.WPF.Views.Windows
{
    /// <summary>
    /// Interaction logic for AddWordWindow.xaml
    /// </summary>
    public partial class AddWordWindow : Window
    {
        public AddWordWindow(AddWordWindowViewModel addWordWindowViewModel)
        {
            InitializeComponent();
            DataContext = addWordWindowViewModel;
            addWordWindowViewModel.CloseAddWordWindowRequested += OnCloseAddWordWindowRequested;
        }

        private void OnCloseAddWordWindowRequested(object? sender, EventArgs e)
        {
            Close();
        }
    }
}
