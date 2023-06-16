using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                return BadRequest("你不能創立一個無效的代辦事項");
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
        



      
    }


}
  
