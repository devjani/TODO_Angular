using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TODO.Business.Todo;
using TODO.Model.Common;
using TODO.Model.DTO;

namespace TODO.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : BaseApiController
    {
        #region PrivateVariables
        private readonly ITodoBL _todoBL;
        #endregion
        #region Constructor
        public TodoController(ITodoBL todoBL)
        {
            _todoBL = todoBL;
        }
        #endregion
        #region Public Methods
        [HttpPost("[action]")]
        public async Task<IActionResult> SaveTodo(TodoDTO todoDTO)
        {
            try
            {
                var response = await _todoBL.SaveTodo(todoDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DeleteTodo(string id)
        {
            try
            {
                var response = await _todoBL.DeleteTodo(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetTodoById(string id)
        {
            try
            {
                var response = await _todoBL.GetTodoById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetTodoList(PaginationRequest request)
        {
            try
            {
                var response = await _todoBL.GetTodoList(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion
    }
}