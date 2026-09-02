using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.Models
{
    public class Category : INotifyPropertyChanged
    {
        private string name = string.Empty;
        public event PropertyChangedEventHandler? PropertyChanged;

        public Guid Id { get; private set; }

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
