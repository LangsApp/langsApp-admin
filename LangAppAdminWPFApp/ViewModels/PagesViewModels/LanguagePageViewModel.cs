using LangApp.Admin.WPF.Infrastructure;
using LangApp.Admin.WPF.Models;
using LangApp.Admin.WPF.Services;
using LangApp.Admin.WPF.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

public class LanguagePageViewModel : INotifyPropertyChanged
{
    private readonly ILanguageService _languageService;
    private CancellationTokenSource? _loadCancellationTokenSource;
    private const int PageSize = 5;
    private List<Language> _allLanguages = [];
    private PaginateViewModel<Language> _paginateViewModel = new(0, 1, PageSize);

    public int CurrentPage => _paginateViewModel.PaginateNumber;
    public event PropertyChangedEventHandler? PropertyChanged;
    public ObservableCollection<Language> Languages => _paginateViewModel.PageCollection;
    public LanguagePageViewModel(ILanguageService languageService)
    {
        _paginateViewModel.PageChanged += OnPageChanged;

        _languageService = languageService;

        PreviousPageCommand = new RelayCommand(
            _ => _paginateViewModel.ShowPage(CurrentPage - 1),
            _ => _paginateViewModel.HasPreviousPage);

        NextPageCommand = new RelayCommand(
            _ => _paginateViewModel.ShowPage(CurrentPage + 1),
            _ => _paginateViewModel.HasNextPage);
    }


    public string PageInfo
    {
        get
        {
            return _paginateViewModel.PageInfo("languages");
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

        _paginateViewModel.GetCollection(_allLanguages);
        _paginateViewModel.ShowPage(1);
        
        //ShowPage(1);
    }

    private void OnPageChanged(object? sender, EventArgs e)
    {
        NotifyPropertyChanged(nameof(CurrentPage));
        NotifyPropertyChanged(nameof(PageInfo));

        CommandManager.InvalidateRequerySuggested();
    }

    //private void ShowPage(int pageNumber)
    //{
    //    _paginateViewModel = new PaginateViewModel<Language>(
    //        _allLanguages.Count,
    //        pageNumber,
    //        PageSize);

    //    var pageItems = _allLanguages
    //        .Skip((CurrentPage - 1) * PageSize)
    //        .Take(PageSize);

    //    Languages.Clear();

    //    foreach (var language in pageItems)
    //    {
    //        Languages.Add(language);
    //    }

    //    NotifyPropertyChanged(nameof(CurrentPage));
    //    NotifyPropertyChanged(nameof(PageInfo));

    //    CommandManager.InvalidateRequerySuggested();
    //}


    private void NotifyPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}