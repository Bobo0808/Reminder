using Microsoft.AspNetCore.Mvc;
using TodoAPI.Models;

namespace TodoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly TodoContext _context;

        public WeatherForecastController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Webuser")]
        public async Task<IActionResult> GetWebusers()
        {
            List<Webuser>listUsers = _context.Webuser.ToList();
            try
            {
                if(listUsers != null)
                {
                    return Ok(listUsers);
                }
                return Ok("沒有這個使用者");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

        
    
}