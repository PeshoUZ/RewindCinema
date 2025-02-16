namespace VintageCinema.Models
{
    public class ScreeningModel
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public MovieModel? Movie { get; set; } //Navigational property
        public DateTime DateTime { get; set; }
        public string Format { get; set; }
        public List<List<bool>> Seats { get; set; } = new List<List<bool>>(); // 16 rows, 16 columns
        public decimal Price { get; set; }
        public List<ReservationModel> Reservations { get; set; } = new List<ReservationModel>(); //Navigational property
    }
}
