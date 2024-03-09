using Microsoft.AspNetCore.Mvc;
using MvcCoreChatApp.Data;

namespace MvcCoreChatApp.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search(string username)
        {
         
            var users = _context.Users.Where(u => u.UserName.Contains(username)).ToList();
            return View(users);
        }
    }
}
