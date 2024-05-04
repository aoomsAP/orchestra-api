using Microsoft.EntityFrameworkCore;

namespace Project.Entities
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options): base(options) { }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Orchestra> Orchestras { get; set; }
        public DbSet<Musician> Musicians { get; set; }
    }
}
