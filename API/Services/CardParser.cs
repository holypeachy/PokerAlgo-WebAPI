using PokerAlgo;
using PokerAlgoAPI.Exceptions;

namespace PokerAlgoAPI.Services;

public static class CardParser
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
        if (playerStrings.Length < 1) throw new Exception("playerStrings empty");

        List<Player> players = new();
        int count = 1;

        foreach (string s in playerStrings)
        {
            string[] split = s.Split(',');

            if (split.Length != 2)
            {
                throw new IncorrectFormatExceptionException($"Bad string: \"{s}\"");
            }

            Pair pair = new(ParseCard(split[0]), ParseCard(split[1]));
            players.Add(new Player($"Player {count++}", pair.First, pair.Second));
        }

        if (players.Count < 2)
        {
            throw new NotEnoughPlayersExceptionException("Count: " + players.Count);
        }

        return players;
    }

    public static List<Card> ParseCommunity(string communityCards)
    {
        if (string.IsNullOrEmpty(communityCards)) throw new Exception("communityCards empty");

        List<Card> cards = new();
        string[] split = communityCards.Split(',');
        foreach (string s in split)
        {
            cards.Add(ParseCard(s));
        }

        if (cards.Count != 5)
        {
            throw new IncorrectFormatExceptionException("communityCard Count: " + cards.Count);
        }

        return cards;
    }

    public static Card ParseCard(string s)
    {
        int rank;
        CardSuit suit;

        if (s.Length != 2)
        {
            throw new IncorrectFormatExceptionException($"Bad string: \"{s}\"");
        }
        try
        {
            rank = _rank[s[0]];
        }
        catch
        {
            throw new IncorrectFormatExceptionException($"Bad string: \"{s}\"");
        }


        try
        {
            suit = _suit[s[1]];
        }
        catch
        {
            throw new IncorrectFormatExceptionException($"Bad string: \"{s}\"");
        }

        return new Card(rank, suit, false);
    }

    public static string CardListToString(List<Card> cards)
    {
        string s = string.Empty;
        foreach (Card c in cards)
        {
            s += _cRank[c.Rank] + "" + _cSuit[c.Suit] + ',';
        }
        s = s.Remove(s.Length - 1);
        return s;
    }

    public static string CardToString(Card card)
    {
        return string.Empty + _cRank[card.Rank] + _cSuit[card.Suit];
    }
}