using BurgerKingBackEnd.DAL;
using BurgerKingBackEnd.Entities;
using BurgerKingBackEnd.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
//using System.Security.Cryptography.Xml;

namespace BurgerKingBackEnd.Controllers
{
    public class MenuController : Controller
    {
        private readonly BurgerKingDBContext _context;
        private readonly UserManager<CustomUser> _userManager;


        public MenuController(BurgerKingDBContext context , UserManager<CustomUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index(int id)
        {
            ViewBag.js = "#";
            ViewBag.title = "Menu - Burger King";

            if (id == 0) return BadRequest();
            MenuVM menuVM = new MenuVM
            {
                RestaurantProducts = _context.RestaurantProduct.Where(x => x.RestaurantId == id).Where(x=>x.Count>0).Include(x => x.Product).ToList(),
                Restaurants = _context.Restaurant.FirstOrDefault(x=>x.Id == id)!
            };

            if(menuVM is null) return NotFound();

            return View(menuVM);
        }

        public IActionResult Detail(int RestaurantId , int ProductId )
        {
            ViewBag.js = "Detail/detail.js";
            ViewBag.title = "Detail - Burger King";

            MenuVM model = new MenuVM
            {

                Product = _context.RestaurantProduct.Include(x=>x.Product).Include(x=>x.Restaurant).Where(x=>x.RestaurantId == RestaurantId).FirstOrDefault(x=>x.ProductId == ProductId).Product,
                Restaurants= _context.Restaurant.FirstOrDefault(x=>x.Id == RestaurantId),
                ProductStockQuantity = _context.RestaurantProduct.Where(x=>x.RestaurantId == RestaurantId).FirstOrDefault(x=>x.ProductId == ProductId).Count,
                ProductQuantity = 1
                
            };
                

            return View(model);


        }



        [HttpPost]  
        public async Task<IActionResult> AddOrder(int restaurantId, int productId, int quantity , string size )
        {
            
          
            Product product = _context.RestaurantProduct.Include(x=>x.Product).Include(x=>x.Restaurant).Where(x=>x.RestaurantId== restaurantId).FirstOrDefault(x=>x.ProductId==productId).Product;
            if (product is null) return BadRequest();
            var currentQuantity = _context.RestaurantProduct.Where(x => x.RestaurantId == restaurantId).FirstOrDefault(x=>x.ProductId== productId).Count;
            CustomUser user = await _userManager.GetUserAsync(User);
            if (quantity > currentQuantity)
            {
                TempData["ErrorMessage"] = "The amount you have entered is more than the available amount..";
                return RedirectToAction("Detail", new { RestaurantId = restaurantId, ProductId = productId });
            }
            {
                CardItem cardItem = new CardItem
                {
                    UserId = user.Id,
                    ProductId = product.Id,
                    RestaurantId = restaurantId,
                    Title = product.Title,
                    Description = product.Description,
                    Price = product.Price * quantity,
                    Size = size,
                    Quantity = quantity
                };
                _context.Add(cardItem);
                _context.SaveChanges();

                user.SelectedRestaurantId = restaurantId;
                _context.SaveChanges();


               return RedirectToAction("PickUp" , "Card");
            }


        }

        public async Task<IActionResult> DeleteUserOrders()
        {
            CustomUser user = await _userManager.GetUserAsync(User);
            List<CardItem> items = _context.CardItems.Where(x=>x.UserId == user.Id).ToList();
            //foreach (var item in items)
            //{
            //    _context.Remove(item);

            //     _context.SaveChanges();

            //}
            _context.CardItems.RemoveRange(items);
            await _context.SaveChangesAsync();


            return RedirectToAction("PickUp", "Location");
        }

    }
}
