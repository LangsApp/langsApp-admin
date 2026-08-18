using LangApp.Admin.WPF.Infrastructure;
using LangApp.Admin.WPF.Models;
using LangApp.Admin.WPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LangApp.Admin.WPF.ViewModels.Pages
{
    public class LanguagePageViewModel : INotifyPropertyChanged
    {
        private readonly ILanguageService _languageService;
        private CancellationTokenSource? _loadCancellationTokenSource;
        private const int PageSize = 5;
        private List<Language> _allLanguages = [];
        private PaginateViewModel _paginateViewModel = new(0, 1, PageSize);

        public int CurrentPage => _paginateViewModel.PaginateNumber;
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<Language> Languages { get; } = [];
        public LanguagePageViewModel(ILanguageService languageService) 
        {
            _languageService = languageService;

            PreviousPageCommand = new RelayCommand(
                _ => ShowPage(CurrentPage - 1),
                _ => _paginateViewModel.HasPreviousPage);

            NextPageCommand = new RelayCommand(
                _ => ShowPage(CurrentPage + 1),
                _ => _paginateViewModel.HasNextPage);
        }

        
        public string PageInfo
        {
            get
            {
                if(_allLanguages.Count == 0)
                {
                    return "No languages";
                }

                int firstItem = (CurrentPage - 1) * PageSize + 1;
                int lastItem = Math.Min(
                    CurrentPage * PageSize,
                    _allLanguages.Count);

                return $"{firstItem}-{lastItem} of {_allLanguages.Count}";
            }
        }
        public ICommand PreviousPageCommand { get; }
        public ICommand NextPageCommand { get; }

        public async Task LoadLanguageAsync()
        {
            _loadCancellationTokenSource?.Cancel();
            _loadCancellationTokenSource?.Dispose();
            _loadCancellationTokenSource = new CancellationTokenSource();

            _allLanguages = await _languageService.GetLanguagesAsync(_loadCancellationTokenSource.Token);

            ShowPage(1);
        }

        private void ShowPage(int pageNumber)
        {
            _paginateViewModel = new PaginateViewModel(
                _allLanguages.Count,
                pageNumber,
                PageSize);

            var pageItems = _allLanguages
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize);

            Languages.Clear();

            foreach (var language in pageItems)
            {
                Languages.Add(language);
            }

            NotifyPropertyChanged(nameof(CurrentPage));
            NotifyPropertyChanged(nameof(PageInfo));

            CommandManager.InvalidateRequerySuggested();
        }


        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
