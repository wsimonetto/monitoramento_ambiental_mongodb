using MongoDB.Bson;
using MongoDB.Driver;
using monitoramento_ambiental_mongodb.Data;
using monitoramento_ambiental_mongodb.Models;

namespace monitoramento_ambiental_mongodb.Services
{
    public class SensorService
    {
        private readonly IMongoCollection<SensorModel> _sensorCollection;

        public SensorService(MongoDBContext context)
        {
            _sensorCollection = context.Sensores;
        }

        public async Task<List<SensorModel>> GetAsync() =>
            await _sensorCollection.Find(x => true).ToListAsync();

        public async Task<Response> GetByIdAsync(string id)
        {
            try
            {
                var sensor = await _sensorCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

                if (sensor == null)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Sensor não encontrado. Verifique o ID fornecido.",
                        CreatedItem = null
                    };
                }

                return new Response
                {
                    Success = true,
                    Message = "Sensor encontrado com sucesso.",
                    CreatedItem = sensor
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    Success = false,
                    Message = $"Erro ao buscar o sensor: {e.Message}",
                    CreatedItem = null
                };
            }
        }

        public async Task<Response> CreateAsync(SensorModel sensorModel)
        {
            try
            {
                var existingSensor = await _sensorCollection
                    .Find(x => x.CodSensor == sensorModel.CodSensor)
                    .FirstOrDefaultAsync();

                if (existingSensor != null)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Já existe um sensor com esse Código de Sensor."
                    };
                }
                else
                {
                    await _sensorCollection.InsertOneAsync(sensorModel);

                    return new Response
                    {
                        Success = true,
                        Message = "Sensor cadastrado com sucesso",
                        CreatedItem = sensorModel
                    };
                }
            }
            catch (Exception e)
            {
                return new Response
                {
                    Success = false,
                    Message = $"Erro ao cadastrar o Sensor: {e.Message}"
                };
            }

        }

        public async Task<Response> UpdateAsync(string id, SensorModel sensorModel)
        {
            try
            {
                var existingSensor = await _sensorCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

                if (existingSensor == null)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Sensor não encontrado."
                    };
                }
                else
                {
                    await _sensorCollection.ReplaceOneAsync(x => x.Id == id, sensorModel);
                    return new Response
                    {
                        Success = true,
                        Message = "Sensor atualizado com sucesso",
                        CreatedItem = sensorModel,
                    };
                }
            }
            catch (Exception e)
            {
                return new Response
                {
                    Success = false,
                    Message = $"Erro ao atualizar o Sensor: {e.Message}"
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
                        Message = "ID do Sensor inválido."
                    };
                }

                var result = await _sensorCollection.DeleteOneAsync(x => x.Id == id);

                if (result.DeletedCount == 0)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Sensor não encontrado para exclusão."
                    };
                }

                return new Response
                {
                    Success = true,
                    Message = "Sensor removido com sucesso."
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    Success = false,
                    Message = $"Erro ao remover o Sensor: {e.Message}"
                };
            }
        }
        
        public class Response
        {
            public bool Success { get; set; }
            public string? Message { get; set; }
            public SensorModel? CreatedItem { get; set; }
        }

    }

}
