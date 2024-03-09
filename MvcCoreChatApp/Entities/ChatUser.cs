using System.ComponentModel.DataAnnotations;

namespace MvcCoreChatApp.Entities
{
    public class ChatUser
    {
        [Key]
        public int Id { get; set; }

        public int ChatId { get; set; }
        public Chat Chat { get; set; }

        public int UserId { get; set; }
        public AppUser User { get; set; }


    }
}
