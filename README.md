# API Description


<details>
<summary>GET /api/v1/game/start</summary>

Starts a new TicTacToe game.  

**Return value: GUID** of started game  

</details>

<details>
<summary>GET api/v1/player/by-name/{nickname}</summary>

Get a player by player nickname.  

**Return value: PlayerModel**  
| Name     | Data Type | Description               |
|----------|-----------|---------------------------|
| Id       | GUID      | GUID of player.           |
| Nickname | string    | Nickname of player.       |
| Wins     | int       | Number of player's wins.  |
| Loses    | int       | Number of player's loses. |
| Draws    | int       | Number of player's draws. |
</details>

<details>
<summary>GET api/v1/player/by-id/{player-id}</summary>

Get a player by player id.  

**Return value: PlayerModel**  
| Name     | Data Type | Description               |
|----------|-----------|---------------------------|
| Id       | GUID      | GUID of player.           |
| Nickname | string    | Nickname of player.       |
| Wins     | int       | Number of player's wins.  |
| Loses    | int       | Number of player's loses. |
| Draws    | int       | Number of player's draws. |
</details>

<details>
<summary>POST /api/v1/player</summary>
Creates new player with given nickname.
</details>

<details>
<summary>POST /api/v1/game/{gameID}/{cell}</summary>

***This method needs an authentication but I don't know how to implement it yet.***

Places mark on given cell in specified game by player. Needs parameter **playerID** to execute. 

**Return value: int**, can have next values: 0 - draw, 1 - first player win, 2 - second player win, -1 - game continues.
</details>

<details>
<summary>DELETE /api/v1/player </summary>

Deletes player from database.
</details>
