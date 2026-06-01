using Microsoft.EntityFrameworkCore;
using Won.Api.Entities;

namespace Won.Api.Data;

public class WonDbContext : DbContext
{
    public WonDbContext(DbContextOptions<WonDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Trip> Trips => Set<Trip>();
    public DbSet<Activity> Activities => Set<Activity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            entity.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(256);

            entity.HasIndex(x => x.Email)
                .IsUnique();

            entity.Property(x => x.PasswordHash)
                .IsRequired();

            entity.Property(x => x.CreatedAt)
                .IsRequired();
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.ToTable("Trips");

            entity.HasKey(x => x.TripId);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(x => x.Location)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(x => x.Details)
                .HasMaxLength(1000);

            entity.Property(x => x.Budget)
                .HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<Activity>(entity =>
        {
            entity.ToTable("Activities", table =>
            {
                table.HasCheckConstraint(
                    "CK_Activities_WeatherDependency",
                    "\"WeatherDependency\" >= 1 AND \"WeatherDependency\" <= 10");

                table.HasCheckConstraint(
                    "CK_Activities_EnergyIntensity",
                    "\"EnergyIntensity\" >= 1 AND \"EnergyIntensity\" <= 10");

                table.HasCheckConstraint(
                    "CK_Activities_GroupSize",
                    "\"MinimumGroupSize\" <= \"MaximumGroupSize\"");
            });

            entity.HasKey(x => x.ActivityId);

            entity.Property(x => x.ActivityId)
                .ValueGeneratedOnAdd();

            entity.Property(x => x.TripId)
                .IsRequired();

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(x => x.WeatherDependency)
                .IsRequired();

            entity.Property(x => x.Cost)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            entity.Property(x => x.EnergyIntensity)
                .IsRequired();

            entity.Property(x => x.MinimumGroupSize)
                .IsRequired();

            entity.Property(x => x.MaximumGroupSize)
                .IsRequired();

            entity.Property(x => x.ActivityDateTime)
                .IsRequired();

            entity.Property(x => x.CreatedAt)
                .IsRequired();

            entity.HasOne(x => x.Trip)
                .WithMany()
                .HasForeignKey(x => x.TripId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}