using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Data;

public class ZupContext : DbContext {
    public ZupContext(DbContextOptions<ZupContext> options) : base (options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<DepartmentMember>()
            .HasKey(dm => new { dm.MemberId, dm.DepartmentId });
        modelBuilder.Entity<ItemAssignee>()
            .HasKey(ia => new { ia.ItemId, ia.AssingeeId });
        modelBuilder.Entity<ProjectUserRole>()
            .HasKey(pur => new { pur.ProjectId, pur.UserId, pur.RoleId });
        
        modelBuilder.UseSerialColumns();
    }

    public DbSet<Department> Department { get; set; }
    public DbSet<DepartmentMember> DepartmentMember { get; set; }
    public DbSet<Item> Item { get; set; }
    public DbSet<ItemAssignee> ItemAssignee { get; set; }
    public DbSet<ItemComment> ItemComment { get; set; }
    public DbSet<ItemHistory> ItemHistory { get; set; }
    public DbSet<Note> Note { get; set; }
    public DbSet<NoteRating> NoteRating { get; set; }
    public DbSet<Project> Project { get; set; }
    public DbSet<ProjectUserRole> ProjectUserRole { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<WikiPage> WikiPage { get; set; }
}
