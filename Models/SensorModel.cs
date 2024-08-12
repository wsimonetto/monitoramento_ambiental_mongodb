using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace monitoramento_ambiental_mongodb.Models
{
    public class SensorModel
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string? Id { get; set; }

        [BsonElement("cod_sensor")]
        public int CodSensor { get; set; }

        [BsonElement("tipo_sensor")]
        public string? TipoSensor { get; set; } = null;

        [BsonElement("localizacao")]
        public string? Localizacao { get; set; } = null;
    }

}