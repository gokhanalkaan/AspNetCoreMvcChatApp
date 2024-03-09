using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcCoreChatApp.Data;
using MvcCoreChatApp.Entities;

namespace MvcCoreChatApp.Controllers
{
    public class ChatController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ChatController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index(int ?id)
        {
          var chat=  _context.Chats.Include(p => p.Message).ThenInclude(m => m.AppUser).Where(c=>c.Id==id).ToList();
            return View(chat);
        }

        public async Task<IActionResult> CreateChat(int userId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
    

            var newChat=new Chat()
            { ChatUser=  new List<ChatUser>(){
                new ChatUser()
                { UserId=userId,


                },
                new ChatUser()
                {
                    UserId=user.Id
                }
            
            }
                
            };
            _context.Chats.Add(newChat);
            _context.SaveChanges();

           
            return Redirect("/Home/Index");
        }
    }
}
