using Microsoft.AspNetCore.Mvc;
using VintageCinema.Data;
using VintageCinema.Models;
using System.Collections.Generic;
using System.Linq;

public class MoviesController : Controller
{
    private readonly ApplicationDbContext _db;

    public MoviesController(ApplicationDbContext db)
    {
        _db = db;
    }

    // All movies
    public IActionResult Movies()
    {
        var movies = _db.Movies
        .Select(m => new MovieModel
        {
            Id = m.Id,
            Title = m.Title,
        })
        .ToList();

        return View(movies);
    }

    // Movie details
    public IActionResult Details(int id)
    {
        var movie = _db.Movies.FirstOrDefault(m => m.Id == id);
        if (movie == null)
        {
            return NotFound();
        }
        return View(movie);
    }
}