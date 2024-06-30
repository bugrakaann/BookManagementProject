using bookproject2.Controllers;
using bookproject2.Models;

namespace TestProject1;

public class DatabaseTest
{
    [Test]
    public async Task TestCreateBook()
    {
        var controller = new DatabaseController();
        var book = new BookModel
        {
            Name = "Test Book",
            Author = "Test Author",
            VolumeNumber = "1",
            isInStock = true
        };
        await controller.CreateBook(book);
        var allBooks = await controller.GetAllBooks(1,10);
        
        var retrievedBook = allBooks.FirstOrDefault(b => b.Name == book.Name && b.Author == book.Author);
        Assert.IsNotNull(retrievedBook, "Book was not created.");
        Assert.AreEqual(book.Name, retrievedBook.Name, "Book name does not match.");
        Assert.AreEqual(book.Author, retrievedBook.Author, "Book author does not match.");
    }
    [Test]
    public async Task TestGetAllBooks()
    {
        var controller = new DatabaseController();
        List<BookModel> books = await controller.GetAllBooks(1,10);
        
        Assert.Warn($"{books.Count} number of books retrieved from database.");
        
    }
}