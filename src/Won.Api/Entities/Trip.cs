using System.ComponentModel.DataAnnotations;

namespace Won.Api.Entities
{
    public class Trip
    {
        public int TripId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string Details { get; set; }
        public int Budget { get; set; }
        public int GroupSize { get; set; }
    }
}
