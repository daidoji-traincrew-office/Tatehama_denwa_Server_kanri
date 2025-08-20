using System;
using Microsoft.EntityFrameworkCore;

namespace TatehamaKanriServer.Models
{
    public static class SessionDbInitializer
    {
        public static void Initialize()
        {
            using var context = new SessionDbContext();
            context.Database.Migrate();
        }
    }
}
