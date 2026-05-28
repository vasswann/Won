using System.ComponentModel.DataAnnotations;

namespace Won.Shared.Models
{
    public class Trip
    {
        public int TripId { get; set; }
        public string Name { get; set; } = String.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; } = String.Empty;
        public string Details { get; set; } = String.Empty;
        public decimal Budget { get; set; }
        public int GroupSize { get; set; }
    }
}
