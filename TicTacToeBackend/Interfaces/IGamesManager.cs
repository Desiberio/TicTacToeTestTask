using TicTacToe.Models;

namespace TicTacToe.Interfaces
{
    public interface IGamesManager
    {
        /// <summary>
        /// Places mark on specified cell. Mark type ("X" or "O") alternates each turn, starting with "X".
        /// </summary>
        /// <param name="gameID">ID of the game.</param>
        /// <param name="turn">Cell where mark will be placed.</param>
        /// <returns>Integer that determines game state (0 - draw, 1 - win of the first player, 2 - win of the second player, -1 - game continues).</returns>
        Task<int> MakeTurn(Guid gameID, int turn);
        /// <summary>
        /// Starts a new TicTacToe game.
        /// </summary>
        /// <param name="gameData">Game data.</param>
        /// <param name="firstPlayerId">First player GUID.</param>
        /// <param name="secondPlayerId">Second player GUID.</param>
        /// <returns>GUID of created game. Will return null if there is an error.</returns>
        Task<Guid?> StartGame(IGameData gameData, Guid firstPlayerId, Guid secondPlayerId);
        /// <summary>
        /// Ends game, saving all data and removing provided game from all current games list.
        /// </summary>
        /// <param name="game">A game instance with players info.</param>
        /// <param name="gameResult">Result of game (0 - draw, 1 - win of the first player, 2 - win of the second player).</param>
        Task EndGame(GameModel game, int gameResult);
        /// <summary>
        /// Gets list of all current games.
        /// </summary>
        /// <returns>List of all current games.</returns>
        List<GameModel> GetCurrentGames();
    }
}