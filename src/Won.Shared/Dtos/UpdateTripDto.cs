namespace Won.Shared.Dtos;

public class UpdateTripDto
{
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public decimal Budget { get; set; }
    public int GroupSize { get; set; }
}
