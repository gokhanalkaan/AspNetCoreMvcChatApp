using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcCoreChatApp.Data;
using MvcCoreChatApp.Entities;
using MvcCoreChatApp.Models;
using NuGet.Protocol;

namespace MvcCoreChatApp.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(ApplicationDbContext dbcontext, UserManager<AppUser> userManager)
        {
            _dbcontext = dbcontext;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(RegisterViewModel registerViewModel)
        {
         

            AppUser appUser = new AppUser()
            {
                Name = registerViewModel.Name,
            
                UserName = registerViewModel.UserName,
                
                ImageUrl = registerViewModel.ImageUrl,
               
                



            };
            Console.WriteLine(appUser.UserName);
            var result = await _userManager.CreateAsync(appUser, registerViewModel.Password);
            Console.WriteLine(result);

            if (result.Succeeded)
            {
              
              
                return RedirectToAction("Index", "Login");

            }
             return View();
        
          
        }
    }
}
