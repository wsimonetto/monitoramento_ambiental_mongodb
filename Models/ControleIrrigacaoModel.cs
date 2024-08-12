using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace monitoramento_ambiental_mongodb.Models
{
    public class ControleIrrigacaoModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string? Id { get; set; }

        [BsonElement("localizacao")]
        public string? Localizacao { get; set; }

        [BsonElement("estado")]
        public string? Estado { get; set; }

        [BsonElement("data_hora")]
        public DateTime DataHora { get; set; }

        [BsonElement("previsao_chuva_id")]
        public string PrevisaoChuvaId { get; set; }

        public PrevisaoChuvaModel? PrevisaoChuva { get; set; }
    }

}