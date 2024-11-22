using Microsoft.EntityFrameworkCore;
using Web_API_Caching_with_Redis.Models;

namespace Web_API_Caching_with_Redis.Data;

public class AppDbContext:DbContext
{
    public DbSet<Driver> Drivers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options):
        base(options)
    {
        
    }

}
