namespace TicTacToe.Interfaces
{
    public interface ISqlDataAccess
    {
        /// <summary>
        /// Loads data in database.
        /// </summary>
        /// <typeparam name="T">Type of data to save.</typeparam>
        /// <typeparam name="U">Type of parameters.</typeparam>
        /// <param name="query">SQL query.</param>
        /// <param name="parameters">Additional parameters of query.</param>
        /// <param name="connctionId">Connection ID from connection strings in appsettings.json.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> LoadData<T, U>(string query, U parameters, string connctionId = "Default");
        Task SaveData<T>(string query, T parameters, string connctionId = "Default");
    }
}