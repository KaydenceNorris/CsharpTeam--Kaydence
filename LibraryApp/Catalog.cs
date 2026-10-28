namespace LibraryApp;

/// <summary>
/// Manages the full collection of books in the library and the
/// operations students will write tests against: adding, finding,
/// checking out, and returning books.
/// </summary>
public class Catalog
{
    private readonly List<Book> _books = new();

    public IReadOnlyList<Book> Books => _books.AsReadOnly();

    public void AddBook(Book book)
    {
        if (book is null)
            throw new ArgumentNullException(nameof(book));

        if (_books.Any(b => b.Isbn == book.Isbn))
            throw new InvalidOperationException($"A book with ISBN '{book.Isbn}' already exists in the catalog.");

        _books.Add(book);
    }

    public Book? FindByIsbn(string isbn)
    {
        return _books.FirstOrDefault(b => b.Isbn == isbn);
    }

    public List<Book> SearchByTitle(string titleQuery)
    {
        if (string.IsNullOrWhiteSpace(titleQuery))
            return new List<Book>();

        return _books
            .Where(b => b.Title.Contains(titleQuery, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<Book> SearchByAuthor(string authorQuery)
    {
        if (string.IsNullOrWhiteSpace(authorQuery))
            return new List<Book>();

        return _books
            .Where(b => b.Author.Contains(authorQuery, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public void CheckOutBook(string isbn)
    {
        var book = FindByIsbn(isbn);
        if (book is null)
            throw new InvalidOperationException($"No book found with ISBN '{isbn}'.");

        //book.CheckOut();
    }

    public void ReturnBook(string isbn)
    {
        var book = FindByIsbn(isbn);
        if (book is null)
            throw new InvalidOperationException($"No book found with ISBN '{isbn}'.");

        book.Return();
    }

    public int TotalAvailableCopies()
    {
        return _books.Sum(b => b.AvailableCopies);
    }
}
