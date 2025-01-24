using Microsoft.EntityFrameworkCore;

namespace OneBigRun2025.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Participant> Participants { get; set; }
        public DbSet<Registration> Registration { get; set; }
    }
}

