using Microsoft.AspNetCore.Mvc;
using BMICalculatorApi.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BMICalculatorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BmiController : ControllerBase
    {
        private readonly IBMIRecordRepository _repository;

        public BmiController(IBMIRecordRepository repository)
        {
            _repository = repository;
        }

        // GET: api/BMI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BMIRecord>>> GetBmiRecords()
        {
            var records = await _repository.GetAllRecordsAsync();
            return Ok(records);
        }

        // GET: api/BMI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BMIRecord>> GetBmiRecord(int id)
        {
            var bmiRecord = await _repository.GetRecordByIdAsync(id);

            if (bmiRecord == null)
            {
                return NotFound("BMI record not found.");
            }

            return Ok(bmiRecord);
        }

        // POST: api/BMI
        [HttpPost]
        public async Task<ActionResult<BMIRecord>> PostBmiRecord(BMIRecord bmiRecord)
        {
            await _repository.AddRecordAsync(bmiRecord);
            return CreatedAtAction(nameof(GetBmiRecord), new { id = bmiRecord.Id }, bmiRecord);
        }


        // PUT: api/BMI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBmiRecord(int id, BMIRecord bmiRecord)
        {
            if (id != bmiRecord.Id)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                await _repository.UpdateRecordAsync(bmiRecord);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.RecordExistsAsync(id))
                {
                    return NotFound("BMI record not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/BMI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBmiRecord(int id)
        {
            var bmiRecord = await _repository.GetRecordByIdAsync(id);
            if (bmiRecord == null)
            {
                return NotFound("BMI record not found.");
            }

            await _repository.DeleteRecordAsync(bmiRecord);
            return NoContent();
        }
    }
}
