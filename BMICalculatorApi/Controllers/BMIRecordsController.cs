using Microsoft.AspNetCore.Mvc;
using BMICalculatorApi.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BMICalculatorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BmiController : ControllerBase
    {
        private readonly IBMIRecordRepository _repository;
        private readonly ILogger<BmiController> _logger;

        public BmiController(IBMIRecordRepository repository, ILogger<BmiController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // GET: api/BMI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BMIRecord>>> GetBmiRecords()
        {
            _logger.LogInformation("Fetching all BMI records");
            var records = await _repository.GetAllRecordsAsync();
            return Ok(records);
        }

        // GET: api/BMI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BMIRecord>> GetBmiRecord(int id)
        {
            _logger.LogInformation($"Fetching BMI record with id: {id}");
            var bmiRecord = await _repository.GetRecordByIdAsync(id);

            if (bmiRecord == null)
            {
                _logger.LogWarning($"BMI record with id: {id} not found");
                return NotFound("BMI record not found.");
            }

            return Ok(bmiRecord);
        }

        // POST: api/BMI
        [HttpPost]
        public async Task<ActionResult<BMIRecord>> PostBmiRecord(BMIRecord bmiRecord)
        {
            _logger.LogInformation($"Adding new BMI record for {bmiRecord.Name}");
            bmiRecord.RecordedDate = DateTime.UtcNow; // Set the current UTC time
            await _repository.AddRecordAsync(bmiRecord);
            return CreatedAtAction(nameof(GetBmiRecord), new { id = bmiRecord.Id }, bmiRecord);
        }



        // PUT: api/BMI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBmiRecord(int id, BMIRecord bmiRecord)
        {
            if (id != bmiRecord.Id)
            {
                _logger.LogWarning("ID mismatch when updating BMI record");
                return BadRequest("ID mismatch.");
            }

            try
            {
                _logger.LogInformation($"Updating BMI record with id: {id}");
                await _repository.UpdateRecordAsync(bmiRecord);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.RecordExistsAsync(id))
                {
                    _logger.LogWarning($"BMI record with id: {id} not found during update");
                    return NotFound("BMI record not found.");
                }
                else
                {
                    _logger.LogError($"Concurrency error while updating BMI record with id: {id}");
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/BMI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBmiRecord(int id)
        {
            _logger.LogInformation($"Deleting BMI record with id: {id}");
            var bmiRecord = await _repository.GetRecordByIdAsync(id);
            if (bmiRecord == null)
            {
                _logger.LogWarning($"BMI record with id: {id} not found during delete");
                return NotFound("BMI record not found.");
            }

            await _repository.DeleteRecordAsync(bmiRecord);
            return NoContent();
        }
    }
}
