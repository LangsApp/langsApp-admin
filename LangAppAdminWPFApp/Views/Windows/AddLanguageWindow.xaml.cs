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
    /// Interaction logic for AddLanguageWindow.xaml
    /// </summary>
    public partial class AddLanguageWindow : Window
    {
        public AddLanguageWindow(AddLanguageWindowViewModel addLanguageWindowViewModel)
        {
            InitializeComponent();
            DataContext = addLanguageWindowViewModel;
            addLanguageWindowViewModel.CloseAddLanguageWindowRequested += OnCloseAddLanguageWindowRequested;
        }

        private void OnCloseAddLanguageWindowRequested(object? sender, EventArgs e)
        {
            Close();
        }
    }
}
