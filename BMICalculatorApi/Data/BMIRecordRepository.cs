using Microsoft.EntityFrameworkCore;

namespace BMICalculatorApi.Data
{
    public class BMIRecordRepository : IBMIRecordRepository
    {
        private readonly AppDbContext _context;

        public BMIRecordRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BMIRecord>> GetAllRecordsAsync()
        {
            return await _context.BMIRecords.ToListAsync();
        }

        public async Task<BMIRecord?> GetRecordByIdAsync(int id)
        {
            return await _context.BMIRecords.FindAsync(id);
        }

        public async Task AddRecordAsync(BMIRecord record)
        {
            _context.BMIRecords.Add(record);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRecordAsync(BMIRecord record)
        {
            _context.Entry(record).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRecordAsync(BMIRecord record)
        {
            _context.BMIRecords.Remove(record);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RecordExistsAsync(int id)
        {
            return await _context.BMIRecords.AnyAsync(e => e.Id == id);
        }
    }
}