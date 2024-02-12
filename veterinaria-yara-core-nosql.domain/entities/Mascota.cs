using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace veterinaria_yara_core_nosql.domain.entities
{
    public class Mascotas
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string Nombre { get; set; }
        public string Raza { get; set; }
        public int Edad { get; set; }
        public decimal Peso { get; set; }
        public DateTime Fecha { get; set; }
    }
}