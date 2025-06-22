using Microsoft.AspNetCore.Mvc;
using PokerAlgoAPI.Models;
using PokerAlgoAPI.Services;

namespace PokerAlgoAPI.Endpoints;

public static class PokerAlgoEndPoints
{
    public static void MapEndPoints(WebApplication app)
    {
        app.MapPost("/winners", ([FromBody] WinnersRequest request, IPokerAlgoService service) => GetWinners(request, service));
        app.MapPost("/hand", () => GetHand());
        app.MapPost("/chances", () => GetChances());
    }

    private static IResult GetWinners(WinnersRequest request, IPokerAlgoService service)
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

    private static IResult GetHand()
    {
        return Results.Ok("Endpoint for getting the winning hand of a player.");
    }
    
    private static IResult GetChances()
    {
        return Results.Ok("Endpoint for getting the chances of a player winnning.");
    }
}