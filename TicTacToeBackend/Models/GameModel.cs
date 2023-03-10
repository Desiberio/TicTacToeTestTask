namespace TicTacToe.Models;

public class GameModel
{
    public PlayerModel? FirstPlayer { get; set; } = null;
    public PlayerModel? SecondPlayer { get; set; } = null;
    public List<int> FirstPlayerMarks { get; set; } = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    public List<int> SecondPlayerMarks { get; set; } = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    public Guid GameId { get; set; }
    public int? GameResult = null;
    public bool IsFirstPlayerTurn { get; set; } = true;
    public int MarksCount { get; set; } = 0;

    public GameModel(Guid gameID)
    {
        GameId = gameID;
    }

    public GameModel(Guid gameID, PlayerModel firstPlayer, PlayerModel secondPlayer)
    {
        GameId = gameID;
        FirstPlayer = firstPlayer;
        SecondPlayer = secondPlayer;
    }
}

