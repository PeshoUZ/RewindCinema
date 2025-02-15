using Microsoft.EntityFrameworkCore;
using VintageCinema.Models;
using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Humanizer.Localisation;
using System.IO;

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
                        Description = "Dom Cobb is a skilled thief, not of material wealth, but of secrets buried deep within" +
                        " the subconscious mind. Using advanced dream-sharing technology, he is able to enter the dreams of others" +
                        " to extract valuable information. When he is offered a chance to erase his criminal past and reunite with" +
                        " his children, Cobb must do the impossible—implant an idea instead of stealing one. As he assembles a team" +
                        " for the mission, the line between dreams and reality begins to blur, and the stakes become life-threatening.",
                        Director = "Christopher Nolan",
                        ReleaseDate = 2010,
                        Genre = "Action, Sci-Fi",
                        Length = 148,
                        Category = "12+",
                        Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Jpg", "Inception.jpg"))// File name in wwwroot/covers
                    },
                    new MovieModel
                    {
                        Title = "The Matrix",
                        Description = "A computer hacker named Neo discovers that the world he lives in is a simulated reality" +
                        " created by intelligent machines to subjugate humanity. With the help of a group of rebels led by the enigmatic" +
                        " Morpheus and the skilled warrior Trinity, Neo must embrace his destiny as 'The One' and fight against the agents" +
                        " of the system, particularly the relentless Agent Smith, in a battle to free mankind from its digital prison.",
                        Director = "Lana Wachowski, Lilly Wachowski",
                        ReleaseDate = 1999,
                        Genre = "Action, Sci-Fi",
                        Length = 136,
                        Category = "15+",
                        Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Jpg", "TheMatrix.jpg"))// File name in wwwroot/covers
                    },
                    new MovieModel
                    {
                        Title = "A Quiet Place",
                        Description = "In a post-apocalyptic world overrun by creatures that hunt by sound, a family struggles to survive" +
                        " by living in complete silence. As they navigate their dangerous reality, a mother prepares to give birth, leading" +
                        " to a terrifying night where they must fight for their lives without making a single noise.",
                        Director = "John Krasinski",
                        ReleaseDate = 2018,
                        Genre = "Horror, Sci-Fi",
                        Length = 90,
                        Category = "12+",
                        Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Jpg", "AQuietPlace.jpg"))// File name in wwwroot/covers
                    },
                    new MovieModel
                    {
                        Title = "Crazy Rich Asians",
                        Description = "When Rachel, a Chinese-American professor, travels to Singapore with her boyfriend Nick for a wedding," +
                        " she is shocked to discover that he belongs to one of the wealthiest families in Asia. Facing scrutiny and jealousy from" +
                        " the elite, Rachel must navigate family drama, extravagant lifestyles, and cultural expectations in a battle for love" +
                        " and self-worth.",
                        Director = "Jon M. Chu",
                        ReleaseDate = 2018,
                        Genre = "Comedy, Romance",
                        Length = 121,
                        Category = "12+",
                        Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Jpg", "CrazyRichAsians.jpg"))// File name in wwwroot/covers
                    },
                    new MovieModel
                    {
                        Title = "Dune: Part Two",
                        Description = "Following the tragic fall of House Atreides, Paul Atreides and his mother, Lady Jessica, seek refuge among" +
                        " the Fremen in the harsh deserts of Arrakis. As Paul embraces his destiny, he must navigate political alliances," +
                        " mystical prophecies, and a brewing war against House Harkonnen and the oppressive rule of the Emperor. With Chani" +
                        " and the Fremen by his side, Paul rises as a leader, challenging the galaxy’s power structure and embracing his role" +
                        " as the prophesied Kwisatz Haderach in a battle that will change the fate of Arrakis forever.",
                        Director = "Denis Villeneuve",
                        ReleaseDate = 2024,
                        Genre = "Sci-Fi, Adventure, Action",
                        Length = 166,
                        Category = "12+",
                        Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Jpg", "Dune2.jpg"))// File name in wwwroot/covers
                    },
                    new MovieModel
                    {
                        Title = "Get Out",
                        Description = "Chris, a young Black man, visits the family estate of his white girlfriend, Rose, for a weekend getaway." +
                        " At first, he finds their overly accommodating behavior unsettling, but as the visit progresses, a series of bizarre and" +
                        " disturbing events lead him to uncover a horrifying secret about the seemingly perfect community. Trapped in a nightmare," +
                        " Chris must fight to escape before it's too late.",
                        Director = "Jordan Peele",
                        ReleaseDate = 2017,
                        Genre = "Horror, Mystery, Thriller",
                        Length = 104,
                        Category = "15+",
                        Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Jpg", "GetOut.jpg"))// File name in wwwroot/covers
                    }, 
                    new MovieModel
                    {
                        Title = "Gladiator",
                        Description = "Maximus Decimus Meridius, a respected general of the Roman army, is betrayed when the corrupt emperor" +
                        " Commodus seizes power and orders his execution. Left for dead and forced into slavery, Maximus rises as a gladiator," +
                        " fighting for survival in the Colosseum. With vengeance in his heart, he must navigate the brutal world of the arena to" +
                        " challenge the emperor and restore honor to Rome and his fallen family.",
                        Director = "Ridley Scott",
                        ReleaseDate = 2000,
                        Genre = "Action, Drama",
                        Length = 155,
                        Category = "15+",
                        Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Jpg", "Gladiator.jpg"))// File name in wwwroot/covers
                    },
                    new MovieModel
                    {
                        Title = "Harry Potter and the Sorcerer’s Stone",
                        Description = "An orphaned boy, Harry Potter, discovers that he is a wizard and has been accepted into the magical Hogwarts" +
                        " School of Witchcraft and Wizardry. As he learns about his past and the dark wizard who killed his parents, Harry and his" +
                        " friends must uncover the mystery behind the Sorcerer’s Stone before it falls into the wrong hands.",
                        Director = "Chris Columbus",
                        ReleaseDate = 2001,
                        Genre = "Fantasy, Adventure",
                        Length = 152,
                        Category = "PG",
                        Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Jpg", "HarryPotter.jpg"))// File name in wwwroot/covers
                    },
                    new MovieModel
                    {
                        Title = "Hereditary",
                        Description = "When the matriarch of the Graham family passes away, her daughter and grandchildren uncover" +
                        " disturbing secrets about their ancestry. As supernatural forces take hold, the family spirals into paranoia" +
                        " and terror, realizing that their fate may be sealed by an inescapable, malevolent presence.",
                        Director = "Ari Aster",
                        ReleaseDate = 2018,
                        Genre = "Horror, Drama",
                        Length = 127,
                        Category = "15+",
                        Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Jpg", "Heriditary.jpg"))// File name in wwwroot/covers
                    },
                    new MovieModel
                    {

                        Title = "It (2017)",
                        Description = "When children in the town of Derry start disappearing, a group of bullied kids known as" +
                        " 'The Losers Club' band together to face their worst fears. They soon discover that the cause of these" +
                        " disappearances is Pennywise, a terrifying shape-shifting entity that preys on their deepest fears." +
                        " The group must work together to overcome their trauma and stop the ancient evil before it claims more victims.",
                        Director = "Andy Muschietti",
                        ReleaseDate = 2017,
                        Genre = "Horror",
                        Length = 135,
                        Category = "15+",
                        Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Jpg", "It.jpg"))// File name in wwwroot/covers
                    },
                    new MovieModel
                    {

                        Title = "The Lord of the Rings: The Fellowship of the Ring",
                        Description = "In the land of Middle-earth, a young hobbit named Frodo Baggins inherits a powerful" +
                        " and dangerous ring once belonging to the dark lord Sauron. With the fate of the world at stake," +
                        " Frodo embarks on a perilous journey to destroy the ring in the fires of Mount Doom. Accompanied by" +
                        " a diverse group of warriors, wizards, and friends, he must battle evil forces and resist the corrupting" +
                        " power of the ring before darkness consumes the world.",
                        Director = "Peter Jackson",
                        ReleaseDate = 2001,
                        Genre = "Adventure, Fantasy",
                        Length = 178,
                        Category = "12+",
                        Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Jpg", "LOTR.jpg"))// File name in wwwroot/covers
                    },
                    new MovieModel
                    {
                        Title = "Mad Max: Fury Road",
                        Description = "In a post-apocalyptic wasteland ruled by a ruthless warlord, Max Rockatansky joins forces" +
                        " with Imperator Furiosa to escape his captors and lead a group of women to freedom. Pursued by heavily " +
                        "armed war parties, they must battle across the desert in a high-octane chase for survival, pushing their" +
                        " limits to break free from tyranny.",
                        Director = "George Miller",
                        ReleaseDate = 2015,
                        Genre = "Action, Sci-Fi",
                        Length = 120,
                        Category = "15+",
                        Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Jpg", "MadMaxjpg.jpg"))// File name in wwwroot/covers
                    },
                    new MovieModel
                    {
                        Title = "Pan’s Labyrinth",
                        Description = "In post-Civil War Spain, young Ofelia discovers an ancient labyrinth and meets a mysterious" +
                        " faun who claims she is a lost princess. To reclaim her place in the magical realm, she must complete three" +
                        " dangerous tasks. As reality and fantasy collide, Ofelia must find the courage to fight for her destiny.",
                        Director = "Guillermo del Toro",
                        ReleaseDate = 2006,
                        Genre = "Fantasy, Drama, War",
                        Length = 118,
                        Category = "15+",
                        Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Jpg", "Pan'sLabyrinth.jpg"))// File name in wwwroot/covers
                    },
                    new MovieModel
                    {
                        Title = "Pirates of the Caribbean: The Curse of the Black Pearl",
                        Description = "Captain Jack Sparrow, a cunning and eccentric pirate, finds himself caught in a battle" +
                        " to reclaim his stolen ship, the Black Pearl, from a crew of cursed pirates. Alongside Will Turner," +
                        " a blacksmith seeking to rescue his kidnapped love, Jack must navigate betrayals, sword fights, and" +
                        " supernatural forces to reclaim what is rightfully his.",
                        Director = "Gore Verbinski",
                        ReleaseDate = 2003,
                        Genre = "Fantasy, Action, Adventure",
                        Length = 143,
                        Category = "12+",
                        Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Jpg", "PiratesOfTheCaribbean.jpg"))// File name in wwwroot/covers
                    },
                    new MovieModel
                    {
                        Title = "Step Brothers",
                        Description = "Brennan and Dale are two lazy, immature adults still living at home when their single parents get married." +
                        " Forced to become stepbrothers, their childish rivalry quickly spirals out of control, leading to outrageous fights," +
                        " pranks, and chaos. However, when they finally bond over their shared interests, they must prove to their parents" +
                        " that they are capable of being responsible adults.",
                        Director = "Adam McKay",
                        ReleaseDate = 2008,
                        Genre = "Comedy",
                        Length = 98,
                        Category = "15+",
                        Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Jpg", "StepBrothers.jpg"))// File name in wwwroot/covers
                    },
                    new MovieModel
                    {
                        Title = "Superbad",
                        Description = "High school seniors Seth and Evan have one goal before they graduate—lose their virginity. " +
                        "When they get invited to a party, they embark on a chaotic adventure to secure alcohol and impress their " +
                        "crushes. Along the way, they encounter awkward situations, questionable cops, and a series of hilarious " +
                        "mishaps that test their friendship and ultimately lead to self-discovery.",
                        Director = "Greg Mottola",
                        ReleaseDate = 2007,
                        Genre = "Comedy",
                        Length = 113,
                        Category = "15+",
                        Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Jpg", "Superbad.jpg"))// File name in wwwroot/covers
                    },
                    new MovieModel
                    {
                        Title = "The Conjuring",
                        Description = "Based on real-life events, this chilling horror story follows paranormal investigators" +
                        " Ed and Lorraine Warren as they take on one of the most terrifying cases of their careers. When the" +
                        " Perron family moves into an old farmhouse, they experience a series of disturbing supernatural occurrences." +
                        " As the Warrens dig deeper into the house's dark past, they uncover a sinister presence" +
                        " that threatens to tear the family apart.",
                        Director = "James Wan",
                        ReleaseDate = 2013,
                        Genre = "Horror, Mystery, Thriller",
                        Length = 112,
                        Category = "15+",
                        Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Jpg", "TheConjuring.jpg"))// File name in wwwroot/covers
                    },
                    new MovieModel
                    {
                        Title = "The Dark Knight",
                        Description = "With Gotham City plagued by crime and corruption, Batman, the masked vigilante, strives" +
                        " to restore order. However, his greatest challenge emerges in the form of the Joker, a sadistic criminal" +
                        " mastermind who seeks to create chaos and test Batman’s moral limits. As the city spirals into anarchy," +
                        " Bruce Wayne must make impossible choices, pushing his limits as a hero while facing personal sacrifices" +
                        " in his battle against darkness.",
                        Director = "Christopher Nolan",
                        ReleaseDate = 2008,
                        Genre = "Action, Crime, Drama",
                        Length = 152,
                        Category = "12+",
                        Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Jpg", "TheDarkKnight.jpg"))// File name in wwwroot/covers
                    },
                    new MovieModel
                    {
                        Title = "The Hangover",
                        Description = "A bachelor party in Las Vegas takes a wild turn when three groomsmen wake up with no memory" +
                        " of the previous night and the groom missing. As they retrace their steps, they discover a stolen tiger," +
                        " a baby in the closet, and an angry gangster on their trail. With time running out, they must piece together" +
                        " the events of the night and find their friend before the wedding.",
                        Director = "Todd Phillips",
                        ReleaseDate = 2009,
                        Genre = "Comedy",
                        Length = 100,
                        Category = "15+",
                        Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Jpg", "TheHangover.jpg"))// File name in wwwroot/covers
                    },
                    new MovieModel
                    {
                        Title = "The Notebook",
                        Description = "In the 1940s, a young couple, Noah and Allie, fall deeply in love despite coming from different" +
                        " social backgrounds. Torn apart by circumstances and family interference, their love story unfolds through" +
                        " decades of longing, separation, and an emotional reunion that proves true love never fades.",
                        Director = "Nick Cassavetes",
                        ReleaseDate = 2004,
                        Genre = "Romance, Drama",
                        Length = 123,
                        Category = "12+",
                        Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Jpg", "TheNotebook.jpg"))// File name in wwwroot/covers
                    },
                    new MovieModel
                    {
                        Title = "The Wolf of Wall Street",
                        Description = "Jordan Belfort rises from a small-time stockbroker to a wealthy, corrupt Wall Street tycoon." +
                        " Engaging in a life of excess filled with parties, drugs, and outrageous schemes, he manipulates the stock" +
                        " market to amass a fortune. As the FBI closes in, Jordan’s greed and recklessness threaten to bring his empire" +
                        " crashing down in this high-energy, dark comedy about power and excess.",
                        Director = "Martin Scorsese",
                        ReleaseDate = 2013,
                        Genre = "Biography, Comedy, Crime",
                        Length = 180,
                        Category = "18+",
                        Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Jpg", "TheWolfOfWallStreet.jpg"))// File name in wwwroot/covers
                    },
                    new MovieModel
                    {
                        Title = "Titanic",
                        Description = "Jack Dawson, a young, free-spirited artist, wins a ticket to board the RMS Titanic," +
                        " the most luxurious ship of its time. Onboard, he meets Rose DeWitt Bukater, a wealthy young woman " +
                        "trapped in an engagement she dreads. As they fall deeply in love, their romance is tested when the " +
                        "Titanic strikes an iceberg, leading to a catastrophic disaster that will challenge their fate and survival.",
                        Director = "James Cameron",
                        ReleaseDate = 1997,
                        Genre = "Drama, Romance",
                        Length = 195,
                        Category = "12+",
                        Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Jpg", "Titanic.jpg"))// File name in wwwroot/covers
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
