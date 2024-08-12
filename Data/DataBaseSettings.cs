namespace monitoramento_ambiental_mongodb.Data
{
    public class DataBaseSettings
    {
        public string ConnectionURI { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string SensorCollectionName { get; set; } = null!;
        public string LeituraCollectionName { get; set; } = null!;
        public string ControleIrrigacaoCollectionName { get; set; } = null!;
        public string AlertaCollectionName { get; set; } = null!;
        public string PrevisaoChuvaCollectionName { get; set; } = null!;

    }
}
