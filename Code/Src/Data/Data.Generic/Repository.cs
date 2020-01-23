namespace Data.Generic
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Collections;
    using System.Threading.Tasks;
    using Dapper;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Transactions;
    using MySql.Data.MySqlClient;

    /// <summary>
    /// Its a generic repository to get data from mysql database
    /// </summary>
    /// <typeparam name="T">Its name of entity</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Private Methods
        // Its connection string to connect database
        private readonly string _connectionString;
        #endregion

        #region Public Methods
        /// <summary>
        /// Its a construction to create its object
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Repository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnection");
        }

        #endregion

        #region Variables

        //Its a property to access connection object
        public IDbConnection Connection => new MySqlConnection(_connectionString);

        #endregion

        #region Public Methods

        /// <summary>
        /// Return Connection
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            return Connection;
        }

        /// <summary>
        /// Its generic function to get list of data by using procedure
        /// </summary>
        /// <param name="query">Name of store procedure</param>
        /// <param name="parameters">list of parameter for procedure</param>
        /// <returns>
        /// Returns enumerable for specified entity
        /// </returns>
        public async Task<IEnumerable<T>> QueryListByProcedureAsync(string query, DynamicParameters parameters)
        {
            using (var dbConnection = Connection)
            {
                IEnumerable<T> list;
                await OpenAsync(dbConnection);
                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        list = await dbConnection.QueryAsync<T>(query, parameters, commandType: CommandType.StoredProcedure);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
                return list;
            }
        }

        /// <summary>
        /// Its generic function to get list of data by using procedure
        /// </summary>
        /// <param name="query">Name of store procedure</param>
        /// <param name="parameters">list of parameter for procedure</param>
        /// <returns>
        /// Returns enumerable for specified entity
        /// </returns>
        public IEnumerable<T> QueryListByProcedure(string query, DynamicParameters parameters)
        {
            using (IDbConnection dbConnection = Connection)
            {
                IEnumerable<T> list;
                Open(dbConnection);
                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        list = dbConnection.Query<T>(query, parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
                        transaction.Commit();
                    }
                    catch (Exception )
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
                return list;
            }
        }

        /// <summary>
        /// Its a generic function to get single object by using procedure
        /// </summary>
        /// <param name="query">Name of stored procedure</param>
        /// <param name="parameters">List of parameters for procedure</param>
        /// <returns>
        /// Specified entity object
        /// </returns>
        public async Task<T> QuerySingleByProcedureAsync(string query, DynamicParameters parameters)
        {
            using (IDbConnection dbConnection = Connection)
            {
                await OpenAsync(dbConnection);
                IEnumerable<T> list;
                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        list = await dbConnection.QueryAsync<T>(query, parameters, commandType: CommandType.StoredProcedure);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
                return list.FirstOrDefault();
            }
        }

        /// <summary>
        /// Its a generic function to get single object by using procedure
        /// </summary>
        /// <param name="query">Name of stored procedure</param>
        /// <param name="parameters">List of parameters for procedure</param>
        /// <returns>
        /// Specified entity object
        /// </returns>
        public T QuerySingleByProcedure(string query, DynamicParameters parameters)
        {
            using (IDbConnection dbConnection = Connection)
            {
                Open(dbConnection);
                IEnumerable<T> list;
                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        list = dbConnection.Query<T>(query, parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
                        transaction.Commit();
                    }
                    catch (Exception )
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
                return list.FirstOrDefault();
            }
        }

        /// <summary>
        /// Its a generic function to get single object by using procedure
        /// </summary>
        /// <param name="query">Name of stored procedure</param>
        /// <param name="parameters">List of parameters for procedure</param>
        /// <returns>
        /// Disctionary with multiple results in it.
        /// </returns>
        public IDictionary QueryMultipleByProcedure(string query, DynamicParameters parameters)
        {
            IDictionary keyValuePairs = new Dictionary<string, dynamic>();
            using (IDbConnection dbConnection = Connection)
            {
                Open(dbConnection);
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        var results = dbConnection.QueryMultiple(query, parameters, commandType: CommandType.StoredProcedure);

                        int i = 0;
                        while (!results.IsConsumed)
                        {
                            i++;
                            keyValuePairs.Add("result" + i, results.Read());
                        }
                        transaction.Complete();
                    }
                    catch (Exception )
                    {
                        transaction.Dispose();
                        throw;
                    }
                }
            }

            return keyValuePairs;
        }

        /// <summary>
        /// Its a generic function to get single object by using procedure
        /// </summary>
        /// <param name="query">Name of stored procedure</param>
        /// <param name="parameters">List of parameters for procedure</param>
        /// <returns>
        /// Disctionary with multiple results in it.
        /// </returns>
        public async Task<IDictionary> QueryMultipleByProcedureAsync(string query, DynamicParameters parameters)
        {
            IDictionary keyValuePairs = new Dictionary<string, dynamic>();
            using (IDbConnection dbConnection = Connection)
            {
                await OpenAsync(dbConnection);
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        var results = await dbConnection.QueryMultipleAsync(query, parameters, commandType: CommandType.StoredProcedure);

                        int i = 0;
                        while (!results.IsConsumed)
                        {
                            i++;
                            keyValuePairs.Add("result" + i, results.Read());
                        }
                        transaction.Complete();
                    }
                    catch (Exception )
                    {
                        transaction.Dispose();
                        throw;
                    }
                }
            }

            return keyValuePairs;
        }

        /// <summary>
        /// Its a function to add/update/delete by stored procedure async
        /// </summary>
        /// <param name="query">Name of stored procedure</param>
        /// <param name="parameters">List of parameters</param>
        /// <returns>
        /// Int value
        /// </returns>
        public async Task<int> ExecuteProcedureAsync(string query, DynamicParameters parameters)
        {
            using (IDbConnection dbConnection = Connection)
            {
                await OpenAsync(dbConnection);
                int response;
                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        response = await dbConnection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
                        transaction.Commit();
                    }
                    catch (Exception )
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
                return response;
            }
        }

        /// <summary>
        /// Its a function to add/update/delete by stored procedure
        /// </summary>
        /// <param name="query">Name of stored procedure</param>
        /// <param name="parameters">List of parameters</param>
        /// <returns>No of affected rows</returns>
        public int ExecuteProcedure(string query, DynamicParameters parameters)
        {
            using (IDbConnection dbConnection = Connection)
            {
                Open(dbConnection);
                int response = 0;
                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        response = dbConnection.Execute(query, parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
                        transaction.Commit();
                    }
                    catch (Exception )
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
                return response;
            }
        }



        #endregion

        #region Private Methods

        /// <summary>
        /// Opens the asynchronous.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <returns>
        /// Async task
        /// </returns>
        public async Task OpenAsync(IDbConnection dbConnection)
        {
            bool wasClosed = dbConnection.State == ConnectionState.Closed;
            if (wasClosed)
                await ((MySqlConnection)dbConnection).OpenAsync();
        }

        /// <summary>
        /// Opens the specified database connection.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        private void Open(IDbConnection dbConnection)
        {
            bool wasClosed = dbConnection.State == ConnectionState.Closed;
            if (wasClosed)
                dbConnection.Open();
        }

        #endregion
    }
}
