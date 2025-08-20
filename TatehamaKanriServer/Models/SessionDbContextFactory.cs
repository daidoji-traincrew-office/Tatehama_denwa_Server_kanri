using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TatehamaKanriServer.Models
{
    public class SessionDbContextFactory : IDesignTimeDbContextFactory<SessionDbContext>
    {
        public SessionDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SessionDbContext>();
            optionsBuilder.UseSqlite("Data Source=phonesession.db");
            return new SessionDbContext();
        }
    }
}
