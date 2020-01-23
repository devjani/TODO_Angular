using Microsoft.Extensions.Configuration;
using MongoDB.BaseEntity;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MongoDB.Data.Generic
{
    /// <summary>
    /// MongoDB repository
    /// </summary>
    public partial class MongoDBRepository<T> : IMongoDBRepository<T> where T : MongoDBBaseEntity
    {
        #region Fields

        private readonly string _connectionString;

        private readonly IClientSessionHandle _session;
        /// <summary>
        /// Gets the collection
        /// </summary>
        protected IMongoCollection<T> _collection;
        public IMongoCollection<T> Collection
        {
            get
            {
                return _collection;
            }
        }

        /// <summary>
        /// Mongo Database
        /// </summary>
        protected IMongoDatabase _database;
        public IMongoDatabase Database
        {
            get
            {
                return _database;
            }
        }

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public MongoDBRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnection");
            string connectionString = _connectionString;

            if (!string.IsNullOrEmpty(connectionString))
            {
                var client = new MongoClient(connectionString);
                _session = client.StartSession();
                var databaseName = configuration.GetConnectionString("MongoDBName");
                _database = client.GetDatabase(databaseName);
                _collection = _database.GetCollection<T>(typeof(T).Name);
            }
        }

        ~MongoDBRepository()
        {
            _session.Dispose();
        }
        //public MongoDBRepository(string connectionString)
        //{
        //    var client = new MongoClient(connectionString);
        //    var databaseName = new MongoUrl(connectionString).DatabaseName;
        //    _database = client.GetDatabase(databaseName);
        //    _collection = _database.GetCollection<T>(typeof(T).Name);
        //}

        //public MongoDBRepository(IMongoClient client)
        //{
        //    string connectionString = _connectionString;
        //    var databaseName = new MongoUrl(connectionString).DatabaseName;
        //    _database = client.GetDatabase(databaseName);
        //    _collection = _database.GetCollection<T>(typeof(T).Name);
        //}

        //public MongoDBRepository(IMongoDatabase database)
        //{
        //    _database = database;
        //    _collection = _database.GetCollection<T>(typeof(T).Name);
        //}

        #endregion

        #region Methods

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public virtual T GetById(ObjectId id)
        {
            return _collection.Find(e => e.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Get entity by identifier async
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public virtual Task<T> GetByIdAsync(ObjectId id)
        {
            return _collection.Find(e => e.Id == id).FirstOrDefaultAsync();
        }
        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual T Insert(T entity)
        {
            StartTransaction();
            try
            {
                _collection.InsertOne(entity);
                _session.CommitTransaction();
                return entity;
            }
            catch (Exception)
            {
                _session.AbortTransaction();
                throw;
            }

        }

        /// <summary>
        /// Async Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task<T> InsertAsync(T entity)
        {
            StartTransaction();
            try
            {
                await _collection.InsertOneAsync(entity);
                await _session.CommitTransactionAsync();
                return entity;
            }
            catch (Exception)
            {
                await _session.AbortTransactionAsync();
                throw;
            }
        }


        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Insert(IEnumerable<T> entities)
        {
            StartTransaction();
            try
            {
                _collection.InsertMany(entities);
                _session.CommitTransaction();
            }
            catch (Exception)
            {
                _session.AbortTransactionAsync();
                throw;
            }
        }

        /// <summary>
        /// Async Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual async Task<IEnumerable<T>> InsertAsync(IEnumerable<T> entities)
        {
            StartTransaction();
            try
            {
                await _collection.InsertManyAsync(entities);
                await _session.CommitTransactionAsync();
                return entities;
            }
            catch (Exception)
            {
                await _session.AbortTransactionAsync();
                throw;
            }

        }


        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual T Update(T entity)
        {
            StartTransaction();
            try
            {
                _collection.ReplaceOne(x => x.Id == entity.Id, entity, new UpdateOptions() { IsUpsert = false });
                _session.CommitTransaction();
                return entity;
            }
            catch (Exception)
            {
                _session.AbortTransaction();
                throw;
            }
        }

        /// <summary>
        /// Async Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task<T> UpdateAsync(T entity)
        {
            StartTransaction();
            try
            {
                await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity, new UpdateOptions() { IsUpsert = false });
                await _session.CommitTransactionAsync();
                return entity;
            }
            catch (Exception)
            {
                await _session.AbortTransactionAsync();
                throw;
            }
        }


        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Update(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                StartTransaction();
                try
                {
                    Update(entity);
                    _session.CommitTransaction();
                }
                catch (Exception)
                {
                    _session.AbortTransaction();
                    throw;
                }
            }
        }

        /// <summary>
        /// Async Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual async Task<IEnumerable<T>> UpdateAsync(IEnumerable<T> entities)
        {
            var tEntities = entities;
            foreach (T entity in tEntities)
            {
                StartTransaction();
                try
                {
                    await UpdateAsync(entity);
                    await _session.CommitTransactionAsync();
                }
                catch (Exception)
                {
                    await _session.AbortTransactionAsync();
                    throw;
                }
            }
            return tEntities;
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Delete(T entity)
        {
            StartTransaction();
            try
            {
                _collection.FindOneAndDelete(e => e.Id == entity.Id);
                _session.CommitTransaction();
            }
            catch (Exception)
            {
                _session.AbortTransaction();
                throw;
            }
        }

        /// <summary>
        /// Async Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task<T> DeleteAsync(T entity)
        {
            StartTransaction();
            try
            {
                await _collection.DeleteOneAsync(e => e.Id == entity.Id);
                await _session.CommitTransactionAsync();
                return entity;
            }
            catch (Exception)
            {
                await _session.AbortTransactionAsync();
                throw;
            }

        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Delete(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                StartTransaction();
                try
                {
                    _collection.FindOneAndDeleteAsync(e => e.Id == entity.Id);
                    _session.CommitTransaction();
                }
                catch (Exception)
                {
                    _session.AbortTransaction();
                    throw;
                }
            }
        }

        /// <summary>
        /// Async Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual async Task<IEnumerable<T>> DeleteAsync(IEnumerable<T> entities)
        {
            var tEntities = entities;
            foreach (T entity in tEntities)
            {
                StartTransaction();
                try
                {
                    await DeleteAsync(entity);
                    await _session.CommitTransactionAsync();
                    return entities;
                }
                catch (Exception)
                {
                    await _session.AbortTransactionAsync();
                    throw;
                }
            }
            return tEntities;
        }


        #endregion


        #region Methods

        /// <summary>
        /// Determines whether a list contains any elements
        /// </summary>
        /// <returns></returns>
        public virtual bool Any()
        {
            return _collection.AsQueryable().Any();
        }

        /// <summary>
        /// Get List
        /// </summary>
        /// <returns>List</returns>        
        public virtual async Task<List<T>> List()
        {
            return await _collection.AsQueryable().ToListAsync();
        }

        /// <summary>
        /// Determines whether any element of a list satisfies a condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual bool Any(Expression<Func<T, bool>> where)
        {
            return _collection.Find(where).Any();
        }

        /// <summary>
        /// Async determines whether a list contains any elements
        /// </summary>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync()
        {
            return await _collection.AsQueryable().AnyAsync();
        }

        /// <summary>
        /// Async determines whether any element of a list satisfies a condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> where)
        {
            return await _collection.Find(where).AnyAsync();
        }

        /// <summary>
        /// Returns the number of elements in the specified sequence.
        /// </summary>
        /// <returns></returns>
        public virtual long Count()
        {
            return _collection.CountDocuments(new BsonDocument());
        }

        /// <summary>
        /// Returns the number of elements in the specified sequence that satisfies a condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual long Count(Expression<Func<T, bool>> where)
        {
            return _collection.CountDocuments(where);
        }

        /// <summary>
        /// Async returns the number of elements in the specified sequence
        /// </summary>
        /// <returns></returns>
        public virtual async Task<long> CountAsync()
        {
            return await _collection.CountDocumentsAsync(new BsonDocument());
        }

        /// <summary>
        /// Async returns the number of elements in the specified sequence that satisfies a condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual async Task<long> CountAsync(Expression<Func<T, bool>> where)
        {
            return await _collection.CountDocumentsAsync(where);
        }


        #endregion

        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IMongoQueryable<T> Table
        {
            get { return _collection.AsQueryable(); }
        }

        /// <summary>
        /// Get collection by filter definitions
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async virtual Task<IList<T>> FindByFilterDefinitionAsync(FilterDefinition<T> query)
        {
            return await _collection.Find(query).ToListAsync();
        }


        /// <summary>
        /// Get collection by filter definitions
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual IList<T> FindByFilterDefinition(FilterDefinition<T> query)
        {
            return _collection.Find(query).ToList();
        }


        /// <summary>
        ///  Get entity by Filter Query.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async virtual Task<T> FindSingleByFilterDefinitionAsync(FilterDefinition<T> query)
        {
            return await _collection.Find(query).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Find Entity and Update
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async virtual Task<T> FindOneAndUpdateAsync(FilterDefinition<T> filter,
            UpdateDefinition<T> update)
        {
            StartTransaction();
            try
            {
                var entity = await _collection.FindOneAndUpdateAsync(filter, update);
                await _session.CommitTransactionAsync();
                return entity;
            }
            catch (Exception)
            {
                await _session.AbortTransactionAsync();
                throw;
            }
        }

        /// <summary>
        /// Find Entity and Delete
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async virtual Task<T> FindOneAndDeleteAsync(FilterDefinition<T> filter)
        {
            StartTransaction();
            try
            {
                var entity = await _collection.FindOneAndDeleteAsync(filter);
                await _session.CommitTransactionAsync();
                return entity;
            }
            catch (Exception)
            {
                await _session.AbortTransactionAsync();
                throw;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Start transaction if transaction is already in progress
        /// </summary>
        private void StartTransaction()
        {
            if (!_session.IsInTransaction)
                _session.StartTransaction();
        }

        #endregion
    }
}
