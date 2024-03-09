using Microsoft.AspNetCore.Identity;

namespace MvcCoreChatApp.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public override string  UserName { get; set; }
        public string ? ImageUrl { get; set; }



        public List<ChatUser> ChatUsers { get; set; }

        public List<Message> Messages { get; set; }

    


      


    }
}
