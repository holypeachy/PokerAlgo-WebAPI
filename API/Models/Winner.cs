namespace PokerAlgoAPI.Models;

public record Winner
{
    public string HoleCards { get; set; }
    public string HandType { get; set; }
    public string WinningHand { get; set; }
    public string PrettyName { get; set; }
}