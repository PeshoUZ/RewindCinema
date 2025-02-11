using System.Reflection.Metadata;

namespace VintageCinema.Models
{
    public class MovieModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public int ReleaseDate {  get; set; } //It need to be just an year
        public string Genre { get; set; }
        public int Length { get; set; }//It should be in minutes (whole number)
        public string Category {  get; set; } //It should be in this format "18+"
        public byte[] Picture { get; set; } 
    }
}
