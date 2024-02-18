using BurgerKingBackEnd.DAL;
using BurgerKingBackEnd.Entities;
using BurgerKingBackEnd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BurgerKingBackEnd.Controllers
{
    public class MenuController : Controller
    {
        private readonly BurgerKingDBContext _context;

        public MenuController(BurgerKingDBContext context)
        {
          _context = context;
        }

        public IActionResult Index(int id)
        {
            ViewBag.js = "#";
            ViewBag.title = "Menu - Burger King";

            if (id == 0) return BadRequest();
            MenuVM menuVM = new MenuVM
            {
                RestaurantProducts = _context.RestaurantProduct.Where(x => x.RestaurantId == id).Include(x => x.Product).ToList(),
                Restaurants = _context.Restaurant.FirstOrDefault(x=>x.Id == id)!
            };

            if(menuVM is null) return NotFound();

            return View(menuVM);
        }

        public IActionResult Detail(int id)
        {
            ViewBag.js = "Detail/detail.js";
            ViewBag.title = "Detail - Burger King";


            List<RestaurantProduct> products = _context.RestaurantProduct.Where(x=>x.ProductId==id).Include(x => x.Product).ToList();   

            return View(products);


        }
    }
}
