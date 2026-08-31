using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.Models
{
    public class Stage : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private string name = string.Empty;
        private int order = 0;

        public Guid Id 
        { 
            get; 
            private set; 
        }

        public string StageName
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

        public int Order
        {
            get => order;
            set
            {
                if (order != value)
                {
                    order = value;
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
