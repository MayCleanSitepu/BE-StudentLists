using Kampus.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kampus.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Students> StudentDatas{ get; set; }

        public override int SaveChanges()
        {
            var currentDate = DateTime.Now;
            // Format TahunBulan (2501 = Januari 2025)
            var currentYearMonth = currentDate.ToString("yyMM"); 

            // Ambil increment terakhir
            var lastStudent = StudentDatas
                .Where(s => s.StudentId.StartsWith(currentYearMonth))
                .OrderByDescending(s => s.StudentId)
                .FirstOrDefault();

            int increment = 1;
            if (lastStudent != null)
            {
                // Ambil digit terakhir
                var lastIncrement = int.Parse(lastStudent.StudentId.Substring(4, 2));
                increment = lastIncrement + 1;
            }

            var newStudentId = currentYearMonth + increment.ToString("D2"); 

            // Set StudentId 
            var student = ChangeTracker.Entries<Students>()
                .Where(e => e.State == EntityState.Added)
                .Select(e => e.Entity)
                .FirstOrDefault();

            if (student != null)
            {
                student.StudentId = newStudentId;
            }

            return base.SaveChanges();
        }


    }
}
