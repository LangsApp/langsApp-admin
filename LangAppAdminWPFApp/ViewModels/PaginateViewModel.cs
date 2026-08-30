using System.Collections.ObjectModel;

public class PaginateViewModel<T>
{
    private List<T> _items = [];
    private int _pageSize;

    public ObservableCollection<T> PageCollection { get; } = [];
    public event EventHandler? PageChanged;
    public int PaginateNumber { get; set; } // number of current page

    public int TotalPaginate { get; set; } // total pages count


                              // кількість    // номер            // кількість  
                              // об'єктів    // поточної сторінки // об'єктів на сторінці
    public PaginateViewModel(int objectCount, int paginateNumber, int paginateSize)
    {
        PaginateNumber = paginateNumber;
        TotalPaginate = (int)Math.Ceiling((double)objectCount / paginateSize);
        _pageSize = paginateSize;
    }

    public bool HasPreviousPage
    {
        get { return PaginateNumber > 1; }
    }
    public bool HasNextPage
    {
        get { return PaginateNumber < TotalPaginate; }
    }


    public string PageInfo(string message)
    {
        if (_items.Count == 0)
        {
            return $"No {message}";
        }

        int firstItem = (PaginateNumber - 1) * _pageSize + 1;
        int lastItem = Math.Min(
            PaginateNumber * _pageSize,
            _items.Count);

        return $"{firstItem}-{lastItem} of {_items.Count}";
    }

    public void ShowPage(int pageNumber)
    {
        SetPagination(_items.Count, pageNumber, _pageSize);

        var pageItems = _items
            .Skip((PaginateNumber - 1) * _pageSize)
            .Take(_pageSize);

        PageCollection.Clear();

        foreach (var item in pageItems)
        {
            PageCollection.Add(item);
        }

        PageChanged?.Invoke(this, EventArgs.Empty);
    }

    public void GetCollection(List<T> allItems)
    {
        _items = allItems;
    }

    private void SetPagination(int objectCount, int paginateNumber, int paginateSize)
    {
        PaginateNumber = paginateNumber;
        TotalPaginate = (int)Math.Ceiling((double)objectCount / paginateSize);
        _pageSize = paginateSize;
    }
}