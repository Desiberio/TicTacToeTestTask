using TicTacToe.Interfaces;
using TicTacToe.Models;

namespace TicTacToe.Validation;

public class Validator : IValidator
{
    public ValidationResult ValidateNickname(string playerNickname)
    {
        ValidationResult result = new()
        {
            IsValid = true
        };

        if (string.IsNullOrWhiteSpace(playerNickname)) result.Errors.Add(nameof(playerNickname), new string[] { "Player nickname cannot be empty." });
        if (playerNickname.Length < 3 || playerNickname.Length > 25) result.Errors.Add(nameof(playerNickname), new string[] { "Player nickname should be at least 3 and maximum 25 characters long." });

        if (result.Errors.Count != 0) result.IsValid = false;

        return result;
    }

    public ValidationResult ValidateTurn(IGamesManager gamesManager, Guid playerId, Guid gameID, int cell)
    {
        ValidationResult result = new()
        {
            IsValid = true
        };

        if (cell < 0 || cell > 8) result.Errors.Add(nameof(cell), new string[] { "Cell value was out of range. Acceptable values are in range 0-8." });

        GameModel? game = gamesManager.GetCurrentGames().Find(x => x.GameId == gameID);
        if (game is not null)
        {
            if (game.FirstPlayerMarks[cell] == 0 || game.SecondPlayerMarks[cell] == 0) result.Errors.Add(nameof(cell), new string[] { $"Wrong turn. Cell {cell} already has mark." });
            if (game.FirstPlayer.Id != playerId && game.SecondPlayer.Id != playerId) result.Errors.Add(nameof(playerId), new string[] { $"This player are not part of the created game." });
            if (game.FirstPlayer.Id == playerId && game.IsFirstPlayerTurn == false) result.Errors.Add(nameof(playerId), new string[] { $"Second player didn't make turn yet." });
            if (game.SecondPlayer.Id == playerId && game.IsFirstPlayerTurn == true) result.Errors.Add(nameof(playerId), new string[] { $"First player didn't make turn yet." });
        }
        else result.Errors.Add(nameof(gameID), new string[] { "Game with given ID not found." });


        if (result.Errors.Count != 0) result.IsValid = false;

        return result;
    }
}

public class ValidationResult
{
    public bool IsValid { get; set; }
    public Dictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
}
