using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.Models
{
    public class BaseWord : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private string _normalizedWord = string.Empty;
        private string _displayWord = string.Empty;

        public Guid Id
        {
            get;
            private set;
        }

        public string NormalizedWord
        {
            get => _normalizedWord;
            set
            {
                if (_normalizedWord != value)
                {
                    _normalizedWord = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string DisplayWord
        {
            get => _displayWord;
            set
            {
                if (_displayWord != value)
                {
                    _displayWord = value;
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
