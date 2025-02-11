using Microsoft.EntityFrameworkCore;
using VintageCinema.Models;
using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace VintageCinema.Data
{
    public static class DataSeeder
    {
        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
        public static void Seed(ApplicationDbContext context)
        {
            // Ensure the database is created and migrated
            context.Database.Migrate();

            // Seed Users
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new UserModel
                    {
                        FirstName = "Admin",
                        LastName = "User",
                        Email = "admin@vintagecinema.com",
                        PasswordHash = HashPassword("12345"), // Replace with a real hashed password
                        IsAdmin = true
                    },
                    new UserModel
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "john.doe@example.com",
                        PasswordHash = HashPassword("12345"), // Replace with a real hashed password
                        IsAdmin = false
                    }
                );
                context.SaveChanges();
            }

            // Seed Movies
            if (!context.Movies.Any())
            {
                context.Movies.AddRange(
                  
                    new MovieModel
                    {
                        Title = "Inception",
                        Description = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O.",
                        Director = "Christopher Nolan",
                        ReleaseDate = 2010,
                        Genre = "Action, Sci-Fi",
                        Length = 148,
                        Category = "12+",
                        Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "covers", "Inception.jpg"))// File name in wwwroot/covers
            }
                );
                context.SaveChanges();
            }

            // Seed Screenings
            if (!context.Screenings.Any())
            {
                var movie = context.Movies.First(); // Get the first movie

                // Create a new screening
                var newScreening = new ScreeningModel
                {
                    MovieId = movie.Id,
                    DateTime = DateTime.Now.AddDays(1),
                    Format = "2D",
                    Seats = new List<List<bool>>()
                };

                // Initialize the 16x16 seats grid (all seats available, false)
                for (int i = 0; i < 16; i++)
                {
                    var row = new List<bool>();
                    for (int j = 0; j < 16; j++)
                    {
                        row.Add(false); // All seats are available (false = not taken)
                    }
                    newScreening.Seats.Add(row);
                }

                // Add the new screening to the context
                context.Screenings.Add(newScreening);

                // Save changes to the database
                context.SaveChanges();
            }
        }
    }
}
