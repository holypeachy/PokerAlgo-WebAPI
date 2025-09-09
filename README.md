# 🍑 ASP.NET Minimal Web API - A simple Web API for my [PokerAlgo](https://github.com/holypeachy/PokerAlgo).
#### The first of a series of projects to practice and showcase my skills with ASP.NET.
- 2 endpoints:
  - /winners provides the winner(s) given 2-5 players and all 5 community cards.
  - /hands provides the winning hand and individual chances of winning (independent from other players) per player, given 2-5 players and none or any community cards.
- Using Swashbuckle/Swagger for automated documentation and a simple front end for developers.
- Solid error handling and fully tested.

> Cards must be in poker notation: "Ac,6h,Td,2s"

## Output Example:

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
