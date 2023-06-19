using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using TodoAPI.Models;
using TodoAPI.ViewModel;

namespace TodoAPI.Controllers
{
    [EnableCors("appCors")]
    [Authorize]
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
            try
            {
                //從HttpContext 中獲取當前使用者的使用者ID。該程式碼假設使用者的ID以字串形式存儲在身份驗證主張（claim）中，並且使用者已經通過身份驗證並登錄。
                int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));
                if (addTodoRequest == null)
                {
                    return BadRequest("你不能創立一個無效的代待辦事項");
                }
                if (userID == null || userID <=0) 
                {
                    return BadRequest("這個使用者不允許使用這個端點");
                }
                var Todo = new Todolist
                {
                    //TodoId = addTodoRequest.TodoId,
                    UserId = userID,
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
                
            

        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateTodo(UpdateTodoRequest updateTodoRequest)
        {
            try
            {


                //取得正確的userID
                int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));
                if (updateTodoRequest == null || updateTodoRequest.TodoId == null || updateTodoRequest.TodoId <= 0 )
                {
                    return BadRequest("你不能創立一個無效的代辦事項");
                }

                if (userID == null || userID <= 0)
                {
                    return BadRequest("這個使用者不允許使用這個端點");
                }
                //根據待辦事項唯一識別ID檢索待辦事項

                var Utodo =await  _todoContext.Todolist.Where(t => t.TodoId == updateTodoRequest.TodoId && t.UserId == userID).FirstOrDefaultAsync();


                if (Utodo == null)
                {
                    return NotFound("無法更新");
                }

               
                Utodo.Category = updateTodoRequest.Category;
                Utodo.Remark = updateTodoRequest.Remark;
                Utodo.ScaheduledDate = updateTodoRequest.ScaheduledDate;
                Utodo.ModifyDate = DateTime.Now;
                Utodo.IsCompleted = updateTodoRequest.IsCompleted;

                _todoContext.Entry(Utodo).State = EntityState.Modified;
                await _todoContext.SaveChangesAsync();

                return Ok("更新成功");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Completed")]
        public async Task<IActionResult> getUserFalseTodolist()
        {
            try
            {
                int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

                if (userID == null || userID <= 0)
                {
                    return BadRequest("這個使用者不允許使用這個端點");
                }

                var userTodolist = _todoContext.Todolist
                    .Where(t => t.UserId == userID && t.IsCompleted == true)
                    .Select(t => new
                    {
                        t.TodoId,
                        t.UserId,
                        t.Category,
                        t.ModifyDate,
                        t.CreateDate,
                        t.IsCompleted,
                        t.ScaheduledDate,
                        t.Remark
                    })
                    .ToList();

                return Ok(userTodolist);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("getAllUserTodolist")]
        public async Task<IActionResult> getAllUserTodolist()
        {
            try
            {
                int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));
               
                if (userID == null || userID <= 0)
                {
                    return BadRequest("這個使用者不允許使用這個端點");
                }
               var userTodolist = _todoContext.Todolist.Select(t => new
                {
                    t.TodoId,
                    t.UserId,
                    t.Category,
                    t.ModifyDate,
                    t.CreateDate,
                    t.IsCompleted,
                    t.ScaheduledDate,
                    t.Remark
                }).Where(t => t.UserId == userID).ToList();
                return Ok(userTodolist);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getTodobyTodoId/{id}")]
        public async Task<IActionResult> getUserTodobByTodoId(int id)
        {
            try
            {
                int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

                if (userID == null || userID <= 0)
                {
                    return BadRequest("這個使用者不允許使用這個端點");
                }
                
                if (id==null || id <= 0)
                {
                    return BadRequest("這個待做事項不存在");
                }
                var TodoidTask = _todoContext.Todolist.Select(t => new
                {
                    t.TodoId,
                    t.UserId,
                    t.Category,
                    t.ModifyDate,
                    t.CreateDate,
                    t.IsCompleted,
                    t.ScaheduledDate,
                    t.Remark
                }).Where(t => t.UserId == userID && t.TodoId ==id).FirstOrDefault();
                return Ok(TodoidTask);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {

            try
            {
                int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

                if (userID == null || userID <= 0)
                {
                    return BadRequest("這個使用者不允許使用這個端點");
                }

                if (id == null || id <= 0)
                {
                    return BadRequest("無效的TodoID");
                }
                
                var TodoTask = _todoContext.Todolist.Where(t => t.UserId == userID && t.TodoId == id).FirstOrDefault();
                if (TodoTask == null)
                {
                    return BadRequest("你不能刪除不存在的待辦事項");
                }
                    _todoContext.Todolist.Remove(TodoTask);
                await _todoContext.SaveChangesAsync();
                return Ok("已成功刪除");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            
        }



    }


}
  
