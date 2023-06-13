using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reminder.Models;
using System.Data.SqlClient;

namespace Reminder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        SqlConnection connection = null;


        public RegisterController(IConfiguration configuration)
        {
            _configuration = configuration;
            connection = new  SqlConnection(_configuration.GetConnectionString("DefaultConnection").ToString());
        }

        [HttpPost]
        [Route("RegisterNewUser")]
        public Response RegisterNewUser(Users users)
        {
            Response response = new Response();
            DAL dAL = new DAL();
            response = dAL.Registration(users ,connection);
            return response;
        }
        [HttpPost]
        [Route("Login")]
        public Response Login(Users users)
        {
            Response response = new Response();
            DAL dAL = new DAL();
            response = dAL.Login(users, connection);
            return response;
        }
    }
}
