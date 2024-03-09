using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcCoreChatApp.Data;
using MvcCoreChatApp.Entities;

namespace MvcCoreChatApp.Controllers
{
    public class MessageController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;


        public MessageController(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
         
        }

        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult GetChatMessages(int chatId)
        {
         
           
            Console.WriteLine("getchatmessages action worked");
            var messages = _dbcontext.Chats.Include(c=>c.Message).
                ThenInclude(x=>x.AppUser)
                .Where(m => m.Id == chatId)
                .FirstOrDefault();

        

            return PartialView("_ChatMessages", messages);
        }

    }
}
