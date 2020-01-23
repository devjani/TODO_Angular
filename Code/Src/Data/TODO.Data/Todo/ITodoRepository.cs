using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TODO.Model.Entities;

namespace TODO.Data.Todo
{
    public interface ITodoRepository
    {
        Task<Todos> AddTodo(Todos todo);
        Task<Todos> GetTodoById(string id);
        Task<Todos> UpdateTodo(Todos todo);
        Task<bool> DeleteTodo(string id);
        Task<List<Todos>> GetTodoList();
    }
}
