using Microsoft.AspNetCore.Mvc;
using monitoramento_ambiental_mongodb.Models;
using monitoramento_ambiental_mongodb.Services;

namespace monitoramento_ambiental_mongodb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrevisaoChuvaController : ControllerBase
    {
        private readonly PrevisaoChuvaService _previsaoChuvaService;

        public PrevisaoChuvaController(PrevisaoChuvaService previsaoChuvaService)
        {
            _previsaoChuvaService = previsaoChuvaService;
        }

        // GET: api/PrevisaoChuva
        [HttpGet]
        public async Task<ActionResult<List<PrevisaoChuvaModel>>> GetPrevisoesChuva() =>
            Ok(await _previsaoChuvaService.GetAsync());

        // GET: api/PrevisaoChuva/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PrevisaoChuvaModel>> GetPrevisaoChuvaById(string id)
        {
            var response = await _previsaoChuvaService.GetByIdAsync(id);

            if (!response.Success)
            {
                return NotFound(new { response.Message });
            }

            return Ok(new { PrevisaoChuva = response.CreatedItem, response.Message });
        }

        // POST: api/PrevisaoChuva
        [HttpPost]
        public async Task<ActionResult<PrevisaoChuvaModel>> PostPrevisaoChuva(PrevisaoChuvaModel previsaoChuvaModel)
        {
            var response = await _previsaoChuvaService.CreateAsync(previsaoChuvaModel);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return CreatedAtAction(nameof(GetPrevisaoChuvaById), new { id = response.CreatedItem?.Id }, response);
        }

        // PUT: api/PrevisaoChuva/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrevisaoChuva(string id, PrevisaoChuvaModel previsaoChuvaModel)
        {
            var response = await _previsaoChuvaService.UpdateAsync(id, previsaoChuvaModel);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return CreatedAtAction(nameof(GetPrevisaoChuvaById), new { id = response.CreatedItem?.Id }, response);
        }

        // DELETE: api/PrevisaoChuva/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrevisaoChuva(string id)
        {
            var response = await _previsaoChuvaService.RemoveAsync(id);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }

    }

}
