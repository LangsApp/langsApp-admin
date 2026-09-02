using LangApp.Admin.WPF.ViewModels.PagesViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LangApp.Admin.WPF.Views.PagesViews
{
    /// <summary>
    /// Interaction logic for Categories.xaml
    /// </summary>
    public partial class Categories : Page
    {
        public Categories(CategoriesPageViewModel categoriesPageViewModel)
        {
            InitializeComponent();
            DataContext = categoriesPageViewModel;
        }

        private async void CategoriesPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is CategoriesPageViewModel viewModel)
            {
                await viewModel.LoadCategoriesAsync();
            }
        }
    }
}
