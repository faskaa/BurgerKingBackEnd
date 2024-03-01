using BurgerKingBackEnd.DAL;
using BurgerKingBackEnd.Entities;
using BurgerKingBackEnd.Helpers;
using BurgerKingBackEnd.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BurgerKingBackEnd.Controllers
{
    public class BurgerMenuController : Controller
    {

        private readonly BurgerKingDBContext _context;
        private readonly UserManager<CustomUser> _userManager;


        public BurgerMenuController(BurgerKingDBContext context, UserManager<CustomUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        public async Task<IActionResult> Index()
        {
            CustomUser user = await _userManager.GetUserAsync(User);
            //IList<string> userRole = await _userManager.GetRolesAsync(user);
            if (user == null)
            {
                BurgerMenuVM burgerMenuVM = new BurgerMenuVM
                {
                    CustomUser = user,
                    IsCourier = false,
                };
                return View(burgerMenuVM);
            }
            else
            {
              bool userRole = await _userManager.IsInRoleAsync(user, Roles.Courier.ToString());
                BurgerMenuVM burgerMenuVM = new BurgerMenuVM
                {
                    CustomUser = user,
                    IsCourier = userRole,
                };

              return View(burgerMenuVM);

            }
        }
    }
}
