using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.Models
{
    public class Translate : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private string baseWord = string.Empty;
        private string translateTo = string.Empty;
        private string normalizedTranslatedText = string.Empty;
        private string displayTranslatedText = string.Empty;


        public Guid Id
        {
            get;
            private set;
        }

        public string BaseWord
        {
            get => baseWord;
            set
            {
                if (baseWord != value)
                {
                    baseWord = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string TranslateTo
        {
            get => translateTo;
            set
            {
                if (translateTo != value)
                {
                    translateTo = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string NormalizedTranslatedText
        {
            get => normalizedTranslatedText;
            set
            {
                if(normalizedTranslatedText != value)
                {
                    normalizedTranslatedText = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string DisplayTranslatedText
        {
            get => displayTranslatedText;
            set
            {
                if(displayTranslatedText != value)
                {
                    displayTranslatedText = value;
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
