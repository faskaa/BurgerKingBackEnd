using BurgerKingBackEnd.DAL;
using BurgerKingBackEnd.Entities;
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

            Restaurant restaurant = _context.Restaurant.FirstOrDefault(x => x.Id == user.SelectedRestaurantId);
            BurgerMenuVM burgerMenuVM = new BurgerMenuVM
            {
                CustomUser = user,
                Restaurant = restaurant
            };
            return View(burgerMenuVM);
        }
    }
}
