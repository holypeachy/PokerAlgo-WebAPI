using Microsoft.AspNetCore.Mvc;
using PokerAlgoAPI.Models;
using PokerAlgoAPI.Services;

namespace PokerAlgoAPI.Endpoints;

public static class PokerAlgoEndPoints
{
    public static void MapEndPoints(WebApplication app)
    {
        app.MapPost("/winners", ([FromBody] Request request, IPokerAlgoService service) => GetWinners(request, service));
        app.MapPost("/hands", ([FromBody] Request request, IPokerAlgoService service) => GetHands(request, service));
    }

    private static IResult GetWinners(Request request, IPokerAlgoService service)
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