using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using Amazon.Auth.AccessControlPolicy;

namespace monitoramento_ambiental_mongodb.Models
{
    public class LeituraModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        [BsonElement("_id")]
        public string? Id { get; set; }

        private decimal valor;

        [BsonElement("valor")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Valor
        {
            get => valor;
            set => valor = Math.Round(value, 2);
        }

        [BsonElement("data_hora")]
        public DateTime DataHora { get; set; }

        [BsonElement("cod_sensor")]
        public int CodSensor { get; set; }

        public SensorModel? Sensor { get; set; }
    }

}