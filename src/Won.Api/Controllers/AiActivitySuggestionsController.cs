using Microsoft.AspNetCore.Mvc;

using Won.Api.Services.Interfaces;
using Won.Shared.Dtos;

namespace Won.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AiActivitySuggestionsController : ControllerBase
{
    private readonly IAiActivitySuggestionService _aiActivitySuggestionService;

    public AiActivitySuggestionsController(
        IAiActivitySuggestionService aiActivitySuggestionService)
    {
        _aiActivitySuggestionService = aiActivitySuggestionService;
    }

    [HttpPost("suggestions")]
    public async Task<IActionResult> GenerateSuggestions(ActivitySuggestionRequestDto request)
    {
        var response = await _aiActivitySuggestionService.GenerateActivitySuggestionsAsync(request);

        return StatusCode(response.StatusCode, response);
    }
}