namespace Won.Shared.Dtos
{
    public class RecommendedActivityDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal EstimatedCost { get; set; }
        public int EnergyLevel { get; set; }
        public int WeatherSuitability { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime SuggestedDateTime { get; set; }
    }
}
