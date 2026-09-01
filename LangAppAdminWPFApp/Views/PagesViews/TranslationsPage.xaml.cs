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
    /// Interaction logic for TranslationsPage.xaml
    /// </summary>
    public partial class TranslationsPage : Page
    {
        public TranslationsPage(TranslationsPageViewModel translationsPageViewModel)
        {
            InitializeComponent();
            DataContext = translationsPageViewModel;
        }

        private async void TranslationsPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is TranslationsPageViewModel viewModel)
            {
                await viewModel.LoadTranslatesAsync();
            }
        }
    }
}
