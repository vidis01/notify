using Microsoft.EntityFrameworkCore;
using NotifyDB.Models;

namespace NotifyDb
{
    public class NotifyDBContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        public NotifyDBContext(DbContextOptions<NotifyDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
