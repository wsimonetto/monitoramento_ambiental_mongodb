using Microsoft.AspNetCore.Mvc;
using monitoramento_ambiental_mongodb.Models;
using monitoramento_ambiental_mongodb.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace monitoramento_ambiental_mongodb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControleIrrigacaoController : ControllerBase
    {
        private readonly ControleIrrigacaoService _controleIrrigacaoService;

        public ControleIrrigacaoController(ControleIrrigacaoService controleIrrigacaoService)
        {
            _controleIrrigacaoService = controleIrrigacaoService;
        }

        // GET: api/ControleIrrigacao
        [HttpGet]
        public async Task<ActionResult<List<ControleIrrigacaoModel>>> GetControleIrrigacoes()
        {
            var controleIrrigacoes = await _controleIrrigacaoService.GetAsync();
            return Ok(controleIrrigacoes);
        }

        // GET: api/ControleIrrigacao/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ControleIrrigacaoModel>> GetControleIrrigacaoById(string id)
        {
            var response = await _controleIrrigacaoService.GetByIdAsync(id);

            if (!response.Success)
            {
                return NotFound(new { response.Message });
            }

            return Ok(new { ControleIrrigacao = response.CreatedItem, response.Message });
        }

        // POST: api/ControleIrrigacao
        [HttpPost]
        public async Task<ActionResult<ControleIrrigacaoModel>> PostControleIrrigacao(ControleIrrigacaoModel controleIrrigacaoModel)
        {
            var response = await _controleIrrigacaoService.CreateAsync(controleIrrigacaoModel);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return CreatedAtAction(nameof(GetControleIrrigacaoById), new { id = response.CreatedItem?.Id }, response);
        }

        // PUT: api/ControleIrrigacao/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutControleIrrigacao(string id, ControleIrrigacaoModel controleIrrigacaoModel)
        {
            var response = await _controleIrrigacaoService.UpdateAsync(id, controleIrrigacaoModel);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(new { ControleIrrigacao = response.CreatedItem, response.Message });
        }

        // DELETE: api/ControleIrrigacao/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteControleIrrigacao(string id)
        {
            var response = await _controleIrrigacaoService.RemoveAsync(id);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }

    }

}
