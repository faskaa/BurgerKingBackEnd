using BurgerKingBackEnd.DAL;
using BurgerKingBackEnd.Entities;
using BurgerKingBackEnd.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BurgerKingBackEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly BurgerKingDBContext _context;

        public HomeController(BurgerKingDBContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            ViewBag.Title = "Home - Burger King";

            HomeVM model = new HomeVM
            {
                Sliders = _context.Sliders.ToList(),
                Ads = _context.Ads.ToList(),
            };
           
            return View(model);
        }
    }
}
