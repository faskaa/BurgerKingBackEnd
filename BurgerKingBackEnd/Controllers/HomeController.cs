using BurgerKingBackEnd.DAL;
using BurgerKingBackEnd.Entities;
using BurgerKingBackEnd.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BurgerKingBackEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly BurgerKingDBContext _context;
        private readonly UserManager<CustomUser> _userManager;


        public HomeController(BurgerKingDBContext context , UserManager<CustomUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        //[HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Home - Burger King";
            var customUser = await _userManager.GetUserAsync(User);
            if (customUser != null)
            {

                HomeVM model = new HomeVM
                {
                    Sliders = _context.Sliders.ToList(),
                    Ads = _context.Ads.ToList(),
                    Downloads = _context.Downloads.ToList(),
                    Settings = _context.Settings.ToList(),
                    CardItemTotalPrice = _context.CardItems.Where(x => x.UserId == customUser.Id).Sum(x => x.Price),
                    CustomUser = _context.CustomUsers.FirstOrDefault(x => x.Id == customUser.Id) ?? null,
                    Restaurant = _context.Restaurant.FirstOrDefault(x=>x.Id == customUser.SelectedRestaurantId)

                };

                return View(model);

            }
            else
            {
                HomeVM model = new HomeVM
                {
                    Sliders = _context.Sliders.ToList(),
                    Ads = _context.Ads.ToList(),
                    Downloads = _context.Downloads.ToList(),
                    Settings = _context.Settings.ToList(),
                    CardItemTotalPrice = 0,
                    CustomUser = null!

                };

                return View(model);

            }

        }

       


    }
}
