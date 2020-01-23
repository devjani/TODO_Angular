using AutoMapper;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.Data.Todo;
using TODO.Model.Common;
using TODO.Model.DTO;
using TODO.Model.Entities;

namespace TODO.Business.Todo
{
    public class TodoBL: ITodoBL
    {
        #region Private Variables
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;
        #endregion


        #region Constructor
        public TodoBL(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteTodo(string todoId)
        {
            return await _todoRepository.DeleteTodo(todoId);
        }

        public async Task<TodoDTO> GetTodoById(string todoId)
        {
            return _mapper.Map<Todos, TodoDTO>(await _todoRepository.GetTodoById(todoId));
        }

        public async Task<PaginationResponse<TodoDTO>> GetTodoList(PaginationRequest request)
        {
            var todoList = _mapper.Map<List<TodoDTO>>(await _todoRepository.GetTodoList());
            return new PaginationResponse<TodoDTO>()
            {
                TotalRecords = todoList.Count,
                Data = request.PageNo != 0 ? todoList.Skip((request.PageNo - 1) * request.PageSize)
                         .Take(request.PageSize).ToList() : todoList

            };
        }

        public async Task<bool> SaveTodo(TodoDTO todoDTO)
        {
            var todo = _mapper.Map<TodoDTO, Todos>(todoDTO);
            if (!string.IsNullOrEmpty(todoDTO.TodoId))
            {
                todo.Id = ObjectId.Parse(todoDTO.TodoId);
                return await _todoRepository.UpdateTodo(todo) != null;
            }
            else
                return await _todoRepository.AddTodo(todo) != null;
        }
        #endregion
    }
}
