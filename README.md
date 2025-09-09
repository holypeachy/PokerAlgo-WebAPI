# 🍑 ASP.NET Minimal API - A simple Web API for my [PokerAlgo](https://github.com/holypeachy/PokerAlgo).
#### The first of a series of projects to practice and showcase my skills with ASP.NET.
- 2 endpoints:
  - /winners provides the winner(s).
  - /hands provides the winning hand and individual chances of winning (independent from other players) per player.
- Using Swashbuckle/Swagger for automated documentation and a simple front end for developers.
- Solid error handling and unit tested (parser and PokerAlgo service).

> Cards must be in poker notation: "Ac,6h,Td,2s"

> Players must each have 2 cards, and up to 5 players are supported.

## 🚀 Getting Started

```bash
git clone git@github.com:holypeachy/PokerAlgo-WebAPI.git
cd PokerAlgo-WebAPI
dotnet run
```
Then open http://localhost:5000/ which will take you to swagger automatically. Alternatively you could go to http://localhost:5000/swagger.

### Schema:
```json
{
  "players": [ "Th,Qd", "6c,Kd" ],
  "communityCards": "4d,5c,Tc,Ad,2c"
}
```

## 🧰 Tech Stack

- ASP.NET 9 Minimal API
- Swagger / Swashbuckle
- Custom Parsing & Simulation Logic from [PokerAlgo](https://github.com/holypeachy/PokerAlgo)

## 📤 Output Example:

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
