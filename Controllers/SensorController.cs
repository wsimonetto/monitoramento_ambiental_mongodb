using Microsoft.AspNetCore.Mvc;
using monitoramento_ambiental_mongodb.Models;
using monitoramento_ambiental_mongodb.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace monitoramento_ambiental_mongodb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly SensorService _sensorService;

        public SensorController(SensorService sensorService)
        {
            _sensorService = sensorService;
        }

        // GET: api/sensor
        [HttpGet]
        public async Task<ActionResult<List<SensorModel>>> GetSensores()
        {
            var sensors = await _sensorService.GetAsync();
            return Ok(sensors);
        }

        // GET: api/sensor/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SensorModel>> GetSensorById(string id)
        {
            var response = await _sensorService.GetByIdAsync(id);

            if (!response.Success)
            {
                return NotFound(new { response.Message });
            }

            return Ok(new { Sensor = response.CreatedItem, response.Message });
        }

        // POST: api/sensor
        [HttpPost]
        public async Task<ActionResult<SensorModel>> PostSensor(SensorModel sensorModel)
        {
            var response = await _sensorService.CreateAsync(sensorModel);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return CreatedAtAction(nameof(GetSensorById), new { id = response.CreatedItem?.Id }, response);
        }

        // PUT: api/sensor/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSensor(string id, SensorModel sensorModel)
        {
            var response = await _sensorService.UpdateAsync(id, sensorModel);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return CreatedAtAction(nameof(GetSensorById), new { id = response.CreatedItem?.Id }, response);
        }

        // DELETE: api/sensor/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensor(string id)
        {
            var response = await _sensorService.RemoveAsync(id);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }

    }

 }

