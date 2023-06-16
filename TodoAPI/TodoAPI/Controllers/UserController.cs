using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using TodoAPI.Models;

namespace TodoAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TodoContext _todoContext;
        public UserController(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }

        
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> userLogin([FromBody] Webuser user)
        {
            String password = Password.hashPassword(user.Pwd);

            var dbUser = _todoContext.Webuser.Where(u => u.Username == user.Username && u.Pwd == password).FirstOrDefault();


            if (dbUser == null)
            {
                return BadRequest("使用者名稱或密碼錯誤");
            }



            return Ok(dbUser);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> userRegisteration([FromBody] Webuser user)
        {
            String password = Password.hashPassword(user.Pwd);

            var dbUser = _todoContext.Webuser.Where(u => u.Username == user.Username).FirstOrDefault();


            if (dbUser != null)
            {
                return BadRequest("使用者已經註冊過");
            }
            
            user.Pwd = Password.hashPassword(user.Pwd);
            user.Active = 1;
            _todoContext.Webuser.Add(user);
            _todoContext.SaveChanges();


            return Ok("使用者成功註冊");
        }


    }
}
