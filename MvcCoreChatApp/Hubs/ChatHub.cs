using Microsoft.AspNetCore.SignalR;
using MvcCoreChatApp.Data;
using MvcCoreChatApp.Entities;
using Newtonsoft.Json;

namespace MvcCoreChatApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _dbcontext;

        public ChatHub(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task SendMessageToChat(string chatId, int user, string message)
        {
           var usr= _dbcontext.Users.Where(u => u.Id == user).SingleOrDefault();
            

            var newMessage = new Message() 
            { AppUser=usr!,
            ChatId=Int32.Parse(chatId),
            MessageText=message,
            MessageTime=DateTime.Now,
            UserId=user,

            
            };
           _dbcontext.Messages.Add(newMessage);
           _dbcontext.SaveChanges();

            Console.WriteLine("new message from userId:"+user+"to chatId:"+chatId+ newMessage.MessageText); 
           var messageJson= JsonConvert.SerializeObject(newMessage, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

     

            await Clients.Group(chatId).SendAsync("ReceiveMessage", messageJson,chatId);
          
        }

        public async Task JoinChat(string chatId)
        {
            Console.WriteLine("worked join chatId:"+chatId);
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }

        public async Task LeaveChat(string chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);

            Console.WriteLine("worked leave chatId:"+chatId);
        }
    }

}
