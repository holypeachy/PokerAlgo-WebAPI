# PokerAlgo Web API

A small ASP.NET Core Minimal API that exposes selected [PokerAlgo](https://github.com/holypeachy/PokerAlgo) operations over HTTP.

**Built with:** C# | ASP.NET Core 9 | Minimal APIs | Swagger/Swashbuckle | xUnit

## Overview

I built this as a focused ASP.NET practice project after publishing PokerAlgo. The API parses compact poker notation, validates the request, delegates the actual hand logic to the PokerAlgo library, and returns structured JSON responses.

Swagger provides the interactive API documentation and is served as the application's default page.

## Endpoints

| Endpoint | Description |
| --- | --- |
| `POST /winners` | Determines the winner or tied winners from two to five players and a complete five-card board. |
| `POST /hands` | Returns each player's evaluated hand and independent win and tie estimates. |

Cards use standard compact poker notation, such as `Ac`, `6h`, `Td`, or `2s`. Each player must have exactly two hole cards.

```json
{
  "players": ["Th,Qd", "6c,Kd"],
  "communityCards": "4d,5c,Tc,Ad,2c"
}
```

## Getting Started

Requires the .NET 9 SDK.

```bash
git clone https://github.com/holypeachy/PokerAlgo-WebAPI.git
cd PokerAlgo-WebAPI
dotnet run --project API
```

Open [http://localhost:5234/swagger](http://localhost:5234/swagger) to use the Swagger interface.

The pre-flop lookup files are included in `API/Data/Preflop`. The current implementation contains a machine-specific path in `PokerAlgoService.cs`, which must be updated before pre-flop requests will work on another system.

Run the tests with:

```bash
dotnet test
```

## Images

### /winners
```json
{
  "players": [
    "Th,Qd",
    "6c,Kd"
  ],
  "communityCards": "4d,5c,Tc,Ad,2c"
}
```
<img width="366" height="232" alt="image" src="https://github.com/user-attachments/assets/087b5839-aeef-4761-9b97-09c88b44c499" />

### /hands
```json
{
  "players": [
    "Th,Qd",
    "7c,2d"
  ],
  "communityCards": "4d,5c,Tc"
}
```
<img width="398" height="345" alt="image" src="https://github.com/user-attachments/assets/d20bca2f-739f-4446-9462-5c6d76484852" />
