using Microsoft.AspNetCore.Mvc;
using ReminderApp.Data;

namespace ReminderApp.Controllers
{
    public class ReminderController : Controller
    {
        //使用Dbcontext
        private readonly ReminderDbContext _dbContext;

        public ReminderController(ReminderDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
