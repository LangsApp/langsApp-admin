using LangApp.Admin.WPF.DTOs.Requests;
using LangApp.Admin.WPF.Infrastructure;
using LangApp.Admin.WPF.Models;
using LangApp.Admin.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LangApp.Admin.WPF.ViewModels.WindowsViewModels
{
    public class AddWordWindowViewModel : INotifyPropertyChanged
    {
        private readonly IWordsService _wordService;
        private ICommand? _closeAddWordWindowCommand;
        private ICommand? _saveWordsCommand;
        private ICommand? _addWordsToList;
        private CancellationTokenSource? _cancellationTokenSource;
        private ObservableCollection<string> newWordsToSave = new();
        private string _category = string.Empty;
        private string newWordText = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? CloseAddWordWindowRequested;

        public AddWordWindowViewModel(IWordsService wordService)
        {
            _wordService = wordService;
        }

        public ICommand CloseAddWordWindowCommand =>
            _closeAddWordWindowCommand ??= new RelayCommand(CloseAddWordWindow);
        public ICommand AddWordsToListCommand => _addWordsToList ??= new RelayCommand(AddNewWordsToList);
        public ICommand SaveWordsCommand => _saveWordsCommand ??= new AsyncRelayCommand(AddNewWordsAsync);
        

        public void AddNewWordsToList(object? param)
        {
            if (!string.IsNullOrWhiteSpace(NewWordText))
            {
                newWordsToSave.Add(NewWordText);
                NewWordText = string.Empty;
                NotifyPropertyChanged(nameof(NewWordsToSave));
            }
        }

        public async Task AddNewWordsAsync(object? param)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            if (!string.IsNullOrWhiteSpace(Category))
            {
                var request = new CreateBaseWordsByCategoryDTO
                {
                    CategoryName = Category,
                    Words = newWordsToSave.Select(word => new CreateBaseWordDTO { BaseWord = word }).ToList()
                };

                await _wordService.AddNewBaseWordsByCategoryAsync(request, _cancellationTokenSource.Token);
            }
            else if (newWordsToSave.Count == 1)
            {
                var request = new CreateBaseWordDTO
                {
                    BaseWord = newWordsToSave.FirstOrDefault() ?? string.Empty
                };

                await _wordService.AddNewBaseWordAsync(request, _cancellationTokenSource.Token);
            }
        }

        public string Category
        {
            get => _category;
            set
            {
                if (_category != value)
                {
                    _category = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string NewWordText
        {
            get => newWordText;
            set
            {
                if (newWordText != value)
                {
                    newWordText = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> NewWordsToSave
        {
            get => newWordsToSave;
            set
            {
                if (newWordsToSave != value)
                {
                    newWordsToSave = value;
                    NotifyPropertyChanged();
                }
            }
        }

        


        public void CloseAddWordWindow(object? _)
        {
            CloseAddWordWindowRequested?.Invoke(this, EventArgs.Empty);
        }

        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
