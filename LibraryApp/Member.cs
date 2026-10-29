using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LibraryApp
{
    public class Member
    {
        // Private variables
        public string _name = "";
        public int _memberId = 0;
        public List<Book> _checkedOutBooks = new List<Book>();

        // Public variables
        //public required string Name { get { return _name; } set { _name = value; } }
        //public required int MemberId { get { return _memberId; } set { _memberId = value; } }
        //public required List<Book> CheckedOutBooks { get { return _checkedOutBooks; } set { _checkedOutBooks = value; } }

        // Constructor
        [SetsRequiredMembers]
        public Member(string name, int memberId, List<Book> checkedOutBooks)
        {
            try
            // Address Concern 1: Could you put this in a try catch block so that the program doesn't stop? Or have the constructor be automated so that a name and ID can't be blank.
            // I can put the handling in the try catch block.
            {
                // Invalid input handling
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentException("Name cannot be empty.", nameof(name));
                if (string.IsNullOrWhiteSpace(memberId.ToString()))
                    throw new ArgumentException("MemberId cannot be empty", nameof(memberId));

                // Assigning constructor values to public variables

                _name = name ?? throw new ArgumentNullException(nameof(name));
                _memberId = memberId;
                _checkedOutBooks = checkedOutBooks ?? throw new ArgumentNullException(nameof(name));
            }

            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Null Value Error:" + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error:" + ex.Message);
            }
        }

        // Methods
        public void CheckOut(Book book)
        {
            // Invalid checkout handling
            if (!book.IsAvailable)
                throw new InvalidOperationException($"No available copies of '{book.Title}' to check out.");
            if (_checkedOutBooks.Count > 5)
                throw new InvalidOperationException($"Member '{_name}' has checked out the max amount of books");

            // Add book to List
            _checkedOutBooks.Add(book);
            // Address Concern 2: This is very similar to the checkout method in Book. Could you utilize that method instead of making a new one?
            // While they have similar code, the outcome of each is fundamentally different. I could run CheckOut for the book involved instead.
            // Naming them the same and having them appear functionally similar adheres to the OOP principle of polymorphism
            //book.CheckOut();
        }
    }
}
