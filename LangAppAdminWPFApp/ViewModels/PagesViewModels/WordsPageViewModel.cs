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

namespace LangApp.Admin.WPF.ViewModels.PagesViewModels
{
    public class WordsPageViewModel : INotifyPropertyChanged
    {
        private readonly IWordsService _wordsService;
        private readonly IDialogService _dialogService;
        private CancellationTokenSource? _loadCancellationTokenSource;
        private const int PageSize = 5;
        private List<BaseWord> _allBaseWords = [];
        private List<BaseWord> _copyAllBaseWords = [];
        private PaginateViewModel<BaseWord> _paginateViewModel = new(0, 1, PageSize);
        private ICommand? _openAddWordCommand;
        private ICommand? _openEditWordCommand;
        private ICommand? _deleteBaseWordCommand;
        private ICommand? _savePublicationChangesCommand;

        public int CurrentPage => _paginateViewModel.PaginateNumber;
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<BaseWord> BaseWords => _paginateViewModel.PageCollection;

        public WordsPageViewModel(IWordsService wordsService, IDialogService dialogService)
        {
            _paginateViewModel.PageChanged += OnPageChanged;
            _wordsService = wordsService;
            _dialogService = dialogService;

            PreviousPageCommand = new RelayCommand(
                _ => _paginateViewModel.ShowPage(CurrentPage - 1),
                _ => _paginateViewModel.HasPreviousPage);

            NextPageCommand = new RelayCommand(
                _ => _paginateViewModel.ShowPage(CurrentPage + 1),
                _ => _paginateViewModel.HasNextPage);
        }

        public ICommand OpenAddWordCommand => _openAddWordCommand ??= new RelayCommand(OpenAddWordWindow);
        public ICommand OpenEditWordCommand => _openEditWordCommand
            ??= new RelayCommand(OpenEditWordWindow, CanOpenEditWordCommand);
        public ICommand DeleteBaseWordCommand => _deleteBaseWordCommand
            ??= new AsyncRelayCommand(DeleteBaseWord, CanDeleteBaseWord);
        public ICommand SavePublicationChangesCommand => _savePublicationChangesCommand
            ??= new AsyncRelayCommand(SavePublicationChanges);
        public ICommand PreviousPageCommand { get; }
        public ICommand NextPageCommand { get; }

        public string PageInfo
        {
            get
            {
                return _paginateViewModel.PageInfo("base words");
            }
        }

        public async Task LoadWordsAsync()
        {
            _loadCancellationTokenSource?.Cancel();
            _loadCancellationTokenSource?.Dispose();
            _loadCancellationTokenSource = new CancellationTokenSource();

            _allBaseWords = await _wordsService.GetBaseWordsAsync(_loadCancellationTokenSource.Token);
            _copyAllBaseWords = new List<BaseWord>(_allBaseWords.Select(bw => new BaseWord
            {
                Id = bw.Id,
                NormalizedWord = bw.NormalizedWord,
                DisplayWord = bw.DisplayWord,
                Reviewed = bw.Reviewed,
                Published = bw.Published,
                CanChangePublished = bw.CanChangePublished,
                CanChangeReviewed = bw.CanChangeReviewed

            }));
            foreach (var baseWord in _allBaseWords)
            {
                if (baseWord.Published)
                {
                    baseWord.CanChangePublished = false;
                }
                if (baseWord.Reviewed)
                {
                    baseWord.CanChangeReviewed = false;
                }
            }

            _paginateViewModel.GetCollection(_allBaseWords);
            _paginateViewModel.ShowPage(1);

            //ShowPage(1);
        }

        private async Task SavePublicationChanges(object? param)
        {
            var updatedBaseWords = new List<ReviewAndPublishDTO>();
            foreach (var baseWord in _allBaseWords)
            {
                if(baseWord.Published && !baseWord.Reviewed)
                {
                    _dialogService.ShowError($"Base word: {baseWord.DisplayWord} cannot be published without being reviewed.",
                        "Error");
                    continue;
                }

                if (baseWord.Published != _copyAllBaseWords.FirstOrDefault(cbw => cbw.Id == baseWord.Id)?.Published
                    || baseWord.Reviewed != _copyAllBaseWords.FirstOrDefault(cbw => cbw.Id == baseWord.Id)?.Reviewed)
                {
                    updatedBaseWords.Add(
                        new ReviewAndPublishDTO
                        {
                            Id = baseWord.Id,
                            Published = baseWord.Published,
                            Reviewed = baseWord.Reviewed,
                        });
                }
            }

            _loadCancellationTokenSource = new CancellationTokenSource();
            
            await _wordsService.PublishBaseWordsAsync(updatedBaseWords, _loadCancellationTokenSource.Token);
        }

        public void OpenAddWordWindow(object? _)
        {
            _dialogService.OpenAddWordDialog();
        }
        private void OnPageChanged(object? sender, EventArgs e)
        {
            NotifyPropertyChanged(nameof(CurrentPage));
            NotifyPropertyChanged(nameof(PageInfo));

            CommandManager.InvalidateRequerySuggested();
        }

        private void OpenEditWordWindow(object? param)
        {
            if(param is not BaseWord baseWord)
            {
                return;
            }
            _dialogService.OpenEditWordDialog(baseWord);
        }
        private async Task DeleteBaseWord(object? param)
        {
            if (param is not BaseWord baseWord)
            {
                return;
            }
            var result = _dialogService.ConfirmAction("Are you sure to delete ",
                baseWord.DisplayWord, "Confirm deletion");

            if(result)
            {
                await _wordsService.DeleteBaseWordAsync(baseWord.Id);
                _allBaseWords.Remove(baseWord);
                _paginateViewModel.GetCollection(_allBaseWords);
                _paginateViewModel.ShowPage(CurrentPage);
            }
        }
        private bool CanOpenEditWordCommand(object? param)
        {
            return param is BaseWord baseWord && !baseWord.Published;
        }
        private bool CanDeleteBaseWord(object? param)
        {
            return param is BaseWord baseWord && !baseWord.Published;
        }
        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
