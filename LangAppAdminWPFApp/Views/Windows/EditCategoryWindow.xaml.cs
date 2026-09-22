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
    /// Interaction logic for EditCategoryWindow.xaml
    /// </summary>
    public partial class EditCategoryWindow : Window
    {
        public EditCategoryWindow(// чи не виникне проблеми при закритті?
            EditCategoryWindowViewModel editCategoryWindowViewModel, Category selectedCategory)
        {
            InitializeComponent();
            DataContext = editCategoryWindowViewModel;

            editCategoryWindowViewModel.CategoryToEdit = selectedCategory;

            editCategoryWindowViewModel.CloseEditCategoryWindowRequested += OnCloseEditCategoryWindowRequested;
        }

        private void OnCloseEditCategoryWindowRequested(object?  sender, EventArgs e)
        {
            Close();
        }
    }
}
