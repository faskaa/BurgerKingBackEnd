using BurgerKingBackEnd.DAL;
using BurgerKingBackEnd.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BurgerKingBackEnd.Controllers
{
    public class AboutController : Controller
    {
        private readonly BurgerKingDBContext _context;


        public AboutController(BurgerKingDBContext context, UserManager<CustomUser> userManager, SignInManager<CustomUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "About - Burger King";
            List<Setting> setting = _context.Settings.ToList();
            return View(setting);
        }
    }
}
