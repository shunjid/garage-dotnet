using Garage.Models;
using Microsoft.EntityFrameworkCore;

namespace Garage.Data;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

    public DbSet<Car> Cars { get; set; } = null!;
}