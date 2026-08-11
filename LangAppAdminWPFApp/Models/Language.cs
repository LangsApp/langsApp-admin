using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.Models
{
    public class Language : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private string langCode = string.Empty;
        private string name = string.Empty;
        


        public Guid Id
        {
            get;
            private set;
        }

        public string LangCode
        {
            get => langCode;
            set
            {
                if (langCode != value)
                {
                    langCode = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
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
