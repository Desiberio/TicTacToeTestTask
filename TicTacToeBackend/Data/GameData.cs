using TicTacToe.Interfaces;
using TicTacToe.Models;

namespace TicTacToe.Data
{
    public class GameData : IGameData
    {
        private readonly ISqlDataAccess _db;

        public GameData(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<IEnumerable<GameModel>> GetGames() =>
            await _db.LoadData<GameModel, dynamic>("SELECT * FROM [GamesHistory]", new { });

        public async Task<IEnumerable<GameModel>> GetGames(Guid playerID) =>
            await _db.LoadData<GameModel, dynamic>($"SELECT * FROM [GamesHistory] WHERE playerID = {playerID}", new { });

        public async Task<IEnumerable<GameModel>> GetGame(Guid gameId) =>
            await _db.LoadData<GameModel, dynamic>($"SELECT * FROM [GamesHistory] WHERE ID = @GameId", new { GameId = gameId });

        public async Task SaveGame(GameModel game)
        {
            await _db.SaveData("INSERT INTO [GamesHistory] (Id, firstPlayerId, secondPlayerId, gameResult) VALUES (@Id, @FirstPlayerId, @SecondPlayerId, @GameResult)",
                                            new { Id = game.GameId, FirstPlayerId = game.FirstPlayer.Id, SecondPlayerId = game.SecondPlayer.Id, game.GameResult });
        }

        public async Task InsertPlayer(PlayerModel player)
        {
            await _db.SaveData("INSERT INTO [Players] (Id, Nickname) VALUES (@Id, @Nickname)", new { player.Id, player.Nickname });
        }

        public async Task<PlayerModel?> GetPlayer(string nickname)
        {
            var results = await _db.LoadData<PlayerModel, dynamic>("SELECT * FROM [Players] WHERE nickname = @Nickname", new { Nickname = nickname });
            return results.FirstOrDefault();
        }

        public async Task<PlayerModel?> GetPlayer(Guid playerId)
        {
            var results = await _db.LoadData<PlayerModel, dynamic>("SELECT * FROM [Players] WHERE ID = @PlayerId", new { PlayerId = playerId });
            return results.FirstOrDefault();
        }

        public async Task DeletePlayer(Guid playerId) =>
            await _db.SaveData("DELETE FROM [Players] WHERE ID = @PlayerId", new { PlayerId = playerId });

        public async Task UpdatePlayerScore(PlayerModel player) =>
            await _db.SaveData("UPDATE [Players] SET Wins = @Wins, Loses = @Loses, Draws = @Draws WHERE Id = @Id", new { player.Wins, player.Loses, player.Draws, player.Id });
    }
}
