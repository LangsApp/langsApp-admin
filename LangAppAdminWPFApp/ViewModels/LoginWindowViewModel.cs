using LangApp.Admin.WPF.Infrastructure;
using LangApp.Admin.WPF.Models;
using LangApp.Admin.WPF.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LangApp.Admin.WPF.ViewModels
{
    class LoginWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private ICommand _loginCommand;

        private readonly ILoginService _loginService;

        private User _logInUser;

        private CancellationTokenSource? _loginCancellationTokenSource;

        public LoginWindowViewModel(ILoginService loginService, ICommand loginCommand, User loginUser)
        {
            _loginService = loginService; 
            _loginCommand = loginCommand;
            _logInUser = loginUser;
        }

        public ICommand LoginCommand => _loginCommand ??= new AsyncRelayCommand(Log_In_User);

        public async Task Log_In_User(object? param)
        {
            CancelLogin();

            _loginCancellationTokenSource = new CancellationTokenSource();
            if (_logInUser.Login != null && _logInUser.Password != null)
            {
                try
                {
                   var result = await _loginService.Login(_logInUser.Login, _logInUser.Password, _loginCancellationTokenSource.Token);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Login failed: {ex.Message}");
                }

            }
            else
            {
                MessageBox.Show("Fill all fields");
            }
        }


        

        public User LogInUser
        {
            get => _logInUser;
            set
            {
                if (_logInUser != value)
                {
                    _logInUser = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public void CancelLogin()
        {
            _loginCancellationTokenSource?.Cancel();
        }

        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
