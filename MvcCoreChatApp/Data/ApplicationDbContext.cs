using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcCoreChatApp.Entities;

namespace MvcCoreChatApp.Data
{
    public class ApplicationDbContext: IdentityDbContext<AppUser, AppRole, int>
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {


            optionsBuilder.UseSqlServer("Data Source=Gökhan\\MSSQLSERVER01;Initial Catalog=MvcCoreChatDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }



        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatUser> ChatUsers { get; set; }
        public DbSet<Message> Messages { get; set; }

      

    }
}
