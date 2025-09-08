using Swashbuckle.AspNetCore.Annotations;

namespace PokerAlgoAPI.Models;

public class Request
{
    [SwaggerSchema(Description = "Each string is a player's hole cards in poker notation")]
    public required string[] Players { get; set; }
    
    [SwaggerSchema(Description = "Community cards in pokern notation, comma-separated, must be all 5")]
    public required string CommunityCards { get; set; }
}