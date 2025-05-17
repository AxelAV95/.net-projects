using Microsoft.EntityFrameworkCore;

public class BlogDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UserName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Role).IsRequired();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("SYSDATETIME()");
        });
    }
}
