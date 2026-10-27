using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.Models
{
    public class Stage : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private string name = string.Empty;
        private int order = 0;
        private bool _reviewed = false;
        private bool _published = false;
        public Guid Id 
        { 
            get; 
            set; 
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

        [JsonIgnore]
        public bool CanChangeReviewed { get; set; } = true;
        [JsonIgnore]
        public bool CanChangePublished { get; set; } = true;
        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
