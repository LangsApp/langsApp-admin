using LangApp.Admin.WPF.Models;
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
    /// Interaction logic for EditLanguageWindow.xaml
    /// </summary>
    public partial class EditLanguageWindow : Window
    {
        public EditLanguageWindow(
            EditLanguageWindowViewModel editLanguageViewModel, Language selectedLanguage)
        {
            InitializeComponent();
            DataContext = editLanguageViewModel;

            editLanguageViewModel.LanguageToEdit = selectedLanguage;
            editLanguageViewModel.CloseEditLanguageWindowRequested += OnCloseEditLanguageWindowRequested;
        }

        private void OnCloseEditLanguageWindowRequested(object? sender, EventArgs e)
        {
            Close();
        }
    }
}
