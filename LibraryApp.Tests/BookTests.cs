using LibraryApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryApp.Tests;

/// <summary>
/// EXAMPLE TEST CLASS — this is here as a working pattern to copy.
/// In Week 7 (Unit Testing) you'll add more tests like the one below.
/// To run these tests: right-click the test project in Visual Studio
/// and choose "Run Tests," or use Test Explorer (Test menu > Test Explorer).
/// </summary>
[TestClass]
public class BookTests
{
    [TestMethod]
    public void CheckOut_ReducesAvailableCopiesByOne()
    {
        // Arrange
        var book = new Book("Sample Title", "Sample Author", "0000000000000", 3);

        // Act
        //book.CheckOut();

        // Assert
        Assert.AreEqual(2, book.AvailableCopies);
    }
}
