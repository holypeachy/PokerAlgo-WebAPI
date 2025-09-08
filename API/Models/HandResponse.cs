namespace PokerAlgoAPI.Models;

public record HandRespose
{
    public required string HoleCards { get; set; }
    public string HandType { get; set; }
    public string WinningHand { get; set; }
    public string PrettyName { get; set; }
    public string WinChance { get; set; }
    public string TieChance { get; set; }
}