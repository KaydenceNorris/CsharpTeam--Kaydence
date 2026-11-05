namespace LibraryApp
{
    /// <summary>
    /// Manages the full collection of books in the library and the
    /// operations students will write tests against: adding, finding,
    /// checking out, and returning books.
    /// </summary>
    public class Catalog
    {
        private readonly List<Book> _books = [];
        private readonly List<Member> _members = [];

        public IReadOnlyList<Book> Books => _books.AsReadOnly();
        public IReadOnlyList<Member> Members => _members.AsReadOnly();

        public void AddBook(Book book)
        {
            if (book is null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            if (_books.Any(b => b.Isbn == book.Isbn))
            {
                throw new InvalidOperationException($"A book with ISBN '{book.Isbn}' already exists in the catalog.");
            }

            _books.Add(book);
        }

        public Book? FindByIsbn(string isbn)
        {
            return _books.FirstOrDefault(b => b.Isbn == isbn);
        }

        public List<Book> SearchByTitle(string titleQuery)
        {
            return string.IsNullOrWhiteSpace(titleQuery)
                ? new List<Book>()
                : [.. _books.Where(b => b.Title.Contains(titleQuery, StringComparison.OrdinalIgnoreCase))];
        }

        public List<Book> SearchByAuthor(string authorQuery)
        {
            return string.IsNullOrWhiteSpace(authorQuery)
                ? []
                : [.. _books.Where(b => b.Author.Contains(authorQuery, StringComparison.OrdinalIgnoreCase))];
        }

        // Member functions - primarily mirrors the book functions, but for members
        public void AddMember(Member member)
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            if (_members.Any(m => m._memberId == member._memberId))
            {
                throw new InvalidOperationException($"A member with ID '{member._memberId}' already exists in the catalog.");
            }

            _members.Add(member);
        }
        public Member? FindMemberById(int memberId)
        {
            return _members.FirstOrDefault(m => m._memberId == memberId);
        }
        public List<Member> SearchMembersByName(string nameQuery)
        {
            return string.IsNullOrWhiteSpace(nameQuery)
                ? []
                : [.. _members.Where(m => m._name.Contains(nameQuery, StringComparison.OrdinalIgnoreCase))];
        }

        public void CheckOutBook(string isbn, int memberId)
        {
            Book book = FindByIsbn(isbn)
                ?? throw new InvalidOperationException($"No book found with ISBN '{isbn}'.");
            Member member = FindMemberById(memberId) ?? throw new InvalidOperationException($"No member found with the ID of '{memberId}'.");

            book.CheckOut(member);
        }

        public void ReturnBook(string isbn, int memberId)
        {
            Book book = FindByIsbn(isbn) ?? throw new InvalidOperationException($"No book found with ISBN '{isbn}'.");
            Member member = FindMemberById(memberId) ?? throw new InvalidOperationException($"No member found with the ID of '{memberId}'.");
            if (!member._checkedOutBooks.Remove(book))
            {
                throw new InvalidOperationException($"Member '{memberId}' has not checked out the book with ISBN '{isbn}'.");
            }

            book.Return();
        }

        public int TotalAvailableCopies()
        {
            return _books.Sum(b => b.AvailableCopies);
        }
    }
}
