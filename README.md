# API Description
This API allows to create players and games for TicTacToe.  
Needs connection string from IConfiguration in order to connect to database.

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

<details>
  <summary>API detailed description by OpenAPI</summary>
  
  ```json
  {
  "openapi": "3.0.1",
  "info": {
    "title": "TicTacToeAPI",
    "version": "1.0"
  },
  "paths": {
    "/api/v1/game/start": {
      "get": {
        "tags": [
          "Api"
        ],
        "parameters": [
          {
            "name": "firstPlayerId",
            "in": "query",
            "required": true,
            "schema": {
              "type": "string"
            }
          },
          {
            "name": "secondPlayerId",
            "in": "query",
            "required": true,
            "schema": {
              "type": "string"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/api/v1/game/{gameID}/{cell}": {
      "post": {
        "tags": [
          "Api"
        ],
        "parameters": [
          {
            "name": "gameID",
            "in": "path",
            "required": true,
            "schema": {
              "type": "string"
            }
          },
          {
            "name": "playerId",
            "in": "query",
            "required": true,
            "schema": {
              "type": "string"
            }
          },
          {
            "name": "cell",
            "in": "path",
            "required": true,
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/api/v1/player": {
      "post": {
        "tags": [
          "Api"
        ],
        "parameters": [
          {
            "name": "nickname",
            "in": "query",
            "required": true,
            "schema": {
              "type": "string"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      },
      "delete": {
        "tags": [
          "Api"
        ],
        "parameters": [
          {
            "name": "playerId",
            "in": "query",
            "required": true,
            "schema": {
              "type": "string"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/api/v1/player/by-name/{nickname}": {
      "get": {
        "tags": [
          "Api"
        ],
        "parameters": [
          {
            "name": "nickname",
            "in": "path",
            "required": true,
            "schema": {
              "type": "string"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    },
    "/api/v1/player/by-id/{playerId}": {
      "get": {
        "tags": [
          "Api"
        ],
        "parameters": [
          {
            "name": "playerId",
            "in": "path",
            "required": true,
            "schema": {
              "type": "string"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          }
        }
      }
    }
}
```
</details>
