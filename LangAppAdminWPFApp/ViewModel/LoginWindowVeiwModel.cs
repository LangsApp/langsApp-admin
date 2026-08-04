using LangApp.Admin.WPF.Infrastructure;
using LangApp.Admin.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.ViewModel
{
    internal class LoginWindowVeiwModel : MyRealisationNotifyPropertyChanged
    {
        

        public Login? login;

        public Login? Login
        {
            get { return login; }
            set
            {
                login = value;
                NotifyPropertyChanged();
            }
        }
    }
}
