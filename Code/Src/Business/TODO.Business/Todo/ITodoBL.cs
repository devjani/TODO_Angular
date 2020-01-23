using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TODO.Model.Common;
using TODO.Model.DTO;

namespace TODO.Business.Todo
{
    public interface ITodoBL
    {
        Task<bool> SaveTodo(TodoDTO todoDTO);
        Task<PaginationResponse<TodoDTO>> GetTodoList(PaginationRequest request);
        Task<bool> DeleteTodo(string todoId);
        Task<TodoDTO> GetTodoById(string todoId);
    }
}
