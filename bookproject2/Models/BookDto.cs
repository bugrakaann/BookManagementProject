namespace bookproject2.Models;

public class BookDto
{
    public BookDto(BookModel book)
    {
        _id = book._id?.ToString();
        Name = book.Name;
        Author = book.Author;
        VolumeNumber = book.VolumeNumber;
        isInStock = book.isInStock;
    }
    
    public string _id { get; set; }

    public string Name { get; set; }

    public string Author { get; set; }

    public string VolumeNumber { get; set; }

    public bool isInStock { get; set; }
}