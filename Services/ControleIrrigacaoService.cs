using MongoDB.Bson;
using MongoDB.Driver;
using monitoramento_ambiental_mongodb.Data;
using monitoramento_ambiental_mongodb.Models;

namespace monitoramento_ambiental_mongodb.Services
{
    public class ControleIrrigacaoService
    {
        private readonly IMongoCollection<ControleIrrigacaoModel> _controleIrrigacaoCollection;
        private readonly IMongoCollection<PrevisaoChuvaModel> _previsaoChuvaCollection;

        public ControleIrrigacaoService(MongoDBContext context)
        {
            _controleIrrigacaoCollection = context.ControleIrrigacoes;
            _previsaoChuvaCollection = context.PrevisoesChuva;
        }

        public async Task<List<ControleIrrigacaoModel>> GetAsync() =>
            await _controleIrrigacaoCollection.Find(x => true).ToListAsync();


        public async Task<Response> GetByIdAsync(string id)
        {
            try
            {
                var controleIrrigacao = await _controleIrrigacaoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

                if (controleIrrigacao == null)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Controle de irrigação não encontrado. Verifique o ID fornecido.",
                        CreatedItem = null
                    };
                }

                return new Response
                {
                    Success = true,
                    Message = "Controle de irrigação encontrado com sucesso.",
                    CreatedItem = controleIrrigacao
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    Success = false,
                    Message = $"Erro ao buscar o Controle de irrigação: {e.Message}",
                    CreatedItem = null
                };
            }
        }

        public async Task<Response> CreateAsync(ControleIrrigacaoModel controleIrrigacaoModel)
        {
            try
            {
                var previsaoChuva = await _previsaoChuvaCollection.Find(x => x.Id == controleIrrigacaoModel.PrevisaoChuvaId).FirstOrDefaultAsync();

                if (previsaoChuva == null)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Não consta Previsão de Chuva com esse Id"
                    };
                }

                if (!ObjectId.TryParse(previsaoChuva.Id, out _))
                {
                    return new Response
                    {
                        Success = false,
                        Message = "O ID fornecido não é válido."
                    };
                }

                controleIrrigacaoModel.PrevisaoChuva = previsaoChuva;

                await _controleIrrigacaoCollection.InsertOneAsync(controleIrrigacaoModel);

                return new Response
                {
                    Success = true,
                    Message = "Controle de irrigação criado com sucesso",
                    CreatedItem = controleIrrigacaoModel
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Success = false,
                    Message = $"Erro ao criar o Controle de Irrigação: {ex.Message}"
                };
            }
        }

        public async Task<Response> UpdateAsync(string id, ControleIrrigacaoModel controleIrrigacaoModel)
        {
            try
            {
                var existingControleIrrigacao = await _controleIrrigacaoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

                if (existingControleIrrigacao == null)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Controle de Irrigação não encontrado."
                    };
                }
                else
                {

                    var previsaoChuva = await _previsaoChuvaCollection.Find(x => x.Id == controleIrrigacaoModel.PrevisaoChuvaId).FirstOrDefaultAsync();

                    if (previsaoChuva != null)
                    {
                        controleIrrigacaoModel.PrevisaoChuvaId = previsaoChuva.Id;
                        controleIrrigacaoModel.PrevisaoChuva = previsaoChuva;
                    }

                    await _controleIrrigacaoCollection.ReplaceOneAsync(x => x.Id == id, controleIrrigacaoModel);

                    return new Response
                    {
                        Success = true,
                        Message = "Controle de Irrigação atualizado com sucesso",
                        CreatedItem = controleIrrigacaoModel,
                    };
                }
            }
            catch (Exception e)
            {
                return new Response
                {
                    Success = false,
                    Message = $"Erro ao atualizar o Controle de Irrigação: {e.Message}"
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
                        Message = "ID do Controle de Irrigação inválido."
                    };
                }

                var result = await _controleIrrigacaoCollection.DeleteOneAsync(x => x.Id == id);

                if (result.DeletedCount == 0)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Controle de Irrigação não encontrado para exclusão."
                    };
                }

                return new Response
                {
                    Success = true,
                    Message = "Controle de Irrigação removido com sucesso."
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    Success = false,
                    Message = $"Erro ao remover o Controle de Irrigação: {e.Message}"
                };
            }
        }
    
        public class Response
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public ControleIrrigacaoModel? CreatedItem { get; set; }
        }

    }

}
