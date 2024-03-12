using BurgerKingBackEnd.DAL;
using BurgerKingBackEnd.Entities;
using BurgerKingBackEnd.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BurgerKingBackEnd.Controllers
{
    public class CardController : Controller
    {

        private readonly BurgerKingDBContext _context;
        private readonly UserManager<CustomUser> _userManager;



        public CardController(BurgerKingDBContext context, UserManager<CustomUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> PickUp()
        {
            ViewBag.Title = "Card - Burger King";

            var customUser = await _userManager.GetUserAsync(User);
           
            int restaurantId= _context.Restaurant.FirstOrDefault(r=>r.Id== customUser.SelectedRestaurantId).Id;                                                     //new
            List<CardItem> cardItem = _context.CardItems.Where(x => x.UserId == customUser.Id).Where(x=>x.RestaurantId== customUser.SelectedRestaurantId).Where(x=>x.OrderType == true).Where(x => x.IsSubmited == false).ToList();
            List<CardItem> uniqueCardItems = cardItem.DistinctBy(x => x.Title).Select(
                uniqueItem => new CardItem
                {
                    Id = uniqueItem.Id,
                    ProductId = uniqueItem.ProductId,
                    Description = uniqueItem.Description,
                    UserId = customUser.Id,
                    Size = uniqueItem.Size,
                    Title = uniqueItem.Title,
                    Quantity = cardItem.Where(x => x.Title == uniqueItem.Title).Sum(x => x.Quantity),
                    Price = cardItem.Where(x => x.Title == uniqueItem.Title).Sum(x => x.Price),
                    RestaurantId = uniqueItem.RestaurantId,
                    OrderType = uniqueItem.OrderType,
                    IsSubmited = uniqueItem.IsSubmited,
                    
                }).ToList();


            decimal totalPrice = uniqueCardItems.Sum(item => item.Price);

            CardVM cardVM = new CardVM
            {
                cardItems = uniqueCardItems,
                RestaurantId = restaurantId,
                TotalPrice = totalPrice
                
            };

            //if(uniqueCardItems.Count <= 0)
            //{
            //    return RedirectToAction("Index" , "Home");
            //}


            return View(cardVM);
        }

        public async Task<IActionResult> Delivery()
        {
            ViewBag.Title = "Card - Burger King";

            var customUser = await _userManager.GetUserAsync(User);

            int restaurantId = _context.Restaurant.FirstOrDefault(r => r.Id == customUser.SelectedRestaurantId).Id;
            List<CardItem> cardItem = _context.CardItems.Where(x => x.UserId == customUser.Id).Where(x => x.RestaurantId == customUser.SelectedRestaurantId).Where(x => x.OrderType == false).Where(x=>x.IsSubmited==false).ToList();
            List<CardItem> uniqueCardItems = cardItem.DistinctBy(x => x.Title).Select(
                uniqueItem => new CardItem
                {
                    Id = uniqueItem.Id,
                    ProductId = uniqueItem.ProductId,
                    Description = uniqueItem.Description,
                    UserId = customUser.Id,
                    Size = uniqueItem.Size,
                    Title = uniqueItem.Title,
                    Quantity = cardItem.Where(x => x.Title == uniqueItem.Title).Sum(x => x.Quantity),
                    Price = cardItem.Where(x => x.Title == uniqueItem.Title).Sum(x => x.Price),
                    RestaurantId = uniqueItem.RestaurantId,
                    OrderType = uniqueItem.OrderType,
                    IsSubmited = uniqueItem.IsSubmited,


                }).ToList();


            decimal totalPrice = uniqueCardItems.Sum(item => item.Price);

            CardVM cardVM = new CardVM
            {
                cardItems = uniqueCardItems,
                RestaurantId = restaurantId,
                TotalPrice = totalPrice

            };

           

            return View(cardVM);
        }
    }
}
