namespace VintageCinema.Models.ViewModels
{
    public class MovieScreeningViewModel
    {
        public MovieModel Movie { get; set; }
        public List<ScreeningModel> Screenings { get; set; }
    }
}