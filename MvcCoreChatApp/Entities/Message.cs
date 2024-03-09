namespace MvcCoreChatApp.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string MessageText { get; set; }

        public int UserId { get; set; }
        public AppUser AppUser { get; set; }

        public int ChatId { get; set; }
        public Chat Chat { get; set; }

        public DateTime MessageTime { get; set; }
    }
}
