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
    public class TranslationsPageViewModel : INotifyPropertyChanged
    {
        private readonly ITranslatesService _translatesService;
        private readonly IDialogService _dialogService;
        private CancellationTokenSource? _loadCancellationTokenSource;
        private const int PageSize = 5;
        private List<Translate> _allTranslates = [];
        private List<Translate> _copyAllTranslates = [];
        private PaginateViewModel<Translate> _paginateViewModel = new(0, 1, PageSize);
        private ICommand? _createTranslatesCommand;
        private ICommand? _openEditTranslateCommand;
        private ICommand? _deleteTranslateCommand;
        private ICommand? _savePublicationChangesCommand;

        public int CurrentPage => _paginateViewModel.PaginateNumber;
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<Translate> Translates => _paginateViewModel.PageCollection;

        public TranslationsPageViewModel(ITranslatesService translatesService, IDialogService dialogService)
        {
            _paginateViewModel.PageChanged += OnPageChanged;

            _translatesService = translatesService;
            _dialogService = dialogService;

            PreviousPageCommand = new RelayCommand(
            _ => _paginateViewModel.ShowPage(CurrentPage - 1),
            _ => _paginateViewModel.HasPreviousPage);

            NextPageCommand = new RelayCommand(
                _ => _paginateViewModel.ShowPage(CurrentPage + 1),
                _ => _paginateViewModel.HasNextPage);
        }

        public ICommand CreateTranslatesCommand => _createTranslatesCommand ??= 
            new AsyncRelayCommand(CreateTranslatesAsync);
        public ICommand OpenEditTranslateCommand => _openEditTranslateCommand ??=
            new RelayCommand(OpenEditTranslateWindow, CanOpenEditTranslateWindow);
        public ICommand DeleteTranslateCommand => _deleteTranslateCommand ??=
            new AsyncRelayCommand(DeleteTranslate, CanDeleteTranslate);
        public ICommand SavePublicationChangesCommand => _savePublicationChangesCommand
            ??= new AsyncRelayCommand(SavePublicationChanges);

        public string PageInfo
        {
            get
            {
                return _paginateViewModel.PageInfo("translates");
            }
        }
        public ICommand PreviousPageCommand { get; }
        public ICommand NextPageCommand { get; }

        public async Task LoadTranslatesAsync()
        {
            _loadCancellationTokenSource?.Cancel();
            _loadCancellationTokenSource?.Dispose();
            _loadCancellationTokenSource = new CancellationTokenSource();

            _allTranslates = await _translatesService.GetTranslatesAsync(_loadCancellationTokenSource.Token);
            _copyAllTranslates = new List<Translate>(_allTranslates.Select(t => new Translate
            {
                Id = t.Id,
                BaseWord = t.BaseWord,
                TranslateTo = t.TranslateTo,
                NormalizedTranslatedWord = t.NormalizedTranslatedWord,
                DisplayTranslatedWord = t.DisplayTranslatedWord,
                Reviewed = t.Reviewed,
                Published = t.Published,
                CanChangePublished = t.CanChangePublished,
                CanChangeReviewed = t.CanChangeReviewed
            }));
            foreach (var translate in _allTranslates)
            {
                if (translate.Published)
                {
                    translate.CanChangePublished = false;
                }
                if (translate.Reviewed)
                {
                    translate.CanChangeReviewed = false;
                }
            }

            _paginateViewModel.GetCollection(_allTranslates);
            _paginateViewModel.ShowPage(1);

            //ShowPage(1);
        }
        
        private async Task SavePublicationChanges(object? param)
        {
            var updatedTranslates = new List<ReviewAndPublishDTO>();

            foreach (var translate in _allTranslates)
            {
                if(translate.Published && !translate.Reviewed)
                {
                    _dialogService.ShowError($"Translate: {translate.Id} cannot be published without being reviewed.",
                        "Error");
                    continue;
                }

                if (translate.Published != _copyAllTranslates.FirstOrDefault(ct => ct.Id == translate.Id)?.Published
                    || translate.Reviewed != _copyAllTranslates.FirstOrDefault(ct => ct.Id == translate.Id)?.Reviewed)
                {
                    updatedTranslates.Add(
                        new ReviewAndPublishDTO
                        {
                            Id = translate.Id,
                            Published = translate.Published,
                            Reviewed = translate.Reviewed
                        });
                }
            }

            _loadCancellationTokenSource = new CancellationTokenSource();

            await _translatesService.PublishTranslatesAsync(updatedTranslates, _loadCancellationTokenSource.Token);
        }
        public async Task CreateTranslatesAsync(object? param)
        {
            _loadCancellationTokenSource?.Cancel();
            _loadCancellationTokenSource?.Dispose();
            _loadCancellationTokenSource = new CancellationTokenSource();
            await _translatesService.CreateTranslatesAsync(_loadCancellationTokenSource.Token);
        }

        private void OnPageChanged(object? sender, EventArgs e)
        {
            NotifyPropertyChanged(nameof(CurrentPage));
            NotifyPropertyChanged(nameof(PageInfo));

            CommandManager.InvalidateRequerySuggested();
        }

        private void OpenEditTranslateWindow(object? param)
        {
            if(param is not Translate translate)
            {
                return;
            }
            _dialogService.OpenEditTranslateDialog(translate);
        }

        private async Task DeleteTranslate(object? param)
        {
            if (param is not Translate translate)
            {
                return;
            }

            var result = _dialogService.ConfirmAction("Are you sure to delete ",
                translate.DisplayTranslatedWord, "Confirm deletion");

            if(result)
            {
                await _translatesService.DeleteTranslateAsync(translate.Id);
                _allTranslates.Remove(translate);
                _paginateViewModel.GetCollection(_allTranslates);
                _paginateViewModel.ShowPage(CurrentPage);
            }
        }

        private bool CanOpenEditTranslateWindow(object? param)
        {
            return param is Translate translate && !translate.Published;
        }
        private bool CanDeleteTranslate(object? param)
        {
            return param is Translate translate && !translate.Published;
        }
        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
