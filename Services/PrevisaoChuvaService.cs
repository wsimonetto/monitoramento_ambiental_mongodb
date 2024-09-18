using MongoDB.Bson;
using MongoDB.Driver;
using monitoramento_ambiental_mongodb.Data;
using monitoramento_ambiental_mongodb.Models;

namespace monitoramento_ambiental_mongodb.Services
{
    public class PrevisaoChuvaService
    {
        private readonly IMongoCollection<PrevisaoChuvaModel> _previsaoChuvaCollection;
        private readonly TimeZoneInfo _brazilTimeZone;

        public PrevisaoChuvaService(MongoDBContext context)
        {
            _previsaoChuvaCollection = context.PrevisoesChuva;
            _brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
        }

        public async Task<List<PrevisaoChuvaModel>> GetAsync()
        {
            var previsoesChuva = await _previsaoChuvaCollection.Find(x => true).ToListAsync();

            foreach (var previsaoChuva in previsoesChuva)
            {
                previsaoChuva.DataHora = TimeZoneInfo.ConvertTimeFromUtc(previsaoChuva.DataHora, _brazilTimeZone);
            }
            return previsoesChuva;
        }

        public async Task<Response> GetByIdAsync(string id)
        {
            try
            {
                var previsaoChuva = await _previsaoChuvaCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

                if (previsaoChuva == null)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Previsão de Chuva não encontrada. Verifique o ID fornecido.",
                        CreatedItem = null
                    };
                }
                previsaoChuva.DataHora = TimeZoneInfo.ConvertTimeFromUtc(previsaoChuva.DataHora, _brazilTimeZone);

                return new Response
                {
                    Success = true,
                    Message = "Previsão de Chuva encontrada com sucesso.",
                    CreatedItem = previsaoChuva
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    Success = false,
                    Message = $"Erro ao buscar a Previsão de Chuva: {e.Message}",
                    CreatedItem = null
                };
            }
        }

        public async Task<Response> CreateAsync(PrevisaoChuvaModel previsaoChuvaModel)
        {
            try
            {
                var existingPrevisaoChuva = await _previsaoChuvaCollection
                    .Find(x => x.PrevisaoChuva == previsaoChuvaModel.Id)
                    .FirstOrDefaultAsync();

                if (existingPrevisaoChuva != null)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Já existe uma Previsão de Chuva com esse ID."
                    };
                }
                else
                {
                    var localDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _brazilTimeZone);
                    previsaoChuvaModel.DataHora = localDateTime;

                    await _previsaoChuvaCollection.InsertOneAsync(previsaoChuvaModel);

                    return new Response
                    {
                        Success = true,
                        Message = "Previsão de Chuva cadastrada com sucesso",
                        CreatedItem = previsaoChuvaModel
                    };
                }
            }
            catch (Exception e)
            {
                return new Response
                {
                    Success = false,
                    Message = $"Erro ao cadastrar a Previsão de Chuva: {e.Message}"
                };
            }
        }

        public async Task<Response> UpdateAsync(string id, PrevisaoChuvaModel previsaoChuvaModel)
        {
            try
            {
                var existingPrevisaoChuva = await _previsaoChuvaCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

                if (existingPrevisaoChuva == null)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Previsão de Chuva não encontrada."
                    };
                }
                else
                {
                    var localDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _brazilTimeZone);
                    previsaoChuvaModel.DataHora = localDateTime;

                    await _previsaoChuvaCollection.ReplaceOneAsync(x => x.Id == id, previsaoChuvaModel);

                    return new Response
                    {
                        Success = true,
                        Message = "Previsão de Chuva atualizada com sucesso",
                        CreatedItem = previsaoChuvaModel,
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
                        Message = "ID da Previsão de Chuva inválido."
                    };
                }

                var result = await _previsaoChuvaCollection.DeleteOneAsync(x => x.Id == id);

                if (result.DeletedCount == 0)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Previsão de Chuva não encontrada para exclusão."
                    };
                }

                return new Response
                {
                    Success = true,
                    Message = "Previsão de Chuva removida com sucesso."
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    Success = false,
                    Message = $"Erro ao remover a Previsão de Chuva: {e.Message}"
                };
            }
        }

        public class Response
        {
            public bool Success { get; set; }
            public string? Message { get; set; }
            public PrevisaoChuvaModel? CreatedItem { get; set; }
        }
    }

}
