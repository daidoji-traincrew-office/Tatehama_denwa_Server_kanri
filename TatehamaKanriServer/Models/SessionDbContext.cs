using Microsoft.EntityFrameworkCore;

namespace TatehamaKanriServer.Models
{
    public class SessionDbContext : DbContext
    {
        public DbSet<PhoneSession> PhoneSessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=phonesession.db");
        }
    }
}
