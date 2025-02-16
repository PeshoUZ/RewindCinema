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
        public List<Seat> ReservedSeats { get; set; } = new List<Seat>();

        public decimal Bill { get; set; }
    }

    // Define a Seat class for deserialization
    public class Seat
    {
        public int Row { get; set; }
        public int Col { get; set; }
    }
}
