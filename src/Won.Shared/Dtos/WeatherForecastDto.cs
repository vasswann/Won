namespace Won.Shared.Dtos;

public class WeatherForecastDto
{
    public DateOnly Date { get; set; }
    public double MinTemperature { get; set; }
    public double MaxTemperature { get; set; }
    public int WeatherCode { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
}