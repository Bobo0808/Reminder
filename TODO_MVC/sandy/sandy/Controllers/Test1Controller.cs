using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sandy.Models;
using sandy.ViewModels; // 添加视图模型命名空间
using System.Linq;
using System.Threading.Tasks; // 添加异步任务命名空间

namespace sandy.Controllers
{
    public class Test1Controller : Controller
    {
        private readonly TodoContext _context; // 假设您的 DbContext 名称为 YourDbContext

        public Test1Controller(TodoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // 使用 LINQ 查询来获取 TEST1 视图模型对象
            var viewModelList = await (from todo in _context.Todolist
                                       join user in _context.Webuser
                                       on todo.UserID equals user.UserID
                                       select new TEST1(todo, user)).ToListAsync();

            return View(viewModelList); // 将视图模型传递给视图
        }
    }
}
