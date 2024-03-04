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

        //new
        public IActionResult DeliveryIndex(int id)
        {
            ViewBag.js = "#";
            ViewBag.title = "Menu - Burger King";

            if (id == 0) return BadRequest();
            MenuVM menuVM = new MenuVM
            {
                RestaurantProducts = _context.RestaurantProduct.Where(x => x.RestaurantId == id).Where(x => x.Count > 0).Include(x => x.Product).ToList(),
                Restaurants = _context.Restaurant.FirstOrDefault(x => x.Id == id)!
            };

            if (menuVM is null) return NotFound();

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

        //new
        public IActionResult DeliveryDetail(int RestaurantId, int ProductId)
        {
            ViewBag.js = "Detail/detail.js";
            ViewBag.title = "Detail - Burger King";

            MenuVM model = new MenuVM
            {

                Product = _context.RestaurantProduct.Include(x => x.Product).Include(x => x.Restaurant).Where(x => x.RestaurantId == RestaurantId).FirstOrDefault(x => x.ProductId == ProductId).Product,
                Restaurants = _context.Restaurant.FirstOrDefault(x => x.Id == RestaurantId),
                ProductStockQuantity = _context.RestaurantProduct.Where(x => x.RestaurantId == RestaurantId).FirstOrDefault(x => x.ProductId == ProductId).Count,
                ProductQuantity = 1

            };


            return View(model);


        }



        [HttpPost]  
        public async Task<IActionResult> AddOrder(int restaurantId, int productId, int quantity , string size )
        {
            CustomUser user = await _userManager.GetUserAsync(User);
            List<CardItem> items = _context.CardItems.Where(x => x.UserId == user.Id).Where(x => x.RestaurantId == restaurantId).Where(x => x.OrderType == true).ToList();
            Product product = _context.RestaurantProduct.Include(x=>x.Product).Include(x=>x.Restaurant).Where(x=>x.RestaurantId== restaurantId).FirstOrDefault(x=>x.ProductId==productId)!.Product;
            if (product is null) return BadRequest();
            var currentQuantity = _context.RestaurantProduct.Where(x => x.RestaurantId == restaurantId).FirstOrDefault(x=>x.ProductId== productId)!.Count;
            List<CardItem> item = _context.CardItems.Where(x => x.UserId == user.Id).Where(x=>x.IsSubmited==false).Where(x => x.RestaurantId == restaurantId).Where(x => x.OrderType == true).Where(x => x.ProductId == productId).ToList();

            var itemsQuantitySum = item.Sum(x => x.Quantity);
            var resultQuantity = quantity + itemsQuantitySum;



            if (quantity > currentQuantity)
            {
                TempData["ErrorMessage"] = "The amount you have entered is more than the available amount..";
                return RedirectToAction("Detail", new { RestaurantId = restaurantId, ProductId = productId });
            }
            if (resultQuantity > currentQuantity)
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
                    Quantity = quantity,
                    OrderType = true
                    
                };


                _context.Add(cardItem);
                _context.SaveChanges();

                user.SelectedRestaurantId = restaurantId;
                _context.SaveChanges();

               return RedirectToAction("PickUp" , "Card");
            }


        }




        [HttpPost]
        public async Task<IActionResult> DeliveryAddOrder(int restaurantId, int productId, int quantity, string size)
        {


            CustomUser user = await _userManager.GetUserAsync(User);
            List<CardItem> items = _context.CardItems.Where(x => x.UserId == user.Id).Where(x => x.RestaurantId == restaurantId).Where(x => x.OrderType == false).ToList();
            Product product = _context.RestaurantProduct.Include(x => x.Product).Include(x => x.Restaurant).Where(x => x.RestaurantId == restaurantId).FirstOrDefault(x => x.ProductId == productId)!.Product;
            if (product is null) return BadRequest();
            var currentQuantity = _context.RestaurantProduct.Where(x => x.RestaurantId == restaurantId).FirstOrDefault(x => x.ProductId == productId)!.Count;
            List<CardItem> item = _context.CardItems.Where(x => x.UserId == user.Id).Where(x => x.RestaurantId == restaurantId).Where(x=>x.IsSubmited==false).Where(x => x.OrderType == false).Where(x => x.ProductId == productId).ToList();
            var itemsQuantitySum = item.Sum(x => x.Quantity);
            var resultQuantity = quantity + itemsQuantitySum;
            if (quantity > currentQuantity)
            {
                TempData["ErrorMessage"] = "The amount you have entered is more than the available amount..";
                return RedirectToAction("DeliveryDetail", new { RestaurantId = restaurantId, ProductId = productId });
            }
            if (resultQuantity > currentQuantity)
            {
                TempData["ErrorMessage"] = "The amount you have entered is more than the available amount..";
                return RedirectToAction("DeliveryDetail", new { RestaurantId = restaurantId, ProductId = productId });
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
                    Quantity = quantity,
                    OrderType = false


                };

                _context.Add(cardItem);
                _context.SaveChanges();

                user.SelectedRestaurantId = restaurantId;
                _context.SaveChanges();

                return RedirectToAction("Delivery", "Card");
            }
        }




        public async Task<IActionResult> DeleteUserOrders()
        {
            CustomUser user = await _userManager.GetUserAsync(User);
            List<CardItem> items = _context.CardItems.Where(x=>x.UserId == user.Id).ToList();
 
            _context.CardItems.RemoveRange(items);
            await _context.SaveChangesAsync();


            return RedirectToAction("PickUp", "Location");
        }

        public async Task<IActionResult> DeleteOrderType()
        {
            CustomUser user = await _userManager.GetUserAsync(User);
            List<CardItem> items = _context.CardItems.Where(x => x.UserId == user.Id).ToList();

            _context.CardItems.RemoveRange(items);
            await _context.SaveChangesAsync();


            return RedirectToAction("PickUp", "Delivery");
        }

        public async Task<IActionResult> DeleteCardItem()
        {
            CustomUser user = await _userManager.GetUserAsync(User);
            List<CardItem> items = _context.CardItems.Where(x => x.UserId == user.Id).ToList();
           
            _context.CardItems.RemoveRange(items);
            await _context.SaveChangesAsync();

            return RedirectToAction("PickUp", "Card");
        }

        public async Task<IActionResult> DeletePickupCardItem()
        {
            CustomUser user = await _userManager.GetUserAsync(User);
            List<CardItem> items = _context.CardItems.Where(x => x.UserId == user.Id).Where(x=>x.OrderType == true).ToList();

            _context.CardItems.RemoveRange(items);
            await _context.SaveChangesAsync();

            return RedirectToAction("PickUp", "Card");
        }

        public async Task<IActionResult> DeleteDeliveryCardItem()
        {
            CustomUser user = await _userManager.GetUserAsync(User);
            List<CardItem> items = _context.CardItems.Where(x => x.UserId == user.Id).Where(x => x.OrderType == false).ToList();

            _context.CardItems.RemoveRange(items);
            await _context.SaveChangesAsync();

            return RedirectToAction("Delivery", "Card");
        }




        //[HttpPost]
        //public async Task<IActionResult> DeliveryAddOrder(int restaurantId, int productId, int quantity, string size)
        //{


        //    CustomUser user = await _userManager.GetUserAsync(User);
        //    List<CardItem> items = _context.CardItems.Where(x => x.UserId == user.Id).Where(x => x.RestaurantId == restaurantId).Where(x => x.OrderType == false).ToList();
        //    Product product = _context.RestaurantProduct.Include(x => x.Product).Include(x => x.Restaurant).Where(x => x.RestaurantId == restaurantId).FirstOrDefault(x => x.ProductId == productId)!.Product;
        //    if (product is null) return BadRequest();
        //    var currentQuantity = _context.RestaurantProduct.Where(x => x.RestaurantId == restaurantId).FirstOrDefault(x => x.ProductId == productId)!.Count;
        //    var itemsQuantitySum = items.Sum(x => x.Quantity);
        //    var resultQuantity = quantity + itemsQuantitySum;
        //    if (quantity > currentQuantity)
        //    {
        //        TempData["ErrorMessage"] = "The amount you have entered is more than the available amount..";
        //        return RedirectToAction("DeliveryDetail", new { RestaurantId = restaurantId, ProductId = productId });
        //    }
        //    if (resultQuantity > currentQuantity)
        //    {
        //        TempData["ErrorMessage"] = "The amount you have entered is more than the available amount..";
        //        return RedirectToAction("DeliveryDetail", new { RestaurantId = restaurantId, ProductId = productId });
        //    }
        //    {
        //        CardItem cardItem = new CardItem
        //        {
        //            UserId = user.Id,
        //            ProductId = product.Id,
        //            RestaurantId = restaurantId,
        //            Title = product.Title,
        //            Description = product.Description,
        //            Price = product.Price * quantity,
        //            Size = size,
        //            Quantity = quantity,
        //            OrderType = false


        //        };

        //        _context.Add(cardItem);
        //        _context.SaveChanges();

        //        user.SelectedRestaurantId = restaurantId;
        //        _context.SaveChanges();

        //        return RedirectToAction("Delivery", "Card");
        //    }
        //}

    }
}
