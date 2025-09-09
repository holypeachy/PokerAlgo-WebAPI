using PokerAlgoAPI.Exceptions;
using PokerAlgoAPI.Models;
using PokerAlgoAPI.Services;

namespace Testing;

public class PokerAlgoServiceTests
{
    [Fact]
    public void GetWinners_NoError()
    {
        IPokerAlgoService service = new PokerAlgoServices();

        Request request = new Request() { Players = ["Th,Ts", "6h,7h"], CommunityCards = "4c,2c,Ah,Qd,Ks" };
        service.GetWinners(request);
    }

    [Fact]
    public void GetWinners_Throws()
    {
        IPokerAlgoService service = new PokerAlgoServices();

        Request request = new Request() { Players = [], CommunityCards = "" };
        Assert.Throws<IncorrectFormatException>(() => service.GetWinners(request));

        request = new Request() { Players = ["Th,Ts", "6h,7h"], CommunityCards = "4c,2c,Ah,Qd" };
        Assert.Throws<BadNumberOfCommunityCardsException>(() => service.GetWinners(request));

        request = new Request() { Players = ["Th,Ts", "6h,7h"], CommunityCards = "4c,2c,Ah,Qd,Ks,Kd" };
        Assert.Throws<BadNumberOfCommunityCardsException>(() => service.GetWinners(request));
    }

    
    [Fact]
    public void GetHands_NoError()
    {
        IPokerAlgoService service = new PokerAlgoServices();

        Request request = new Request() { Players = ["Th,Ts", "6h,7h"], CommunityCards = "4c,2c,Ah,Qd" };
        service.GetHands(request);

        request = new Request() { Players = ["Th,Ts", "6h,7h"], CommunityCards = "" };
        service.GetHands(request);

        request = new Request() { Players = ["Th,Ts", "6h,7h"], CommunityCards = "4c,2c,Ah" };
        service.GetHands(request);
    }

    [Fact]
    public void GetHands_Throws()
    {
        IPokerAlgoService service = new PokerAlgoServices();

        Request request = new Request() { Players = [], CommunityCards = "" };
        Assert.Throws<IncorrectFormatException>(() => service.GetHands(request));

        request = new Request() { Players = ["Th,Ts", "6h,7h"], CommunityCards = "4c,2c" };
        Assert.Throws<BadNumberOfCommunityCardsException>(() => service.GetHands(request));

        request = new Request() { Players = ["Th,Ts", "6h,7h"], CommunityCards = "4c,2c,Ah,Qd,Ks,Kd" };
        Assert.Throws<BadNumberOfCommunityCardsException>(() => service.GetHands(request));

        request = new Request() { Players = ["Th,Ts"], CommunityCards = "4c,2c,Ah,Qd,Ks" };
        Assert.Throws<BadNumberOfPlayersException>(() => service.GetHands(request));
    }
}
