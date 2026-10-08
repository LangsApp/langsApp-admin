using LangApp.Admin.WPF.Models;
using LangApp.Admin.WPF.ViewModels.WindowsViewModels.Edit;
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
    /// Interaction logic for EditTranslateWindow.xaml
    /// </summary>
    public partial class EditTranslateWindow : Window
    {
        public EditTranslateWindow(
            EditTranslateWindowViewModel editTranslateWindowViewModel, Translate selectedTranslate)
        {
            InitializeComponent();
            DataContext = editTranslateWindowViewModel;

            editTranslateWindowViewModel.TranslateToEdit = selectedTranslate;
            editTranslateWindowViewModel.CloseEditTranslateRequested += OnCloseEditTranslateWindowRequested;
        }

        private void OnCloseEditTranslateWindowRequested(object? sender, EventArgs e)
        {
            Close();
        }
    }
}
