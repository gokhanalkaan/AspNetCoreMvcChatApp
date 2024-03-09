using System.ComponentModel.DataAnnotations;

namespace MvcCoreChatApp.Entities
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        public List<ChatUser> ChatUser { get; set; }
        public List<Message> Message { get; set; }
    }
}
