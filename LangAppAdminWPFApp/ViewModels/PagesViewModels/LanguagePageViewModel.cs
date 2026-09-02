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
    private PaginateViewModel<Language> _pagenateViewModel = new(0, 1, PageSize);

    public int CurrentPage => _pagenateViewModel.PaginateNumber;
    public event PropertyChangedEventHandler? PropertyChanged;
    public ObservableCollection<Language> Languages => _pagenateViewModel.PageCollection;
    public LanguagePageViewModel(ILanguageService languageService)
    {
        _pagenateViewModel.PageChanged += OnPageChanged;

        _languageService = languageService;

        PreviousPageCommand = new RelayCommand(
            _ => _pagenateViewModel.ShowPage(CurrentPage - 1),
            _ => _pagenateViewModel.HasPreviousPage);

        NextPageCommand = new RelayCommand(
            _ => _pagenateViewModel.ShowPage(CurrentPage + 1),
            _ => _pagenateViewModel.HasNextPage);
    }


    public string PageInfo
    {
        get
        {
            return _pagenateViewModel.PageInfo("languages");
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

        _pagenateViewModel.GetCollection(_allLanguages);
        _pagenateViewModel.ShowPage(1);
        
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