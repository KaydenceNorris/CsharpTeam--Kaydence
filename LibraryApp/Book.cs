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
    public DateTime CheckoutDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime returnedDate { get; set; }
    // true or falsee is the book over due
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

        // sets the checkout date to today
        CheckoutDate = System.DateTime.Today;
        // sets the duedate to 2 weeks in the future
        DueDate = System.DateTime.Today.AddDays(14);

        Title = title;
        Author = author;
        Isbn = isbn;
        TotalCopies = totalCopies;
        AvailableCopies = totalCopies;
    }

    public bool IsAvailable => AvailableCopies > 0;

    public void CheckOut(Member member)
    {
        // Adds book to memebrs list of checked out books
        member.CheckedOutBooks.Add(this);
        if (!IsAvailable)
            throw new InvalidOperationException($"No available copies of '{Title}' to check out.");

        AvailableCopies--;
        WriteCSV(member);

    }
    public void CalculateOverDue()
    {
        string[] line = File.ReadAllLines("CheckoutData.csv");
        string[] info = line[0].Split(',');
        string dueDateString = info[3].Trim();
        DateTime dueDate = DateTime.Parse(dueDateString);
        returnedDate = System.DateTime.Now;

        if ( returnedDate > dueDate)
        {
            isOverdue = true;
        }
    }

    public void Return()
    {
        if (AvailableCopies >= TotalCopies)
            throw new InvalidOperationException($"All copies of '{Title}' are already returned.");

        AvailableCopies++;
        CalculateOverDue();
    }
    public void WriteCSV(Member member)
    {
        // Writes the memeber info connecting to the book they cheked out to a csv file
        string data = $"{member.MemberId},{member.Name},{this.CheckoutDate}, {this.DueDate}";
        File.AppendAllText("CheckoutData.csv", data );
    }
}
