using Microsoft.AspNetCore.Mvc;
using monitoramento_ambiental_mongodb.Models;
using monitoramento_ambiental_mongodb.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace monitoramento_ambiental_mongodb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeituraController : ControllerBase
    {
        private readonly LeituraService _leituraService;

        public LeituraController(LeituraService leituraService)
        {
            _leituraService = leituraService;
        }

        // GET: api/Leitura
        [HttpGet]
        public async Task<ActionResult<List<LeituraModel>>> GetLeituras()
        {
            var leituras = await _leituraService.GetAsync();
            return Ok(leituras);
        }

        // GET: api/Leitura/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<LeituraModel>> GetLeituraById(string id)
        {
            var response = await _leituraService.GetByIdAsync(id);

            if (!response.Success)
            {
                return NotFound(new {response.Message });
            }

            return Ok(new { Leitura = response.CreatedItem, response.Message });
        }

        // POST: api/Leitura
        [HttpPost]
        public async Task<ActionResult<LeituraModel>> PostLeitura(LeituraModel leituraModel)
        {
            var response = await _leituraService.CreateAsync(leituraModel);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return CreatedAtAction(nameof(GetLeituraById), new { id = response.CreatedItem?.Id }, response);
        }


        // PUT: api/Leitura/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeitura(string id, LeituraModel leituraModel)
        {
            leituraModel.Id = id;
            var response = await _leituraService.UpdateAsync(id, leituraModel);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return CreatedAtAction(nameof(GetLeituraById), new { id = response.CreatedItem?.Id }, response);

        }

        //DELETE: api/Leitura/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeitura(string id)
        {
            var response = await _leituraService.RemoveAsync(id);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }
    }
}
