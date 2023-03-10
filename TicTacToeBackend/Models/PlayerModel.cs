namespace TicTacToe.Models
{
    public class PlayerModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nickname { get; set; }
        public int Wins { get; set; } = 0;
        public int Loses { get; set; } = 0;
        public int Draws { get; set; } = 0;

        public PlayerModel() { }

        public PlayerModel(string nickname)
        {
            Nickname = nickname;
        }
    }
}