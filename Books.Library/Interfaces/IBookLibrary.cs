using Books.Library.Models;

namespace Books.Library.Interfaces;

public interface IBookLibrary
{
    int Count { get; }
    IReadOnlyList<Book> Books { get; }

    void Add(Book book);

    IReadOnlyList<Book> GetSorted(StringComparer? stringComparer = default);
    IReadOnlyList<Book> SearchByTitle(string titlePart, StringComparer? stringComparer = default, StringComparison searchComparison = StringComparison.OrdinalIgnoreCase);

    void LoadFromFile(string path);
    void SaveToFile(string path);
}
