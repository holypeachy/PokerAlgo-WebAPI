using System.Diagnostics;
using FluentAssertions;
using PokerAlgo;
using PokerAlgoAPI.Exceptions;
using PokerAlgoAPI.Services;

namespace Testing;

public class ParserTests
{
    [Fact]
    public void ParseCard_No_errors()
    {
        string s = "4c";
        Card result = Parser.ParseCard(s);

        result.Should().BeEquivalentTo(new Card(4, CardSuit.Clubs, false));
    }

    [Fact]
    public void ParseCard_Bad_formatting()
    {
        string s = "4cs";
        Assert.Throws<IncorrectFormatException>(() => Parser.ParseCard(s));

        s = "4";
        Assert.Throws<IncorrectFormatException>(() => Parser.ParseCard(s));

        s = "4o";
        Assert.Throws<IncorrectFormatException>(() => Parser.ParseCard(s));

        s = "1c";
        Assert.Throws<IncorrectFormatException>(() => Parser.ParseCard(s));
    }

    [Fact]
    public void ParsePlayers_No_errors()
    {
        string[] s = ["4c,Td", "6s,Kh"];
        List<Player> result = Parser.ParsePlayers(s);
        List<Player> expected = new()
        {
            new Player("Player 1", new Card(4, CardSuit.Clubs, true), new Card(10, CardSuit.Diamonds, true)),
            new Player("Player 2", new Card(6, CardSuit.Spades, true), new Card(13, CardSuit.Hearts, true)),
        };
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void ParsePlayers_Bad_formatting()
    {
        string[] s = ["4c, Td", "6s,Kh"];

        Assert.Throws<IncorrectFormatException>(() => Parser.ParsePlayers(s));

        s = ["4c,Td ", "6s,Kh"];
        Assert.Throws<IncorrectFormatException>(() => Parser.ParsePlayers(s));

        s = ["4c,(d", "6s,Kh"];
        Assert.Throws<IncorrectFormatException>(() => Parser.ParsePlayers(s));
    }

    [Fact]
    public void ParsePlayers_Bad_Number_Of_Players()
    {
        string[] s = ["4c,Td", "6c,Kh", "2c,4d", "7h,Qd", "5s,6d", "Ac,As"];

        Assert.Throws<BadNumberOfPlayersException>(() => Parser.ParsePlayers(s));

        s = ["4c,Td"];
        Assert.Throws<BadNumberOfPlayersException>(() => Parser.ParsePlayers(s));
    }

    [Fact]
    public void ParseCommunity_No_errors()
    {
        string s = "4c,8h,Tc,9s,Ah";
        List<Card> result = Parser.ParseCommunity(s);
        List<Card> expected = new()
        {
            new Card(4, CardSuit.Clubs, false),
            new Card(8, CardSuit.Hearts, false),
            new Card(10, CardSuit.Clubs, false),
            new Card(9, CardSuit.Spades, false),
            new Card(14, CardSuit.Hearts, false),
        };

        result.Should().BeEquivalentTo(expected);

        s = "4c,8h,Tc";
        result = Parser.ParseCommunity(s);
        expected = new()
        {
            new Card(4, CardSuit.Clubs, false),
            new Card(8, CardSuit.Hearts, false),
            new Card(10, CardSuit.Clubs, false),
        };

        result.Should().BeEquivalentTo(expected);

        s = "";
        result = Parser.ParseCommunity(s);
        expected = new();

        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void ParseCommunity_Bad_Number_Of_Community_Cards()
    {
        string s = "4c,8h,Tc,9s,Ah,Ac";
        Assert.Throws<BadNumberOfCommunityCardsException>(() => Parser.ParseCommunity(s));
    }
}
