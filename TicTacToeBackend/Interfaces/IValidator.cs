using TicTacToe.Validation;

namespace TicTacToe.Interfaces
{
    public interface IValidator
    {
        /// <summary>
        /// Validates turn.
        /// </summary>
        /// <param name="gamesManager">Games manager which provide list of all current games.</param>
        /// <param name="playerId">Player ID.</param>
        /// <param name="gameID">Game ID.</param>
        /// <param name="cell">Cell, where mark should be placed.</param>
        /// <returns>Validation result with dictionary of errors.</returns>
        ValidationResult ValidateTurn(IGamesManager gamesManager, Guid playerId, Guid gameID, int cell);
        ValidationResult ValidateNickname(string playerNickname);
    }
}