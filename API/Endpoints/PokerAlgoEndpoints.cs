using Microsoft.AspNetCore.Mvc;
using PokerAlgoAPI.Models;
using PokerAlgoAPI.Services;

namespace PokerAlgoAPI.Endpoints;

public static class PokerAlgoEndPoints
{
    public static void MapEndPoints(WebApplication app)
    {
        app.MapPost("/winners", GetWinners).WithTags("PokerAlgo");
        app.MapPost("/hands", GetHands).WithTags("PokerAlgo");
    }

    /// <summary>
    /// Gets the winner or winners from provided players and community cards.
    /// </summary>
    private static IResult GetWinners([FromBody] Request request, IPokerAlgoService service)
    {
        try
        {
            return Results.Ok(service.GetWinners(request));
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.GetType().Name + " - Message: " + ex.Message);
        }
    }

    /// <summary>
    /// Gets the winning hand of each player as well as individual chances of winning and tying from provided players and community cards.
    /// </summary>
    private static IResult GetHands(Request request, IPokerAlgoService service)
    {
        try
        {
            return Results.Ok(service.GetHands(request));
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.GetType().Name + " - Message: " + ex.Message);
        }
    }
}