namespace PokerAlgoAPI.Models;

/// <summary>
/// Represents the request body for evaluating poker hands.
/// </summary>
public class Request
{
    /// <summary>Array of player hole cards (e.g., "Th,Qd").</summary>
    public required string[] Players { get; set; }
    
    /// <summary>Community cards (e.g., "4d,5c,Tc,Ad,2c").</summary>
    public required string CommunityCards { get; set; }
}