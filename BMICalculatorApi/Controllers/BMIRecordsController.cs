using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BMICalculatorApi.Data;

namespace BMICalculatorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BMIRecordsController : ControllerBase
    {
        private readonly IBMIRecordRepository _repository;

        public BMIRecordsController(IBMIRecordRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BMIRecord>>> GetBMIRecords()
        {
            var records = await _repository.GetAllRecordsAsync();
            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BMIRecord>> GetBMIRecord(int id)
        {
            var record = await _repository.GetRecordByIdAsync(id);

            if (record == null)
            {
                return NotFound();
            }

            return Ok(record);
        }

        [HttpPost]
        public async Task<ActionResult<BMIRecord>> PostBMIRecord(BMIRecord record)
        {
            await _repository.AddRecordAsync(record);
            return CreatedAtAction(nameof(GetBMIRecord), new { id = record.Id }, record);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBMIRecord(int id, BMIRecord record)
        {
            if (id != record.Id)
            {
                return BadRequest();
            }

            await _repository.UpdateRecordAsync(record);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBMIRecord(int id)
        {
            var record = await _repository.GetRecordByIdAsync(id);
            if (record == null)
            {
                return NotFound();
            }

            await _repository.DeleteRecordAsync(id);

            return NoContent();
        }
    }
}