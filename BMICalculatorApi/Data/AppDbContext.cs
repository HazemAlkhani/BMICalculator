using Microsoft.EntityFrameworkCore;

namespace BMICalculatorApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<BMIRecord> BMIRecords { get; set; }
    }
}