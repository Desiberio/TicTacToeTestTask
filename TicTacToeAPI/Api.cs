using TicTacToe.Interfaces;
using TicTacToe.Models;

namespace TicTacToeAPI;

public static class Api
{
    public static void ConfigureApi(this WebApplication app)
    {
        app.MapGet("api/v1/game/start", StartGame);
        app.MapPost("api/v1/game/{gameID:guid}/{cell:int}", MakeTurn);
        app.MapPost("api/v1/player", CreatePlayer);
        app.MapDelete("api/v1/player", DeletePlayer);
        app.MapGet("api/v1/player/by-name/{nickname:alpha}", GetPlayer);
        app.MapGet("api/v1/player/by-id/{playerId:guid}", GetPlayerById);
    }

    private static async Task<IResult> StartGame(IGamesManager gamesManager, IGameData gameData, Guid firstPlayerId, Guid secondPlayerId)
    {
        Guid? gameId = await gamesManager.StartGame(gameData, firstPlayerId, secondPlayerId);
        if (gameId is null) return Results.BadRequest("Can't start game, one of players with given IDs wasn't found.");
        return Results.Ok(gameId);
    }

    private static IResult MakeTurn(IGamesManager gamesManager, IValidator validator, Guid gameID, Guid playerId, int cell)
    {
        try
        {
            var validation = validator.ValidateTurn(gamesManager, playerId, gameID, cell);
            if (validation.IsValid)
            {
                return Results.Ok(gamesManager.MakeTurn(gameID, cell).Result);
            }
            return Results.ValidationProblem(validation.Errors);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> CreatePlayer(IGameData gameData, IValidator validator, string nickname)
    {
        try
        {
            var validation = validator.ValidateNickname(nickname);
            if (validation.IsValid)
            {
                PlayerModel newPlayer = new PlayerModel(nickname);
                await gameData.InsertPlayer(newPlayer);
                return Results.Ok();
            }
            return Results.ValidationProblem(validation.Errors);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> DeletePlayer(IGameData gameData, Guid playerId)
    {
        try
        {
            await gameData.DeletePlayer(playerId);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> GetPlayer(IGameData gameData, IValidator validator, string nickname)
    {
        try
        {
            var validation = validator.ValidateNickname(nickname);
            if (validation.IsValid)
            {
                PlayerModel? player = await gameData.GetPlayer(nickname);
                if(player is null) return Results.NotFound($"Can't find player with given nickname ({nickname})");
                return Results.Ok(player);
            }
            return Results.ValidationProblem(validation.Errors);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> GetPlayerById(IGameData gameData, IValidator validator, Guid playerId)
    {
        PlayerModel? player = await gameData.GetPlayer(playerId);
        if (player is null) return Results.NotFound($"Can't find player with given Id ({playerId})");
        return Results.Ok(player);
    }
}
