using TicTacToe;
using TicTacToe.Data;
using TicTacToe.Interfaces;
using TicTacToe.Validation;

namespace TicTacToeAPI
{
    public static class Dependencies
    {
        public static void ConfigureDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IGamesManager, GamesManager>();
            builder.Services.AddScoped<IValidator, Validator>();
            builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
            builder.Services.AddSingleton<IGameData, GameData>();
        }
    }
}
