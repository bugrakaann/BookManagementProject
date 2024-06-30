using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace bookproject2.Models;

public class BookModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string _id { get; set; }
    
    public string Name { get; set; }
    
    public string Author { get; set; }
    
    public string VolumeNumber { get; set; }
    
    public bool isInStock { get; set; }
}