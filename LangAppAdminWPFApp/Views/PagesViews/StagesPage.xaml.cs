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
    /// Interaction logic for StagesPage.xaml
    /// </summary>
    public partial class StagesPage : Page
    {
        public StagesPage(StagePageViewModel stagePageViewModel)
        {
            InitializeComponent();
            DataContext = stagePageViewModel;
        }

        private async void StagesPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is StagePageViewModel viewModel)
            {
                await viewModel.LoadStagesAsync();
            }
        }
    }
}
