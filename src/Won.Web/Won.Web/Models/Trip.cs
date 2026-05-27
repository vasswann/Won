using System.ComponentModel.DataAnnotations;

namespace Won.Shared.Models
{
    public class Trip
    {
        public int TripId { get; set; }
        [Required(ErrorMessage = "Trip Name Is Required")]
        public string Name { get; set; } = String.Empty;
        [Required(ErrorMessage = "Start Date Is Required")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End Date Is Required")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Location Is Required")]
        public string Location { get; set; } = String.Empty;
        public string Details { get; set; } = String.Empty;
        [Required(ErrorMessage = "Budget Is Required")]
        public decimal Budget { get; set; }
        [Required(ErrorMessage = "Group Size Is Required")]
        public int GroupSize { get; set; }
    }
}
