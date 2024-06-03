using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace backend.Models
{
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] // Esto indica que el tipo de dato del ID es ObjectId
        public string? Id { get; set; }

        [BsonElement("Nombre")]
        public required string nombre { get; set; }

        [BsonElement("Contrasena")]
        public required string contrasena { get; set; }

    }

}