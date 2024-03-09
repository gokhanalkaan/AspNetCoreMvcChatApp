using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcCoreChatApp.Entities;
using MvcCoreChatApp.Models;

namespace MvcCoreChatApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            Console.WriteLine(loginViewModel.Password);

            var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, true);
            if (result.Succeeded)
            {
             
             
                    return RedirectToAction("Index", "Home");
                

            }
            return View();

        }


        public async Task<IActionResult> LogOut()
        {

            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Login");


        }
    }
}
