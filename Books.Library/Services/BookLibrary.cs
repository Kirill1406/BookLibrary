using Books.Library.Interfaces;
using Books.Library.Models;

namespace Books.Library.Services;

public class BookLibrary(IBookSerializer serializer) : IBookLibrary
{
    #region Fields

    private readonly List<Book> _books = [];
    private readonly Lock _sync = new();

    #endregion Fields

    #region Properties

    public int Count { get { lock (_sync) return _books.Count; } }

    public IReadOnlyList<Book> Books { get { lock (_sync) { return _books; } } }

    #endregion Properties

    #region Methods 

    public void Add(Book book)
    {
        ArgumentNullException.ThrowIfNull(book);
        lock (_sync)
        {
            _books.Add(book);
        }
    }

    public IReadOnlyList<Book> GetSorted(StringComparer? stringComparer = default)
    {
        return [.. Books
            .OrderBy(b => b.Author, stringComparer)
            .ThenBy(b => b.Title, stringComparer)];
    }

    public IReadOnlyList<Book> SearchByTitle(string titlePart, StringComparer? stringComparer = default, StringComparison searchComparison = StringComparison.OrdinalIgnoreCase)
    {
        if (string.IsNullOrWhiteSpace(titlePart))
        {
            return [];
        }

        var q = titlePart.Trim();

        return [.. Books
            .Where(b => b.Title.Contains(q, searchComparison))
            .OrderBy(b => b.Author, stringComparer)
            .ThenBy(b => b.Title, stringComparer)];
    }

    #region Sync Xml

    public void LoadFromFile(string path)
    {
        var loaded = serializer.ReadFromFile<BooksContainer>(path);
        ArgumentNullException.ThrowIfNull(loaded, nameof(loaded));

        lock (_sync)
        {
            _books.Clear();
            _books.AddRange(loaded.Items);
        }
    }

    public void SaveToFile(string path)
    {
        serializer.WriteToFile(Books, path);
    }

    #endregion Sync Xml

    #endregion Methods
}
