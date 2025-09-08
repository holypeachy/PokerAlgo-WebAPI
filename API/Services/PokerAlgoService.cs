using PokerAlgoAPI.Models;
using PokerAlgo;
using PokerAlgoAPI.Exceptions;

namespace PokerAlgoAPI.Services;

public interface IPokerAlgoService
{
    public List<HandRespose> GetWinners(Request request);
    public List<HandRespose> GetHands(Request request);
}

public class PokerAlgoServices : IPokerAlgoService
{
    public List<HandRespose> GetWinners(Request request)
    {
        // Parse request
        List<Player> players = Parser.ParsePlayers(request.Players);
        List<Card> community = Parser.ParseCommunity(request.CommunityCards);

        if (community.Count != 5)
        {
            throw new IncorrectFormatExceptionException("communityCard Count: " + community.Count);
        }

        // Algo.GetWinners
        List<Player> winners = Algo.GetWinners(players, community);

        // Map result to Winner and return
        List<HandRespose> result = new();
        foreach (Player winner in winners)
        {
            result.Add(new HandRespose
            {
                HoleCards = Parser.CardToString(winner.HoleCards.First) + "," + Parser.CardToString(winner.HoleCards.Second),
                HandType = winner.WinningHand.Type.ToString(),
                WinningHand = Parser.CardListToString(winner.WinningHand.Cards),
                PrettyName = Helpers.GetPrettyHandName(winner.WinningHand),
            });
        }

        return result;
    }

    public List<HandRespose> GetHands(Request request)
    {
        // Parse request
        List<Player> players = Parser.ParsePlayers(request.Players);
        List<Card> community = Parser.ParseCommunity(request.CommunityCards);

        HandEvaluator evaluator = new();
        IPreFlopDataLoader dataLoader = new FolderLoader(@"./Data/");

        List<HandRespose> result = new();

        if (community.Count < 3 && community.Count != 0) throw new IncorrectNumberOfCommunityCardsException("communityCard Count violates poker rules: " + community.Count);

        if (community.Count < 3)
        {
            foreach (Player p in players)
            {
                var chances = ChanceCalculator.GetWinningChancePreFlopLookUp(p.HoleCards, players.Count - 1, dataLoader);
                result.Add(new HandRespose
                {
                    HoleCards = Parser.CardToString(p.HoleCards.First) + "," + Parser.CardToString(p.HoleCards.Second),
                    WinChance = chances.winChance.ToString(),
                    TieChance = chances.tieChance.ToString()
                });
            }
        }
        else
        {
            foreach (Player p in players)
            {
                WinningHand hand = evaluator.GetWinningHand(p.HoleCards, community);
                var chances = ChanceCalculator.GetWinningChancePreFlopSimParallel(p.HoleCards, players.Count - 1, 100_000);
                result.Add(new HandRespose
                {
                    HoleCards = Parser.CardToString(p.HoleCards.First) + "," + Parser.CardToString(p.HoleCards.Second),
                    HandType = hand.Type.ToString(),
                    WinningHand = Parser.CardListToString(hand.Cards),
                    PrettyName = Helpers.GetPrettyHandName(hand),
                    WinChance = chances.winChance.ToString(),
                    TieChance = chances.tieChance.ToString()
                });
            }
        }

        return result;
    }

}