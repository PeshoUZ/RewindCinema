using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VintageCinema.Data;
using VintageCinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Authorize(Policy = "RequireEditorRole")] // Only Admins can access
public class AdminController : Controller
{
    private readonly ApplicationDbContext _db;

    public AdminController(ApplicationDbContext db)
    {
        _db = db;
    }

    // Display Admin Panel
    public async Task<IActionResult> IndexAdmin()
    {
        var user = await _db.Users
            .Where(u => u.Email == User.Identity.Name)
            .FirstOrDefaultAsync();

        if (user == null || !user.IsAdmin)
        {
            return Forbid(); // Restrict access
        }

        var screenings = await _db.Screenings.Include(s => s.Movie).ToListAsync();
        ViewBag.Movies = await _db.Movies.ToListAsync();
        return View(screenings);
    }

    // Create Screening (POST)
    [HttpPost]
    public async Task<IActionResult> Create(ScreeningModel screening)
    {
        Console.WriteLine($"🛠️ DEBUG: MovieId: {screening.MovieId}, DateTime: {screening.DateTime}, Format: {screening.Format}, Price: {screening.Price}");

        // Fetch the Movie manually
        var movie = await _db.Movies.FindAsync(screening.MovieId);
        if (movie == null)
        {
            Console.WriteLine("❌ ERROR: Movie not found.");
            ModelState.AddModelError("MovieId", "Invalid movie selected.");
        }
        else
        {
            screening.Movie = movie; // Assign movie manually
        }

        if (!ModelState.IsValid)
        {
            Console.WriteLine("❌ ERROR: ModelState is invalid.");
            ViewBag.Movies = await _db.Movies.ToListAsync();
            return View("IndexAdmin", await _db.Screenings.Include(s => s.Movie).ToListAsync());
        }

        // Initialize seats (16x16 grid)
        screening.Seats = GenerateEmptySeats();

        _db.Screenings.Add(screening);
        await _db.SaveChangesAsync();

        Console.WriteLine("✅ SUCCESS: Screening added!");
        return RedirectToAction("IndexAdmin");
    }

    // Delete Screening
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var screening = await _db.Screenings.FindAsync(id);
        if (screening != null)
        {
            _db.Screenings.Remove(screening);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction("IndexAdmin");
    }

    // View Reservations
    [HttpGet]
    public async Task<IActionResult> Reservations(int screeningId)
    {
        var reservations = await _db.Reservations
            .Include(r => r.User)
            .Include(r => r.Screening)
            .ThenInclude(s => s.Movie)
            .Where(r => r.ScreeningId == screeningId)
            .ToListAsync();

        if (reservations == null || !reservations.Any())
        {
            ViewBag.Message = "No reservations found for this screening.";
        }

        return View(reservations);
    }

    // Helper function to create an empty 16x16 seat layout
    private List<List<bool>> GenerateEmptySeats()
    {
        return Enumerable.Range(0, 16)
            .Select(_ => Enumerable.Repeat(false, 16).ToList())
            .ToList();
    }
}