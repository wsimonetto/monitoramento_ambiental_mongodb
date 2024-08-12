using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace monitoramento_ambiental_mongodb.Models
{
    public class PrevisaoChuvaModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("data_hora")]
        public DateTime DataHora { get; set; }

        [BsonElement("previsao")]
        public string? PrevisaoChuva { get; set; }
    }

}