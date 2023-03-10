using TicTacToe.Models;

namespace TicTacToe.Interfaces
{
    public interface IGameData
    {
        Task<IEnumerable<GameModel>> GetGame(Guid gameId);
        Task<IEnumerable<GameModel>> GetGames();
        Task<IEnumerable<GameModel>> GetGames(Guid playerID);
        Task<PlayerModel?> GetPlayer(string nickname);
        Task<PlayerModel?> GetPlayer(Guid playerId);
        Task DeletePlayer(Guid playerId);
        Task InsertPlayer(PlayerModel player);
        Task UpdatePlayerScore(PlayerModel player);
        Task SaveGame(GameModel game);
    }
}