using System.Transactions;

namespace Data.Generic
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading;
    using System.Threading.Tasks;
    using Dapper;

    /// <summary>
    /// Its a generic interface
    /// </summary>
    /// <typeparam name="T">Its name of enitity type</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Its generic function to get list of data by using procedure async
        /// </summary>
        /// <param name="query">Name of store procedure</param>
        /// <param name="parameters">list of parameter for procedure</param>
        /// <returns>
        /// Returns enumerable for specified entity
        /// </returns>
        Task<IEnumerable<T>> QueryListByProcedureAsync(string query, DynamicParameters parameters);

        /// <summary>
        /// Its generic function to get list of data by using procedure
        /// </summary>
        /// <param name="query">Name of store procedure</param>
        /// <param name="parameters">list of parameter for procedure</param>
        /// <returns>Returns enumerable for specified entity</returns>
        IEnumerable<T> QueryListByProcedure(string query, DynamicParameters parameters);

        /// <summary>
        /// Its a generic function to get single object by using procedure
        /// </summary>
        /// <param name="query">Name of stored procedure</param>
        /// <param name="parameters">List of parameters for procedure</param>
        /// <returns>Specified entity object</returns>
        Task<T> QuerySingleByProcedureAsync(string query, DynamicParameters parameters);

        /// <summary>
        /// Its a generic function to get single object by using procedure
        /// </summary>
        /// <param name="query">Name of stored procedure</param>
        /// <param name="parameters">List of parameters for procedure</param>
        /// <returns>Specified entity object</returns>
        T QuerySingleByProcedure(string query, DynamicParameters parameters);

        /// <summary>
        /// Its a function to add/update/delete by stored procedure async
        /// </summary>
        /// <param name="query">Name of stored procedure</param>
        /// <param name="parameters">List of parameters</param>
        /// <returns>No of affected rows</returns>
        Task<int> ExecuteProcedureAsync(string query, DynamicParameters parameters);

        /// <summary>
        /// Its a function to add/update/delete by stored procedure
        /// </summary>
        /// <param name="query">Name of stored procedure</param>
        /// <param name="parameters">List of parameters</param>
        /// <returns>No of affected rows</returns>
        int ExecuteProcedure(string query, DynamicParameters parameters);

        /// <summary>
        /// Its a generic function to execute sp with multiple result set
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Disctionary with multiple results in it.</returns>
        IDictionary QueryMultipleByProcedure(string query, DynamicParameters parameters);

        /// <summary>
        /// Its a generic function to get single object by using procedure
        /// </summary>
        /// <param name="query">Name of stored procedure</param>
        /// <param name="parameters">List of parameters for procedure</param>
        /// <returns>
        /// Disctionary with multiple results in it.
        /// </returns>
        Task<IDictionary> QueryMultipleByProcedureAsync(string query, DynamicParameters parameters);

        /// <summary>
        /// Return Connection 
        /// </summary>
        /// <returns></returns>
        IDbConnection GetConnection();

        /// <summary>
        /// Open Connection
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <returns></returns>
        Task OpenAsync(IDbConnection dbConnection);
        
    }
}
