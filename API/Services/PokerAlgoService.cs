using PokerAlgoAPI.Models;
using PokerAlgo;

namespace PokerAlgoAPI.Services;

public interface IPokerAlgoService
{
    public List<Winner> GetWinners(WinnersRequest request);
}

public class PokerAlgoServices : IPokerAlgoService
{
    public List<Winner> GetWinners(WinnersRequest request)
    {
        // TODO: Parse request
        List<Player> players = CardParser.ParsePlayers(request.Players);
        List<Card> community = CardParser.ParseCommunity(request.CommunityCards);

        // TODO: Algo.GetWinners
        List<Player> winners = Algo.GetWinners(players, community);

        // TODO: Map result to Winner and return
        List<Winner> result = new();
        foreach (Player winner in winners)
        {
            result.Add(new Winner
            {
                HoleCards = CardParser.CardToString(winner.HoleCards.First) + "," + CardParser.CardToString(winner.HoleCards.Second),
                HandType = winner.WinningHand.Type.ToString(),
                WinningHand = CardParser.CardListToString(winner.WinningHand.Cards),
                PrettyName = Helpers.GetPrettyHandName(winner.WinningHand),
            });
        }

        return result;
    }
}