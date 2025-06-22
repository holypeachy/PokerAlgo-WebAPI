namespace PokerAlgoAPI.Models;

public class WinnersRequest
{
    public string[] Players { get; set; }
    public string CommunityCards { get; set; }
}