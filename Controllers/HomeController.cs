using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VintageCinema.Data;
using VintageCinema.Models;
using VintageCinema.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _db;

    public HomeController(ApplicationDbContext db)
    {
        _db = db;
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
}