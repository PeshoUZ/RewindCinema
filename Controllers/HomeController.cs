using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VintageCinema.Data;
using VintageCinema.Models;
using VintageCinema.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text.Json;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _db;

    public HomeController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult AboutUs()
    {
        return View();
    }

    public IActionResult Index(DateTime? selectedDate, string? formatFilter, string? genreFilter)
    {
        if (!selectedDate.HasValue)
            selectedDate = DateTime.Today;

        var screeningsQuery = _db.Screenings
            .Include(s => s.Movie)
            .Where(s => s.DateTime.Date == selectedDate.Value.Date);

        if (!string.IsNullOrEmpty(formatFilter))
        {
            screeningsQuery = screeningsQuery.Where(s => s.Format == formatFilter);
        }

        if (!string.IsNullOrEmpty(genreFilter))
        {
            screeningsQuery = screeningsQuery.Where(s => s.Movie.Genre.Contains(genreFilter));
        }

        var groupedMovies = screeningsQuery
            .AsEnumerable()
            .GroupBy(s => s.Movie)
            .Select(g => new MovieScreeningViewModel
            {
                Movie = g.Key,
                Screenings = g.ToList()
            })
            .ToList();

        ViewData["SelectedDate"] = selectedDate.Value.ToString("yyyy-MM-dd");
        ViewData["FormatFilter"] = formatFilter;
        ViewData["GenreFilter"] = genreFilter;

        return View(groupedMovies);
    }

    public async Task<IActionResult> Reserve(int id)
    {
        // Check if the user is logged in
        if (!User.Identity.IsAuthenticated)
        {
            TempData["ErrorMessage"] = "You must be logged in to reserve seats.";
            return RedirectToAction("Login", "Account"); // Redirect to the login page
        }

        var screening = await _db.Screenings
            .Include(s => s.Movie)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (screening == null) return NotFound();

        // Load reservations into memory first
        var reservations = await _db.Reservations
            .Where(r => r.ScreeningId == id)
            .ToListAsync();

        // Convert tuples to Seat objects
        var reservedSeats = reservations
            .SelectMany(r => r.ReservedSeats.Select(seat => new Seat { Row = seat.Row, Col = seat.Col }))
            .ToList();

        var viewModel = new ReservationModel
        {
            Screening = screening,
            ReservedSeats = reservedSeats
        };

        return View(viewModel);
    }
    [HttpPost]
    public async Task<IActionResult> ConfirmReservation(int screeningId, [FromForm] string selectedSeats)
    {
        // Check if the user is logged in
        if (!User.Identity.IsAuthenticated)
        {
            TempData["ErrorMessage"] = "You must be logged in to reserve seats.";
            return RedirectToAction("Login", "Account"); // Redirect to the login page
        }

        try
        {
            // Deserialize the selected seats from JSON
            var seats = JsonSerializer.Deserialize<List<Seat>>(selectedSeats);

            Console.WriteLine("Selected Seats: " + JsonSerializer.Serialize(seats));

            var screening = await _db.Screenings.FindAsync(screeningId);
            if (screening == null)
            {
                TempData["ErrorMessage"] = "Screening not found.";
                return RedirectToAction("Index"); // Redirect to the home page
            }

            // Get the current user's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Calculate total price
            decimal pricePerSeat = screening.Price;
            decimal totalPrice = seats.Count * pricePerSeat;

            var reservation = new ReservationModel
            {
                UserId = int.Parse(userId),
                ScreeningId = screeningId,
                ReservedSeats = seats,
                Bill = totalPrice
            };

            Console.WriteLine("Reservation: " + JsonSerializer.Serialize(reservation));

            _db.Reservations.Add(reservation);
            await _db.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Reservation confirmed successfully! Your total is ${totalPrice:0.00}. You will be redirected to the home page in 5 seconds.";

            return RedirectToAction("ReservationSuccess");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error confirming reservation: " + ex.Message);

            TempData["ErrorMessage"] = "An error occurred while processing your reservation. Please try again.";
            return RedirectToAction("Reserve", new { id = screeningId }); 
        }
    }

    // New action for the ReservationSuccess view
    public IActionResult ReservationSuccess()
    {
        return View();
    }

}