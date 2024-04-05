using Microsoft.EntityFrameworkCore;
using PassIn.Infrastructure.Entities;

namespace PassIn.Infrastructure;
public class PassInDbContext : DbContext
{
    public DbSet<CheckIn> CheckIns { get; set; }

    public DbSet<Event> Events { get; set; }

    public DbSet<Attendee> Attendees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = C:\\Users\\ander\\Downloads\\PassInDb.db");
    }
}
