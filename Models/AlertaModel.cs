using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace monitoramento_ambiental_mongodb.Models
{
    public class AlertaModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string? Id { get; set; }

        [BsonElement("tipo_alerta")]
        public string? TipoAlerta { get; set; }

        [BsonElement("descricao")]
        public string? Descricao { get; set; }

        [BsonElement("localizacao")]
        public string? Localizacao { get; set; }

        [BsonElement("data_hora")]
        public DateTime DataHora { get; set; }

    }

}