using Swashbuckle.AspNetCore.Annotations;

namespace PokerAlgoAPI.Models;

public class Request
{
    [SwaggerSchema(Description = "Each string represents a player's hole cards separated by a comma in poker notation. \"Ac,Tc\"")]
    public required string[] Players { get; set; }
    
    [SwaggerSchema(Description = "Community cards in poker notation, comma-separated, must be all 5.")]
    public required string CommunityCards { get; set; }
}