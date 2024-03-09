using MvcCoreChatApp.Entities;

namespace MvcCoreChatApp.Models
{
    public class ChatsViewModel
    {
        public int  ChatId { get; set; }

        public int MyUserId { get; set; }
        public string  FriendName { get; set; }
        public string  FriendUserName { get; set; }
        public string? ImageUrl { get; set; }

        public Message? LastMessage { get; set; }
    }
}
