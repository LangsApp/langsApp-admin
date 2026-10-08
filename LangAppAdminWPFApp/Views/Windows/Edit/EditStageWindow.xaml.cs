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
    /// Interaction logic for EditStageWindow.xaml
    /// </summary>
    public partial class EditStageWindow : Window
    {
        public EditStageWindow(
            EditStageWindowViewModel editStageWindowViewModel, Stage selectedStage)
        {
            InitializeComponent();
            DataContext = editStageWindowViewModel;

            editStageWindowViewModel.StageToEdit = selectedStage;
            editStageWindowViewModel.CloseEditStageWindowRequested += OnCloseEditStageWindowRequested;
        }

        private void OnCloseEditStageWindowRequested(object? sender, EventArgs e)
        {
            Close();
        }
    }
}
