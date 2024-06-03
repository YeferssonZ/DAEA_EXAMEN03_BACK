using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Models
{
    public class Rating
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] // Esto indica que el tipo de dato del ID es ObjectId

        public string? Id { get; set; }

        [BsonElement("usuarioid")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UsuarioId { get; set; }
        [BsonElement("peliculaid")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? PeliculaId { get; set; } // Referencia a documento de Película
        [BsonElement("calificacion")]
        public double Calificacion { get; set; }
    }

}