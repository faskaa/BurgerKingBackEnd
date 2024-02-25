using BurgerKingBackEnd.DAL;
using BurgerKingBackEnd.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BurgerKingBackEnd.Controllers
{
    public class OrderController : Controller
    {

        private readonly BurgerKingDBContext _context;
        private readonly UserManager<CustomUser> _userManager;


        public OrderController(BurgerKingDBContext context, UserManager<CustomUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [HttpPost]
        public async Task<IActionResult> Delivery(string unit , string instructions , int phone)
        {
            CustomUser user = await _userManager.GetUserAsync(User);
           List<CardItem> cardItems=  _context.CardItems.Where(x => x.UserId == user.Id).Where(x => x.OrderType == false).ToList();
            Order order = new Order
            {
                Unit = unit,
                DeliveryInstruction = instructions,
                PhoneNumber = phone,
                PickUpType = true,
                CustomUserId = user.Id,
                cardItems = cardItems,
                CustomUser = user
               
                
            };

           await  _context.AddAsync(order);

           await _context.SaveChangesAsync();

            return Json(order);
        }
    }
}
