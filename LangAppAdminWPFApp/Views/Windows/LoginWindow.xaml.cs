using LangApp.Admin.WPF.Services.Interfaces;
using LangApp.Admin.WPF.ViewModels.WindowsViewModels;
using LangAppAdminWPFApp;
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

namespace LangApp.Admin.WPF.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IWindowFactory _windowFactory;
        public LoginWindow(LoginWindowViewModel viewModel, IWindowFactory windowFactory)
        {
            InitializeComponent();
            DataContext = viewModel;
            _windowFactory = windowFactory;

            viewModel.LoginSucceeded += OnLoginSucceeded;
        }
        private void PasswordInput_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginWindowViewModel viewModel &&
                sender is PasswordBox passwordBox)
            {
                viewModel.LogInUser.Password =
                    passwordBox.Password;
            }
        }

        private void OnLoginSucceeded(object? sender, EventArgs e)
        {
            MainWindow mainWindow = 
                _windowFactory.Create<MainWindow>();

            Application.Current.MainWindow = mainWindow;

            mainWindow.Show();
            Close();
        }
    }
}
