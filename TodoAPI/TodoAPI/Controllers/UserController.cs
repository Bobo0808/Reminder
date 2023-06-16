using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoAPI.Models;

namespace TodoAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TodoContext _todoContext;
        private readonly IConfiguration _configuration;
        public UserController(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }

        
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> userLogin([FromBody] Webuser user)
        {
            try
            {
                String password = Password.hashPassword(user.Pwd);

                var dbUser = _todoContext.Webuser.Where(u => u.Username == user.Username && u.Pwd == password).Select(u => new
                {
                    u.UserId,
                    u.Username,
                    u.Active
                }).FirstOrDefault();


                if (dbUser == null)
                {
                    return BadRequest("使用者名稱或密碼錯誤");
                }

                //List<Claim> autClaims = new List<Claim>
                //{
                //    new Claim(ClaimTypes.Name,dbUser.Username),
                //    new Claim("UserId",dbUser.UserId.ToString())
                //};
                //var token = this.getToken(autClaims);

                //return Ok(new
                //{
                //    token = new JwtSecurityTokenHandler().WriteToken(token),
                //    expiration = token.ValidTo
                //});
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }



           
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> userRegisteration([FromBody] Webuser user)
        {
            try
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
                await _todoContext.SaveChangesAsync();
                
                return Ok("使用者成功註冊");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
         
        }

        //private JwtSecurityToken getToken(List<Claim> authClaim)
        //{
        //    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        //    var token = new JwtSecurityToken(
        //        issuer: _configuration["JWT:ValidIssuer"],
        //        audience: _configuration["JWT:ValidAudience"],
        //        expires: DateTime.Now.AddHours(24),
        //        claims: authClaim,
        //        signingCredentials:new SigningCredentials(authSigningKey,SecurityAlgorithms.HmacSha256)
        //        );
        //        return token;
        //}


    }
}
