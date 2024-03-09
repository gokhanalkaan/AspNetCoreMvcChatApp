using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using MvcCoreChatApp.Data;
using MvcCoreChatApp.Entities;
using MvcCoreChatApp.Models;

namespace MvcCoreChatApp.ViewComponents
{
    public class _Chatlist:ViewComponent
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly UserManager<AppUser> _userManager;

        public _Chatlist(ApplicationDbContext dbcontext, UserManager<AppUser> userManager)
        {
            _dbcontext = dbcontext;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var user = await _userManager.GetUserAsync((System.Security.Claims.ClaimsPrincipal)User);
            if (user == null)
            {
                return Content("<script>window.location.href='/Login/Index';</script>");
            }

            


            var chatList = _dbcontext.ChatUsers
                  .Include(c => c.Chat)
                  .Where(u => u.UserId == user.Id)
                  .ToList();

            var chatsVm = new List<ChatsViewModel>();
            foreach (var chat in chatList)
            {
                Console.WriteLine("chatId:" + chat.Id);

                var lastMessage = _dbcontext.Messages
                    .Include(a => a.AppUser)
                    .Where(m => m.ChatId == chat.Id)
                    .OrderByDescending(m => m.MessageTime)
                    .FirstOrDefault();
                var otherUser = _dbcontext.ChatUsers
                    .Include(x => x.User)
                    .Where(u => u.ChatId == chat.ChatId && u.UserId != user.Id)
                    .FirstOrDefault();
                Console.WriteLine("friend:" + otherUser.User.UserName);

                chatsVm.Add(new ChatsViewModel()
                {
                    ChatId = chat.ChatId,
                    FriendName = otherUser.User.Name,
                    FriendUserName = chat.User.UserName,
                    ImageUrl = otherUser.User.ImageUrl,
                    LastMessage = lastMessage
                });




            }

            return View(chatsVm);

        }
    }
}
