using MongoDB.Bson;
using MongoDB.Driver;
using monitoramento_ambiental_mongodb.Data;
using monitoramento_ambiental_mongodb.Models;

namespace monitoramento_ambiental_mongodb.Services
{
    public class AlertaService
    {
        private readonly IMongoCollection<AlertaModel> _alertaCollection;
        private readonly TimeZoneInfo _brazilTimeZone;

        public AlertaService(MongoDBContext context)
        {
            _alertaCollection = context.Alertas;
            _brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
        }

        public async Task<List<AlertaModel>> GetAsync()
        {
            var alertas = await _alertaCollection.Find(x => true).ToListAsync();
            // Converter UTC para local time for each alertas
            foreach (var alerta in alertas)
            {
                alerta.DataHora = TimeZoneInfo.ConvertTimeFromUtc(alerta.DataHora, _brazilTimeZone);
            }
            return alertas;
        }
            

        public async Task<Response> GetByIdAsync(string id)
        {

            try
            {
                var alerta = await _alertaCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

                if (alerta == null)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Alerta não encontrado. Verifique o ID fornecido.",
                        CreatedItem = null
                    };
                }
                alerta.DataHora = TimeZoneInfo.ConvertTimeFromUtc(alerta.DataHora, _brazilTimeZone);

                return new Response
                {
                    Success = true,
                    Message = "Alerta encontrado com sucesso.",
                    CreatedItem = alerta
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    Success = false,
                    Message = $"Erro ao buscar o Alerta: {e.Message}",
                    CreatedItem = null
                };
            }
        }

        public async Task<Response> CreateAsync(AlertaModel alertaModel)
        {
            try
            {
                var existingAlerta = await _alertaCollection
                    .Find(x => x.Id == alertaModel.Id)
                    .FirstOrDefaultAsync();

                if (existingAlerta != null)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Já existe um Alerta com esse Código de Sensor."
                    };
                }
                else
                {
                    var localDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _brazilTimeZone);
                    alertaModel.DataHora = localDateTime;

                    await _alertaCollection.InsertOneAsync(alertaModel);

                    return new Response
                    {
                        Success = true,
                        Message = "Alerta cadastrado com sucesso",
                        CreatedItem = alertaModel
                    };
                }
            }
            catch (Exception e)
            {
                return new Response
                {
                    Success = false,
                    Message = $"Erro ao cadastrar o Alerta: {e.Message}"
                };
            }
        }

        public async Task<Response> UpdateAsync(string id, AlertaModel alertaModel)
        {
            try
            {
                var existingAlerta = await _alertaCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

                if (existingAlerta == null)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Alerta não encontrado."
                    };
                }
                else
                {
                    var localDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _brazilTimeZone);
                    alertaModel.DataHora = localDateTime;

                    await _alertaCollection.ReplaceOneAsync(x => x.Id == id, alertaModel);

                    return new Response
                    {
                        Success = true,
                        Message = "Alerta atualizado com sucesso",
                        CreatedItem = alertaModel,
                    };
                }
            }
            catch (Exception e)
            {
                return new Response
                {
                    Success = false,
                    Message = $"Erro ao atualizar o Alerta: {e.Message}"
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
                        Message = "ID do Alerta inválido."
                    };
                }

                var result = await _alertaCollection.DeleteOneAsync(x => x.Id == id);

                if (result.DeletedCount == 0)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Alerta não encontrado para exclusão."
                    };
                }

                return new Response
                {
                    Success = true,
                    Message = "Alerta removido com sucesso."
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    Success = false,
                    Message = $"Erro ao remover o Alerta: {e.Message}"
                };
            }

        }
           

        public class Response
        {
            public bool Success { get; set; }
            public string? Message { get; set; }
            public AlertaModel? CreatedItem { get; set; }
        }

    }

}
