using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcCoreChatApp.Data;
using MvcCoreChatApp.Entities;
using MvcCoreChatApp.Models;
using System.Diagnostics;

namespace MvcCoreChatApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(ApplicationDbContext dbcontext, UserManager<AppUser> userManager)
        {
            _dbcontext = dbcontext;
            _userManager = userManager;
        }

        public async  Task<IActionResult> Index()
        {
             var user = await _userManager.GetUserAsync(User);
             if (user == null)
             {
                 return RedirectToAction("Index", "Login");
             }

           var chatList=_dbcontext.ChatUsers
                 .Include(c => c.Chat)
                 .Where(u => u.UserId == user.Id)
                 .ToList();

             var chatsVm=new List<ChatsViewModel>();
             foreach(var chat in chatList)
             {
                 Console.WriteLine("chatId:" +chat.ChatId);

                 
                 var otherUser = _dbcontext.ChatUsers
                     .Include(x => x.User)
                     .Where(u => u.ChatId == chat.ChatId && u.UserId != user.Id)
                     .FirstOrDefault();

                var lastMessage = _dbcontext.Messages
                     .Include(a => a.AppUser)
                     .Where(m => m.ChatId == chat.ChatId )
                     .OrderByDescending(m => m.MessageTime)
                     .FirstOrDefault();
                Console.WriteLine("friend:"+ otherUser.User.UserName);
                Console.WriteLine("lastmessage:"+ lastMessage?.MessageText);

                 chatsVm.Add(new ChatsViewModel()
                 {
                     ChatId = chat.ChatId,
                     FriendName = otherUser.User.Name,
                     FriendUserName = chat.User.UserName,
                     ImageUrl = otherUser.User.ImageUrl,
                     LastMessage = lastMessage,
                     MyUserId=user.Id,
                 });




             }

             return View(chatsVm);
         
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
