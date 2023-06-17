using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TodoAPI.Models;
using TodoAPI.ViewModel;

namespace TodoAPI.Controllers
{
    //[EnableCors("AllowAny")]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _todoContext;

        public TodoController(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateTodo (AddTodoRequest addTodoRequest)
        {
            if(addTodoRequest == null)
            {
                return BadRequest("你不能創立一個無效的代待辦事項");
            }
            var Todo = new Todolist
            {
                //TodoId = addTodoRequest.TodoId,
                UserId = addTodoRequest.UserId,
                Category = addTodoRequest.Category,
                Remark = addTodoRequest.Remark,
                CreateDate = DateTime.Now,
                //ScaheduledDate = addTodoRequest.ScaheduledDate != null ? addTodoRequest.ScaheduledDate : null,
                ScaheduledDate = addTodoRequest.ScaheduledDate,
                IsCompleted = addTodoRequest.IsCompleted,

            };
            _todoContext.Todolist.Add(Todo);
            await _todoContext.SaveChangesAsync();
            return Ok("新增成功");

        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateTodo(UpdateTodoRequest updateTodoRequest)
        {
            if(updateTodoRequest == null)
            {
                return BadRequest("你不能創立一個無效的代辦事項");
            }

            //根據待辦事項唯一識別ID檢索待辦事項

            var Utodo = await _todoContext.Todolist.FirstOrDefaultAsync(t => t.TodoId == updateTodoRequest.TodoId);

            if (Utodo == null)
            {
                return NotFound("找不到待辦事項");
            }

            Utodo.UserId = updateTodoRequest.UserId;
            Utodo.Category = updateTodoRequest.Category;
            Utodo.Remark = updateTodoRequest.Remark;
            Utodo.ScaheduledDate = updateTodoRequest.ScaheduledDate;
            Utodo.ModifyDate= DateTime.Now;
            Utodo.IsCompleted = updateTodoRequest.IsCompleted;

            await _todoContext.SaveChangesAsync();

            return Ok("更新成功");
        }

        [HttpGet]
        [Route("Completed")]
        public async Task<IActionResult> GetCompletedTodos()
        {
            
            var completedTodos = await _todoContext.Todolist.Where(t => t.IsCompleted == false).ToListAsync();

           
            return Ok(completedTodos);
        }
        [HttpGet]
        [Route("getAllUserTodolist")]
        public async Task<IActionResult> getAllUserTodolist(int userId)
        {
            var userTodolist = await _todoContext.Todolist
            .Where(t => t.UserId == userId)
            .ToListAsync();

            return Ok(userTodolist);
        }

        //[HttpGet]
        //[Route("getTodobyTodoId/{id}")]
        //public async Task<IActionResult> getUserTodobByTodoId (int id)
        //{

        //}


        [HttpDelete]
        [Route("Delete/{todoId}")]
        public async Task<IActionResult> DeleteTodoItem(int todoId)
        {
            
            var todoItem = await _todoContext.Todolist.FirstOrDefaultAsync(t => t.TodoId == todoId);

            if (todoItem == null)
            {
                return NotFound("找不到待辦事項");
            }

            
            _todoContext.Todolist.Remove(todoItem);
            await _todoContext.SaveChangesAsync();

            return Ok("待辦事項已成功刪除");
        }



    }


}
  
