namespace VintageCinema.Models
{
    public class ReservationModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserModel? User { get; set; } // Navigational property
        public int ScreeningId { get; set; }
        public ScreeningModel? Screening { get; set; } // Navigational property

        // List of reserved seat positions (row, col)
        public List<(int Row, int Col)> ReservedSeats { get; set; } = new List<(int, int)>();

        public decimal Bill { get; set; }
    }
}
