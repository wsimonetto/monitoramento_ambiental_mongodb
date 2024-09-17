using Microsoft.AspNetCore.Mvc;
using monitoramento_ambiental_mongodb.Models;
using monitoramento_ambiental_mongodb.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace monitoramento_ambiental_mongodb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertaController : ControllerBase
    {
        private readonly AlertaService _alertaService;

        public AlertaController(AlertaService alertaService)
        {
            _alertaService = alertaService;
        }

        // GET: api/alerta
        [HttpGet]
        public async Task<ActionResult<List<AlertaModel>>> GetAlertas()
        {
            var alertas = await _alertaService.GetAsync();
            return Ok(alertas);
        }

        // GET: api/alerta/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AlertaModel>> GetAlertaById(string id)
        {
            var response = await _alertaService.GetByIdAsync(id);

            if (!response.Success)
            {
                return NotFound(new { response.Message });
            }

            return Ok(new { Alerta = response.CreatedItem, response.Message });
        }

        // POST: api/alerta
        [HttpPost]
        public async Task<ActionResult<AlertaModel>> PostAlerta(AlertaModel alertaModel)
        {
            var response = await _alertaService.CreateAsync(alertaModel);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return CreatedAtAction(nameof(GetAlertaById), new { id = response.CreatedItem?.Id }, response);
        }

        // PUT: api/alerta/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlerta(string id, AlertaModel alertaModel)
        {
            var existingAlerta = await _alertaService.GetByIdAsync(id);
            if (existingAlerta == null)
            {
                return NotFound();
            }
            await _alertaService.UpdateAsync(id, alertaModel);//
            return NoContent();
        }

        // DELETE: api/alerta/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlerta(string id)
        {
            var response = await _alertaService.RemoveAsync(id);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }

    }

}
