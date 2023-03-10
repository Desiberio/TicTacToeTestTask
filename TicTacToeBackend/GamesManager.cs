using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Interfaces;
using TicTacToe.Models;

namespace TicTacToe
{
    public class GamesManager : IGamesManager
    {
        private readonly List<GameModel> _currentGames;
        private readonly IGameData _gameData;

        public GamesManager(IGameData gameData)
        {
            _currentGames = new List<GameModel>();
            _gameData = gameData;
        }

        public async Task<Guid?> StartGame(IGameData gameData, Guid firstPlayerId, Guid secondPlayerId)
        {
            Guid gameID = Guid.NewGuid();
            PlayerModel? firstPlayer = await gameData.GetPlayer(firstPlayerId);
            PlayerModel? secondPlayer = await gameData.GetPlayer(secondPlayerId);
            if (firstPlayer is null || secondPlayer is null) return null;
            _currentGames.Add(new GameModel(gameID, firstPlayer, secondPlayer));
            return gameID;
        }

        public async Task<int> MakeTurn(Guid gameID, int cell)
        {
            GameModel game = _currentGames.Find(x => x.GameId == gameID) ?? throw new Exception($"Game with given ID ({gameID}) wasn't not found but passed validation.");

            if (game.IsFirstPlayerTurn)
            {
                game.FirstPlayerMarks[cell] = 0;
                if (WinCheck(game.FirstPlayerMarks))
                {
                    await EndGame(game, 1);
                    return 1;
                }
            }
            else
            {
                game.SecondPlayerMarks[cell] = 0;
                if (WinCheck(game.SecondPlayerMarks))
                {
                    await EndGame(game, 2);
                    return 2;
                }
            }

            game.MarksCount++;
            game.IsFirstPlayerTurn = !game.IsFirstPlayerTurn;

            if (game.MarksCount == 9)
            {
                await EndGame(game, 0);
                return 0;
            }

            return -1;
        }
        /// <summary>
        /// Checks if player with given list of marks has won the game.
        /// </summary>
        /// <param name="playerMarks">A list of marks on board. Contains 9 values from 1 to 9, where zeros indicates placed mark.</param>
        /// <returns>A boolean value that determines if the player has won.</returns>
        private bool WinCheck(List<int> playerMarks)
        {
            if (playerMarks.Count != 9) throw new Exception("Number of one of players marks list was other than 9 (3x3 grid).");
            //rows
            if (playerMarks.Take(new Range(0, 3)).Sum() == 0 ||
            playerMarks.Take(new Range(3, 6)).Sum() == 0 ||
            playerMarks.Take(new Range(6, 9)).Sum() == 0) return true;

            //columns
            if (playerMarks.Where(x => x % 3 == 1).Sum() == 0 ||
                playerMarks.Where(x => x % 3 == 2).Sum() == 0 ||
                playerMarks.Where(x => x % 3 == 0).Sum() == 0) return true;

            //top left to bottom right diagonal
            int sum = 0;
            for(int i = 0; i < playerMarks.Count; i += 4) sum += playerMarks[i];
            if (sum == 0) return true;

            //top right to bottom left diagonal
            sum = 0;
            for (int i = 2; i < playerMarks.Count - 1; i += 2) sum += playerMarks[i];
            if (sum == 0) return true;

            return false;
        }

        public async Task EndGame(GameModel game, int gameResult)
        {
            if (game.FirstPlayer is not null && game.SecondPlayer is not null)
            {
                switch (gameResult)
                {
                    case 0:
                        game.FirstPlayer.Draws++;
                        game.SecondPlayer.Draws++;
                        break;
                    case 1:
                        game.FirstPlayer.Wins++;
                        game.SecondPlayer.Loses++;
                        break;
                    case 2:
                        game.FirstPlayer.Loses++;
                        game.SecondPlayer.Wins++;
                        break;
                }
            }

            game.GameResult = gameResult;

            await _gameData.SaveGame(game);
            await _gameData.UpdatePlayerScore(game.FirstPlayer);
            await _gameData.UpdatePlayerScore(game.SecondPlayer);

            _currentGames.Remove(game);
        }

        public List<GameModel> GetCurrentGames()
        {
            return _currentGames;
        }

    }
}
