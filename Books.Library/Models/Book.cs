using System.Xml.Serialization;

namespace Books.Library.Models;

public record Book
{
    public string Title { get; }
    public string Author { get; }
    public int NumberOfPages { get; }

    public Book(string title, string author, int pages)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title must be provided.", nameof(title));
        }

        if (string.IsNullOrWhiteSpace(author))
        {
            throw new ArgumentException("Author must be provided.", nameof(author));
        }

        if (pages <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pages), "Pages must be greater than 0.");
        }

        Title = title.Trim();
        Author = author.Trim();
        NumberOfPages = pages;
    }
}

[XmlRoot("Books")]
public class BooksContainer
{
    [XmlElement("Book")]
    public List<Book> Items { get; set; } = [];
}
