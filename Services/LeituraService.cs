using MongoDB.Bson;
using MongoDB.Driver;
using monitoramento_ambiental_mongodb.Data;
using monitoramento_ambiental_mongodb.Models;

namespace monitoramento_ambiental_mongodb.Services
{
    public class LeituraService
    {
        private readonly IMongoCollection<LeituraModel> _leituraCollection;
        private readonly IMongoCollection<SensorModel> _sensorCollection;
        private readonly TimeZoneInfo _brazilTimeZone;


        public LeituraService(MongoDBContext context)
        {
            _leituraCollection = context.Leituras;
            _sensorCollection = context.Sensores;
            _brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
        }

        public async Task<List<LeituraModel>> GetAsync()
        {
            var leituras = await _leituraCollection.Find(x => true).ToListAsync();

            foreach (var leitura in leituras)
            {
                leitura.DataHora = TimeZoneInfo.ConvertTimeFromUtc(leitura.DataHora, _brazilTimeZone);
            }
            return leituras;
        }

        public async Task<Response> GetByIdAsync(string id)
        {
            try
            {
                var leitura = await _leituraCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

                if (leitura == null)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Leitura não encontrada. Verifique o ID fornecido.",
                        CreatedItem = null
                    };
                }

                leitura.DataHora = TimeZoneInfo.ConvertTimeFromUtc(leitura.DataHora, _brazilTimeZone);

                return new Response
                {
                    Success = true,
                    Message = "Leitura encontrada com sucesso.",
                    CreatedItem = leitura
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    Success = false,
                    Message = $"Erro ao buscar a leitura: {e.Message}",
                    CreatedItem = null
                };
            }
        }

        public async Task<Response> CreateAsync(LeituraModel leituraModel)
        {
            try
            {
                var sensor = await _sensorCollection.Find(x => x.CodSensor == leituraModel.CodSensor).FirstOrDefaultAsync();

                if (sensor == null)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Não consta Sensor com esse Id"
                    };

                    throw new ArgumentException("SensorId não encontrado.");
                }
            else
            { 
                leituraModel.Sensor = sensor;

                var localDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _brazilTimeZone);

                leituraModel.DataHora = localDateTime;
                await _leituraCollection.InsertOneAsync(leituraModel);

                    return new Response
                    {
                        Success = true,
                        Message = "Leitura criada com sucesso",
                        CreatedItem = leituraModel
                    };
                }
            }
            catch (Exception ex)
            {

                return new Response
                {
                    Success = false,
                    Message = $"Erro ao criar a Leitura: {ex.Message}"
                };
            }

        }      

        public async Task<Response> UpdateAsync(string id, LeituraModel leituraModel)
        {
            try
            {
                var existingsensor = await _sensorCollection.Find(x => x.CodSensor == leituraModel.CodSensor).FirstOrDefaultAsync();

                if (existingsensor == null)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Leitura não encontrada."
                    };
                }

                leituraModel.Sensor = existingsensor;
                var localDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _brazilTimeZone);
                leituraModel.DataHora = localDateTime;

                var existingLeitura = await _leituraCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

                if (existingLeitura == null)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Não consta Leitura não encontrada."
                    };
                }
                else
                {
                    await _leituraCollection.ReplaceOneAsync(x => x.Id == id, leituraModel);

                    return new Response
                    {
                        Success = true,
                        Message = "Leitura atualizada com sucesso.",
                        CreatedItem = leituraModel
                    };
                }
            }
            catch (Exception e)
            {
                return new Response
                {
                    Success = false,
                    Message = $"Erro ao atualizar a Leitura: {e.Message}"
                };
            }
        }

        public async Task<Response> RemoveAsync(string id)
        {
            try
            {
                if (!ObjectId.TryParse(id, out _))
                {
                    return new Response
                    {
                        Success = false,
                        Message = "ID da leitura inválido."
                    };
                }

                var result = await _leituraCollection.DeleteOneAsync(x => x.Id == id);

                if (result.DeletedCount == 0)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Leitura não encontrada para exclusão."
                    };
                }

                return new Response
                {
                    Success = true,
                    Message = "Leitura removida com sucesso."
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    Success = false,
                    Message = $"Erro ao remover a leitura: {e.Message}"
                };
            }
        }

        public class Response
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public LeituraModel? CreatedItem { get; set; }
        }
    }

}