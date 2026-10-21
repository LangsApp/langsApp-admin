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
        private bool _reviewed = false;
        private bool _published = false;

        public Guid Id
        {
            get;
            set;
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

        public bool Reviewed
        {
            get => _reviewed;
            set
            {
                if(_reviewed != value)
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
