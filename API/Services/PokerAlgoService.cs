using PokerAlgoAPI.Models;
using PokerAlgo;
using PokerAlgoAPI.Exceptions;

namespace PokerAlgoAPI.Services;

public interface IPokerAlgoService
{
    public List<HandResponse> GetWinners(Request request);
    public List<HandResponse> GetHands(Request request);
}

public class PokerAlgoServices : IPokerAlgoService
{
    private readonly string _dataPath = @"C:/Users/Frank/Code/PokerAlgoAPI/API/Data/Preflop/";

    public List<HandResponse> GetWinners(Request request)
    {
        // Parse request
        List<Player> players = Parser.ParsePlayers(request.Players);
        List<Card> community = Parser.ParseCommunity(request.CommunityCards);

        if (community.Count != 5)
        {
            throw new BadNumberOfCommunityCardsException("Community Card Count: " + community.Count + ". Please make sure to provide all 5 cards to determine winner.");
        }

        // Algo.GetWinners
        List<Player> winners = Algo.GetWinners(players, community);

        // Map result to Winner and return
        List<HandResponse> result = new();
        foreach (Player winner in winners)
        {
            result.Add(new HandResponse
            {
                HoleCards = Parser.CardToString(winner.HoleCards.First) + "," + Parser.CardToString(winner.HoleCards.Second),
                HandType = winner.WinningHand.Type.ToString(),
                WinningHand = Parser.CardListToString(winner.WinningHand.Cards),
                PrettyName = Helpers.GetPrettyHandName(winner.WinningHand),
            });
        }

        return result;
    }

    public List<HandResponse> GetHands(Request request)
    {
        // Parse request
        List<Player> players = Parser.ParsePlayers(request.Players);
        List<Card> community = Parser.ParseCommunity(request.CommunityCards);

        HandEvaluator evaluator = new();
        IPreFlopDataLoader dataLoader = new FolderLoader(_dataPath);

        List<HandResponse> result = new();

        if (community.Count < 3 && community.Count != 0) throw new BadNumberOfCommunityCardsException("Community Card Count violates poker rules: " + community.Count);

        // Pre-flop
        if (community.Count < 3)
        {
            foreach (Player p in players)
            {
                var (winChance, tieChance) = ChanceCalculator.GetWinningChancePreFlopLookUp(p.HoleCards, players.Count - 1, dataLoader);
                result.Add(new HandResponse
                {
                    HoleCards = Parser.CardToString(p.HoleCards.First) + "," + Parser.CardToString(p.HoleCards.Second),
                    WinChance = winChance.ToString(),
                    TieChance = tieChance.ToString()
                });
            }
        }
        // Post-Flop
        else
        {
            foreach (Player p in players)
            {
                WinningHand hand = evaluator.GetWinningHand(p.HoleCards, community);
                var (winChance, tieChance) = ChanceCalculator.GetWinningChancePreFlopSimParallel(p.HoleCards, players.Count - 1, 100_000);
                result.Add(new HandResponse
                {
                    HoleCards = Parser.CardToString(p.HoleCards.First) + "," + Parser.CardToString(p.HoleCards.Second),
                    HandType = hand.Type.ToString(),
                    WinningHand = Parser.CardListToString(hand.Cards),
                    PrettyName = Helpers.GetPrettyHandName(hand),
                    WinChance = winChance.ToString(),
                    TieChance = tieChance.ToString()
                });
            }
        }

        return result;
    }

}