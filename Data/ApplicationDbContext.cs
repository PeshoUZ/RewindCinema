using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using VintageCinema.Models;

namespace VintageCinema.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<MovieModel> Movies { get; set; }
        public DbSet<ScreeningModel> Screenings { get; set; }
        public DbSet<ReservationModel> Reservations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Identity configurations

            // Configure other relationships and properties
            modelBuilder.Entity<ScreeningModel>()
                .HasOne(s => s.Movie)
                .WithMany()
                .HasForeignKey(s => s.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReservationModel>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReservationModel>()
                .HasOne(r => r.Screening)
                .WithMany()
                .HasForeignKey(r => r.ScreeningId)
                .OnDelete(DeleteBehavior.Cascade);

            // ScreeningModel - Store `Seats` as JSON
            modelBuilder.Entity<ScreeningModel>()
                .Property(s => s.Seats)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),  // Serialize bool[,]
                    v => JsonSerializer.Deserialize<List<List<bool>>>(v, (JsonSerializerOptions)null)
                )
                .HasColumnType("JSON"); // MySQL JSON type

            // ReservationModel - Store `ReservedSeats` as JSON
            modelBuilder.Entity<ReservationModel>()
                .Property(r => r.ReservedSeats)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),  // Serialize List<(int, int)>
                    v => JsonSerializer.Deserialize<List<(int, int)>>(v, (JsonSerializerOptions)null)
                )
                .HasColumnType("JSON"); // MySQL JSON type
        }     
    }
}
