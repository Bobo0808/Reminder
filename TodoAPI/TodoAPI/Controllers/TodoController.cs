using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.Models;
using TodoAPI.ViewModel;

namespace TodoAPI.Controllers
{
    [EnableCors("AllowAny")]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _todoContext;

        public TodoController(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }


        //[HttpPost("AddTodo")]
        //[Route("addTodo")]
        //public async Task<IActionResult> AddTodo(AddTodoRequest request)
        //{
            
        //          return Ok("");
        //}
    }


}
  
