using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.Models
{
    public class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public string login = string.Empty;
        public string password = string.Empty;

        public string Login
        {
            get => login;
            set
            {
                if (login != value)
                {
                    login = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Password 
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
