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

namespace LangApp.Admin.WPF.Views.Windows.Edit
{
    /// <summary>
    /// Interaction logic for EditWordWindow.xaml
    /// </summary>
    public partial class EditWordWindow : Window
    {
        public EditWordWindow(
            EditWordWindowViewModel editWordViewModel, BaseWord selectedWord)
        {
            InitializeComponent();
            DataContext = editWordViewModel;


        }
    }
}
