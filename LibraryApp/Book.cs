namespace LibraryApp;

/// <summary>
/// Represents a single book title in the library catalog.
/// One Book instance represents all copies of that title.
/// </summary>
public class Book
{
    public string Title { get; }
    public string Author { get; }
    public string Isbn { get; }
    public int TotalCopies { get; }
    public int AvailableCopies { get; private set; }
    // Added 3 dates to the book class
    public DateTime CheckoutDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime returnedDate { get; set; }
    // true or fasle is the book over due
    public bool isOverdue { get; set; }

    public Book(string title, string author, string isbn, int totalCopies)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));
        if (string.IsNullOrWhiteSpace(author))
            throw new ArgumentException("Author cannot be empty.", nameof(author));
        if (string.IsNullOrWhiteSpace(isbn))
            throw new ArgumentException("ISBN cannot be empty.", nameof(isbn));
        if (totalCopies < 0)
            throw new ArgumentException("Total copies cannot be negative.", nameof(totalCopies));

        Title = title;
        Author = author;
        Isbn = isbn;
        TotalCopies = totalCopies;
        AvailableCopies = totalCopies;
    }

    public bool IsAvailable => AvailableCopies > 0;

    public void CheckOut()
    {
        if (!IsAvailable)
            throw new InvalidOperationException($"No available copies of '{Title}' to check out.");

        AvailableCopies--;

        // sets the checkout date to today
        CheckoutDate = System.DateTime.Today;
        // sets the duedate to 2 weeks in the future
        DueDate = System.DateTime.Today.AddDays(14);
    }

    public void Return()
    {
        if (AvailableCopies >= TotalCopies)
            throw new InvalidOperationException($"All copies of '{Title}' are already returned.");

        AvailableCopies++;

        CalculateOverDue();

        if (isOverdue)
        {
            Console.WriteLine("This book is overdue");
        }
    }
    // I don't have the time to do some writing to a file to store the real due dates
    // So we are going to play with some math.random to simulate a book being late
    public void CalculateOverDue()
    {
        Random rnd = new Random();
        int daysAdded = rnd.Next( 0, 18);

        returnedDate = CheckoutDate.AddDays(daysAdded);

        if (returnedDate > DueDate)
        {
            isOverdue = true;
        }
           isOverdue = false;

    }
}
