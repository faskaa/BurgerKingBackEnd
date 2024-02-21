using BurgerKingBackEnd.DAL;
using BurgerKingBackEnd.Entities;
using BurgerKingBackEnd.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BurgerKingBackEnd.Controllers
{
    public class AccountController : Controller
    {

        private readonly BurgerKingDBContext _context;
        private readonly UserManager<CustomUser> _userManager;


        public AccountController(BurgerKingDBContext context,UserManager<CustomUser> userManager)
        {
            _context  = context;
            _userManager = userManager;

        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM account)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            var user = new CustomUser
            {
                Email = account.Email,
                UserName = account.Username
            };

           IdentityResult result =  await _userManager.CreateAsync(user, account.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
