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
using LangApp.Admin.WPF.ViewModels.Pages;

namespace LangApp.Admin.WPF.Views.PagesViews
{
    /// <summary>
    /// Interaction logic for LanguagesPage.xaml
    /// </summary>
    public partial class LanguagesPage : Page
    {
        public LanguagesPage(LanguagePageViewModel languagesPageViewModel)
        {
            InitializeComponent();
            DataContext = languagesPageViewModel;
        }

        private async void LanguagePage_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is LanguagePageViewModel viewModel)
            {
                await viewModel.LoadLanguageAsync();
            }
        }
    }
}
