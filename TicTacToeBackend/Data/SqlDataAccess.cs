using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using TicTacToe.Interfaces;

namespace TicTacToe.Data
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> LoadData<T, U>(string query, U parameters, string connctionId = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connctionId));

            return await connection.QueryAsync<T>(query, parameters);
        }

        public async Task SaveData<T>(string query, T parameters, string connctionId = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connctionId));

            await connection.ExecuteAsync(query, parameters);
        }
    }
}
