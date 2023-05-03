using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }

    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Login)
                .IsRequired()
                .HasMaxLength(20);

            entity.HasIndex(e => e.Login)
                .IsUnique();

            entity.Property(u => u.Password)
                .HasMaxLength(50);

            entity.Property(u => u.FirstName)
                .HasMaxLength(40);

            entity.Property(u => u.LastName)
                .HasMaxLength(40);

            entity.Property(u => u.Gender)
                .HasMaxLength(1);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.Id);

            entity.HasIndex(o => new { o.UserId, o.OrderDate })
                .IsUnique();

            entity.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(e => e.OrderCost)
                .HasColumnType("money");

            entity.Property(e => e.ItemsDescription)
                .HasMaxLength(1000);

            entity.Property(e => e.ShippingAddress)
                .HasMaxLength(1000);
        });
    }
}