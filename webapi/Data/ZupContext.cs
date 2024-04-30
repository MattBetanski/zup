using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Data;

public class ZupContext : DbContext {
    public ZupContext(DbContextOptions<ZupContext> options) : base (options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.UseSerialColumns();
    }

    public DbSet<Project> Project { get; set; }
    public DbSet<Department> Department { get; set; }
    public DbSet<User> User { get; set; }
}
