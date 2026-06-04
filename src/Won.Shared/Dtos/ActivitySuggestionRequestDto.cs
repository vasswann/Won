namespace Won.Shared.Dtos;

public class ActivitySuggestionRequestDto
{
    public string Location { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Budget { get; set; }
    public int GroupSize { get; set; }
}