using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.Models
{
    public class Language : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private string langCode = string.Empty;
        private string name = string.Empty;
        private bool _reviewed = false;
        private bool _published = false;


        public Guid Id
        {
            get;
            set;
        }

        public string Code
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

        public bool Reviewed
        {
            get => _reviewed;
            set
            {
                if (_reviewed != value)
                {
                    _reviewed = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool Published
        {
            get => _published;
            set
            {
                if (_published != value)
                {
                    _published = value;
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
