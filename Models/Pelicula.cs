using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Models
{
    public class Pelicula
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("titulo")]
        public string? titulo { get; set; }
        [BsonElement("videoUrl")]
        public string? VideoUrl { get; set; }

        [BsonElement("genero")]
        [BsonRepresentation(BsonType.ObjectId)]

        public  List<string?>? GeneroIds { get; set; }
        

    }

}