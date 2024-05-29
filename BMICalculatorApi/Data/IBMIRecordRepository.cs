using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMICalculatorApi.Data
{
    public interface IBMIRecordRepository
    {
        Task<IEnumerable<BMIRecord>> GetAllRecordsAsync();
        Task<BMIRecord?> GetRecordByIdAsync(int id); 
        Task AddRecordAsync(BMIRecord record);
        Task UpdateRecordAsync(BMIRecord record);
        Task DeleteRecordAsync(int id);
        Task<bool> RecordExistsAsync(int id);
    }
}