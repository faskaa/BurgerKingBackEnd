using BurgerKingBackEnd.DAL;
using BurgerKingBackEnd.Entities;
using BurgerKingBackEnd.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

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


        //public async Task<IActionResult> Index()
        //{
        //    CustomUser user = await _userManager.GetUserAsync(User);
        //    List<Order> order = _context.Orders.Where(x => x.CustomUserId == user.Id).Where(x => x.IsSubmited == true).Include(x=>x.cardItems).ToList();

        //    return Vieworder);
        //}

        [HttpPost]
        public async Task<IActionResult> Delivery(string unit , string instructions , int phone)
        {
            CustomUser user = await _userManager.GetUserAsync(User);
           List<CardItem> cardItems=  _context.CardItems.Where(x => x.UserId == user.Id).Where(x => x.OrderType == false).ToList();
            Order order = new Order
            {
                IsSubmited = false,
                Unit = unit,
                DeliveryInstruction = instructions,
                PhoneNumber = phone,
                PickUpType = false,
                CustomUserId = user.Id,
                cardItems = cardItems,
                CustomUser = user,
            };

            
            await _context.AddAsync(order);
            await _context.SaveChangesAsync();

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> PickUp(string option)
        {
            CustomUser user = await _userManager.GetUserAsync(User);
            List<CardItem> cardItems = _context.CardItems.Where(x => x.UserId == user.Id).Where(x => x.OrderType == true).ToList();
            Order order = new Order
            {
                IsSubmited = false,
                PickUpOption = option,
                PickUpType = true,
                CustomUserId = user.Id,
                cardItems = cardItems,
                CustomUser = user,

            };
             

            await _context.AddAsync(order);
            await _context.SaveChangesAsync();

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> DeliveryPayment(string nameOnCard, int creditCardNumber , DateTime expirationDate , int cvv , int zipcode)
        {
            CustomUser user = await _userManager.GetUserAsync(User);
            //Order order = _context.Orders.Where(x=>x.PickUpType == false).Where(x=>x.IsSubmited == true).FirstOrDefault(x => x.CustomUserId == user.Id);
            Order order = _context.Orders.Where(x=>x.CustomUserId == user.Id).Where(x => x.PickUpType == false).FirstOrDefault(x=>x.IsSubmited == false);
            if (order is null)
            {
                return BadRequest();
            }
            order.IsSubmited = true;
            _context.SaveChanges();


            List<Order> carditems = _context.Orders.Where(x => x.CustomUserId == user.Id).Include(x=>x.cardItems).ToList();
            
            List<CardItem> cardItems = _context.CardItems.Where(x => x.UserId == user.Id).Where(x => x.OrderType == false).ToList();
            Card card = new Card
            {
                
                CardName = nameOnCard,
                CardNumber = creditCardNumber,
                CardDate = expirationDate,
                CVV = cvv,
                ZipCode = zipcode,
                OrderId = order.Id,

            };
            await _context.AddAsync(card);
            await _context.SaveChangesAsync();

            List<CardItem> cardItem = _context.CardItems.Where(x=>x.UserId== user.Id).ToList();
            List<RestaurantProduct> product = _context.RestaurantProduct.ToList();
            foreach (var item in cardItem)
            {
                var restaurantProduct = _context.RestaurantProduct.FirstOrDefault(rp => rp.RestaurantId == item.RestaurantId && rp.ProductId == item.ProductId);

                if (restaurantProduct != null)
                {
                    restaurantProduct.Count -= item.Quantity;
                }
                _context.SaveChanges();
            }

            List<Order> submitedOrders = _context.Orders.Include(x=>x.cardItems).Where(x => x.CustomUserId == user.Id).Where(x => x.PickUpType == false).Where(x => x.IsSubmited == true).ToList();
            foreach (var item in submitedOrders)
            {
                foreach (var item1 in item.cardItems)
                {
                    item1.IsSubmited = true;
                    await _context.SaveChangesAsync();

                }
            }
            //_context.RemoveRange(order.cardItems);
            //_context.SaveChanges();

            return RedirectToAction("Index",  "Home");

        }

        public async Task<IActionResult> PickUpPayment(string nameOnCard, int creditCardNumber, DateTime expirationDate, int cvv, int zipcode)
        {
            CustomUser user = await _userManager.GetUserAsync(User);
            //Order order = _context.Orders.Where(x=>x.PickUpType == true).Where(x=>x.IsSubmited==true).FirstOrDefault(x => x.CustomUserId == user.Id);
            Order order = _context.Orders.Where(x => x.CustomUserId == user.Id).Where(x => x.PickUpType == true).FirstOrDefault(x => x.IsSubmited == false)!;
            if (order is null)
            {
                return BadRequest();
            }

            order.IsSubmited = true;
            _context.SaveChanges();

            List<Order> carditems = _context.Orders.Where(x => x.CustomUserId == user.Id).Include(x => x.cardItems).ToList();
            List<CardItem> cardItems = _context.CardItems.Where(x => x.UserId == user.Id).Where(x => x.OrderType == true).ToList();
            Card card = new Card
            {

                CardName = nameOnCard,
                CardNumber = creditCardNumber,
                CardDate = expirationDate,
                CVV = cvv,
                ZipCode = zipcode,
                OrderId = order.Id,

            };

            await _context.AddAsync(card);
            await _context.SaveChangesAsync();

            List<CardItem> cardItem = _context.CardItems.Where(x => x.UserId == user.Id).ToList();
            List<RestaurantProduct> product = _context.RestaurantProduct.ToList();
            foreach (var item in cardItem)
            {
                var restaurantProduct = _context.RestaurantProduct.FirstOrDefault(rp => rp.RestaurantId == item.RestaurantId && rp.ProductId == item.ProductId);

                if (restaurantProduct != null)
                {
                    restaurantProduct.Count -= item.Quantity;
                }
                _context.SaveChanges();
            }


            List<Order> submitedOrders = _context.Orders.Include(x => x.cardItems).Where(x => x.CustomUserId == user.Id).Where(x => x.PickUpType == true).Where(x => x.IsSubmited == true).ToList();
            foreach (var item in submitedOrders)
            {
                foreach (var item1 in item.cardItems)
                {
                    item1.IsSubmited = true;
                    await _context.SaveChangesAsync();

                }
            }
            //_context.RemoveRange(order.cardItems);
            //_context.SaveChanges();

            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> MyOrders()
        {
            

            CustomUser user = await _userManager.GetUserAsync(User);
            List<Order> orders = _context.Orders.Include(x=>x.cardItems).Where(x=>x.CustomUserId == user.Id).Where(x => x.IsSubmited == true).Where(x => x.PickUpType == false).Include(x => x.cardItems).Include(x => x.CustomUser).ToList();
            List<Restaurant> restaurants = _context.Restaurant.ToList();
            if (orders.Count > 0)
            {
                OrderVM orderVM = new OrderVM
                {

                    CustomUser = user,
                    Orders = orders,
                    Restaurant = restaurants,
                };

                
                return View(orderVM);

            }
            else
            {
                return BadRequest() ;
            }
        }
        

        public async Task<IActionResult> Detail(int id)
        {


            CustomUser user = await _userManager.GetUserAsync(User);
            if (id == 0) return BadRequest();
            Order order = _context.Orders.Where(x => x.CustomUserId == user.Id).Where(x => x.IsSubmited == true).Where(x => x.PickUpType == false).Include(x => x.cardItems).Include(x => x.CustomUser).FirstOrDefault(x=>x.Id == id);
            if (order == null ) return NotFound();
            
            List<Restaurant> restaurants = _context.Restaurant.ToList();

            //OrderVM orderVM = new OrderVM
            //{

            //    CustomUser = user,
            //    Orders = orde,
            //    Restaurant = restaurants,
            //};

            return View(order);
        }

        //public async Task<IActionResult> TakeOrder(int id)
        //{


        //    CustomUser user = await _userManager.GetUserAsync(User);
        //    if (id == 0) return BadRequest();
        //    Order order = _context.Orders.Where(x => x.CustomUserId == user.Id).Where(x => x.IsSubmited == true).Where(x => x.PickUpType == false).Include(x => x.cardItems).Include(x => x.CustomUser).FirstOrDefault(x => x.Id == id);
        //    if (order == null) return NotFound();

        //    order.Status = false;
        //    await _context.SaveChangesAsync();
        //    //List<Restaurant> restaurants = _context.Restaurant.ToList();

        //    //OrderVM orderVM = new OrderVM
        //    //{

        //    //    CustomUser = user,
        //    //    Orders = orde,
        //    //    Restaurant = restaurants,
        //    //};

        //    return View(order);
        //}

    }
}
