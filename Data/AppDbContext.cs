using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Platform> Platforms { get; set; }

    public DbSet<Well> Wells { get; set; }

    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=localhost;Database=AemenersolDB;Trusted_Connection=True;TrustServerCertificate=True;user=ulyasarah\Programmer;password=programmer;");
    }
}