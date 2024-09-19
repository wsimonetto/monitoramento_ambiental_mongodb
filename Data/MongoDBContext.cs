//using Microsoft.Extensions.Options;
//using MongoDB.Driver;
//using monitoramento_ambiental_mongodb.Models;

//namespace monitoramento_ambiental_mongodb.Data
//{
//    public class MongoDBContext
//    {
//        private readonly IMongoDatabase _database;
//        private readonly DataBaseSettings _settings;

//        public MongoDBContext(IOptions<DataBaseSettings> options)
//        {
//            _settings = options.Value;
//            var client = new MongoClient(_settings.ConnectionURI);
//            _database = client.GetDatabase(_settings.DatabaseName);
//        }

//        public IMongoCollection<SensorModel> Sensores =>
//            _database.GetCollection<SensorModel>(_settings.SensorCollectionName);

//        public IMongoCollection<LeituraModel> Leituras =>
//            _database.GetCollection<LeituraModel>(_settings.LeituraCollectionName);

//        public IMongoCollection<PrevisaoChuvaModel> PrevisoesChuva =>
//            _database.GetCollection<PrevisaoChuvaModel>(_settings.PrevisaoChuvaCollectionName);

//        public IMongoCollection<ControleIrrigacaoModel> ControleIrrigacoes =>
//            _database.GetCollection<ControleIrrigacaoModel>(_settings.ControleIrrigacaoCollectionName);

//        public IMongoCollection<AlertaModel> Alertas =>
//            _database.GetCollection<AlertaModel>(_settings.AlertaCollectionName);
//    }
//}

using Microsoft.Extensions.Options;
using MongoDB.Driver;
using monitoramento_ambiental_mongodb.Models;

namespace monitoramento_ambiental_mongodb.Data
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;
        private readonly DataBaseSettings _settings;

        public MongoDBContext(IOptions<DataBaseSettings> options)
        {
            _settings = options.Value ?? throw new ArgumentNullException(nameof(options));
<<<<<<< HEAD
            if (string.IsNullOrEmpty(_settings.ConnectionURI))
                throw new ArgumentException("ConnectionURI cannot be null or empty", nameof(_settings.ConnectionURI));
            if (string.IsNullOrEmpty(_settings.DatabaseName))
                throw new ArgumentException("DatabaseName cannot be null or empty", nameof(_settings.DatabaseName));
=======

            if (string.IsNullOrEmpty(_settings.ConnectionURI))
            {
                throw new InvalidOperationException("ConnectionURI não está configurado.");
            }

            if (string.IsNullOrEmpty(_settings.DatabaseName))
            {
                throw new InvalidOperationException("DatabaseName não está configurado.");
            }
>>>>>>> 8bdfd3e (Update Variáveis no Azure)

            var client = new MongoClient(_settings.ConnectionURI);
            _database = client.GetDatabase(_settings.DatabaseName);
        }

        public IMongoCollection<SensorModel> Sensores =>
            _database.GetCollection<SensorModel>(_settings.SensorCollectionName);

        public IMongoCollection<LeituraModel> Leituras =>
            _database.GetCollection<LeituraModel>(_settings.LeituraCollectionName);

        public IMongoCollection<PrevisaoChuvaModel> PrevisoesChuva =>
            _database.GetCollection<PrevisaoChuvaModel>(_settings.PrevisaoChuvaCollectionName);

        public IMongoCollection<ControleIrrigacaoModel> ControleIrrigacoes =>
            _database.GetCollection<ControleIrrigacaoModel>(_settings.ControleIrrigacaoCollectionName);

        public IMongoCollection<AlertaModel> Alertas =>
            _database.GetCollection<AlertaModel>(_settings.AlertaCollectionName);
    }
}

<<<<<<< HEAD
=======



>>>>>>> 8bdfd3e (Update Variáveis no Azure)
