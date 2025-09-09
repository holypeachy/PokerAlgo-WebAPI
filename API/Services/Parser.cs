using PokerAlgo;
using PokerAlgoAPI.Exceptions;

namespace PokerAlgoAPI.Services;

public static class Parser
{
    private static readonly Dictionary<char, int> _rank = new()
    {
        {'A', 14},
        {'K', 13},
        {'Q', 12},
        {'J', 11},
        {'T', 10},
        {'9', 9},
        {'8', 8},
        {'7', 7},
        {'6', 6},
        {'5', 5},
        {'4', 4},
        {'3', 3},
        {'2', 2},
    };

    private static readonly Dictionary<char, CardSuit> _suit = new()
    {
        {'s', CardSuit.Spades},
        {'c', CardSuit.Clubs},
        {'d', CardSuit.Diamonds},
        {'h', CardSuit.Hearts},
    };

    private static readonly Dictionary<int, char> _cRank= new()
    {
        {14, 'A'},
        {13, 'K'},
        {12, 'Q'},
        {11, 'J'},
        {10, 'T'},
        {9, '9'},
        {8, '8'},
        {7, '7'},
        {6, '6'},
        {5, '5'},
        {4, '4'},
        {3, '3'},
        {2, '2'},
        {1, 'A'},
    };

    private static readonly Dictionary<CardSuit, char> _cSuit= new()
    {
        { CardSuit.Clubs, 'c'},
        { CardSuit.Diamonds, 'd'},
        { CardSuit.Hearts, 'h'},
        { CardSuit.Spades, 's'},
    };

    public static List<Player> ParsePlayers(string[] playerStrings)
    {
        if (playerStrings.Length < 1) throw new IncorrectFormatException("Player string is empty");

        List<Player> players = new();
        int count = 1;

        foreach (string s in playerStrings)
        {
            string[] split = s.Split(',');

            if (split.Length != 2)
            {
                throw new IncorrectFormatException($"Bad string: \"{s}\"");
            }

            Pair pair = new(ParseCard(split[0]), ParseCard(split[1]));
            players.Add(new Player($"Player {count++}", pair.First, pair.Second));
        }

        if (players.Count < 2)
        {
            throw new BadNumberOfPlayersException("Count: " + players.Count);
        }
        else if (players.Count > 5)
        {
            throw new BadNumberOfPlayersException("Due to current limitations, there should be 2-5 players and no more.");
        }

        return players;
    }

    public static List<Card> ParseCommunity(string communityCards)
    {
        List<Card> cards = new();
        string[] split = communityCards.Split(',');

        if (communityCards.Length != 0)
        {
            foreach (string s in split)
            {
                cards.Add(ParseCard(s));
            }
        }

        if (cards.Count > 5) throw new BadNumberOfCommunityCardsException("Community Cards Count violates poker rules: " + cards.Count);

        return cards;
    }

    public static Card ParseCard(string s)
    {
        int rank;
        CardSuit suit;

        if (s.Length != 2)
        {
            throw new IncorrectFormatException($"Bad string: \"{s}\"");
        }
        try
        {
            rank = _rank[s[0]];
        }
        catch
        {
            throw new IncorrectFormatException($"Bad string: \"{s}\"");
        }


        try
        {
            suit = _suit[s[1]];
        }
        catch
        {
            throw new IncorrectFormatException($"Bad string: \"{s}\"");
        }

        return new Card(rank, suit, false);
    }

    public static string CardListToString(List<Card> cards)
    {
        return string.Join(',', cards.Select(CardToString));
    }

    public static string CardToString(Card card)
    {
        return $"{_cRank[card.Rank]}{_cSuit[card.Suit]}";
    }
}