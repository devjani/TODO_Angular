using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Data.Generic;
using MongoDB.Driver;
using TODO.Model.Entities;

namespace TODO.Data.Todo
{
    public class TodoRepository: ITodoRepository
    {
        #region Private Variables
        private readonly IMongoDBRepository<Todos> _todoRepository;
        #endregion

        #region Constructor
        public TodoRepository(IMongoDBRepository<Todos> todoRepository
        )
        {
            _todoRepository = todoRepository;
        }

        public async Task<Todos> AddTodo(Todos todo)
        {
            todo.CreatedDate = DateTime.UtcNow;
            todo.UpdatedDate = DateTime.UtcNow;
            await _todoRepository.InsertAsync(todo);
            return todo;
        }

        public async Task<bool> DeleteTodo(string id)
        {
            var todo = await GetTodoById(id);
            if (todo != null)
            {
                _todoRepository.Delete(todo);
                return true;
            }
            return false;
        }

        public async Task<Todos> GetTodoById(string id)
        {
            var filter = Builders<Todos>.Filter.Where(x => x.Id == ObjectId.Parse(id));
            return await _todoRepository.FindSingleByFilterDefinitionAsync(filter);
        }

        public async Task<List<Todos>> GetTodoList()
        {
            return await _todoRepository.Table.ToListAsync();
        }

        public async Task<Todos> UpdateTodo(Todos todo)
        {
            var filter = Builders<Todos>.Filter.Where(x => x.Id == todo.Id);
            var update = Builders<Todos>.Update.Set(x => x.Name, todo.Name)
                                               .Set(x => x.Description, todo.Description)
                                               .Set(x => x.UpdatedDate,DateTime.UtcNow);

            return await _todoRepository.FindOneAndUpdateAsync(filter, update);
        }
        #endregion
    }
}
