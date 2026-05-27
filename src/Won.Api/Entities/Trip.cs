using System.ComponentModel.DataAnnotations;

namespace Won.Api.Entities
{
    public class Trip
    {
        public int TripId { get; set; }
        [Required(ErrorMessage = "Trip Name Is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Start Date Is Required")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End Date Is Required")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Location Is Required")]
        public string Location { get; set; }
        public string Details { get; set; }
        [Required(ErrorMessage = "Budget Is Required")]
        public int Budget { get; set; }
        [Required(ErrorMessage = "Group Size Is Required")]
        public int GroupSize { get; set; }
    }
}
