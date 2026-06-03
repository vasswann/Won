namespace Won.Shared.Dtos
{
    public class CreateActivityDto
    {
        public int TripId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Cost { get; set; }
        public int WeatherDependency { get; set; }
        public int EnergyIntensity { get; set; }
        public int MinimumGroupSize { get; set; }
        public int MaximumGroupSize { get; set; }
        public DateTime ActivityDateTime { get; set; }
    }
}
