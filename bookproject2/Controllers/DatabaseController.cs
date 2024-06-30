using bookproject2.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace bookproject2.Controllers;

[Route("Database")]
public class DatabaseController : Controller
{
    private const string ConnectionString = "mongodb+srv://admin:password@cluster0.dz76t1i.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
    private const string DatabaseName = "books";
    private const string BookCollection = "bookcollection";

    private IMongoCollection<T> ConnectToMongo<T>(in string collection)
    {
        var client = new MongoClient(ConnectionString);
        var db = client.GetDatabase(DatabaseName);
        return db.GetCollection<T>(collection);
    }

    public async Task<List<BookModel>> GetAllBooks(int pageNumber, int pageSize)
    {
        var booksCollection = ConnectToMongo<BookModel>(BookCollection);
        //var results = await usersCollection.FindAsync(_ => true);
        var results = await booksCollection.Find(_ => true)
            .Skip((pageNumber - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();
        return results;
    }
    
    [HttpPost("CreateBook")]
    public async Task CreateBook(BookModel bookModel)
    {
        var booksCollection = ConnectToMongo<BookModel>(BookCollection);
        await booksCollection.InsertOneAsync(bookModel);
    }
    public async Task UpdateBook(BookModel bookModel)
    {
        var booksCollection = ConnectToMongo<BookModel>(BookCollection);
        var filter = Builders<BookModel>.Filter.Eq("_id", bookModel._id);
        await booksCollection.ReplaceOneAsync(filter, bookModel, new ReplaceOptions { IsUpsert = true });
    }
    [HttpPost("UpdateBookFromForm")]
    public async Task<IActionResult> UpdateBookFromForm([FromForm]BookModel bookModel)
    {
        var booksCollection = ConnectToMongo<BookModel>(BookCollection);
        var book = await booksCollection.Find(b => b._id == bookModel._id).FirstOrDefaultAsync();
        if (book == null)
        {
            return NotFound("Book not found");
        }

        Console.WriteLine("Updating book with ID fromform: " + bookModel._id);
        await UpdateBook(bookModel);
        return Ok();
    }

    [HttpGet("GetBook/{id}")]
    public async Task<IActionResult> GetBook(string id)
    {
        var booksCollection = ConnectToMongo<BookModel>(BookCollection);
        var book = await booksCollection.Find(b => b._id == id).FirstOrDefaultAsync();
        if (book == null)
        {
            return NotFound();
        }
        var bookDto = new BookDto(book);
        Console.WriteLine("Fetched book" + book._id);
        return Ok(bookDto);
    }
    
    public async Task DeleteBook(BookModel bookModel)
    {
        var booksCollection = ConnectToMongo<BookModel>(BookCollection);
        await booksCollection.DeleteOneAsync(c => c._id == bookModel._id);
    }
    [HttpDelete("DeleteBook/{id}")]
    public async Task<IActionResult> DeleteBook(string ID)
    {
        Console.WriteLine("Deleting book with ID: " + ID);
        var booksCollection = ConnectToMongo<BookModel>(BookCollection);
        await booksCollection.DeleteOneAsync(c => c._id == ID);
        return Ok();
    }
}
