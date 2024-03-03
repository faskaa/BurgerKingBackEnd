using BurgerKingBackEnd.DAL;
using BurgerKingBackEnd.Entities;
using BurgerKingBackEnd.Helpers;
using BurgerKingBackEnd.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BurgerKingBackEnd.Controllers
{
    public class DeliveryController : Controller
    {
        private readonly BurgerKingDBContext _context;
        private readonly UserManager<CustomUser> _userManager;



        public DeliveryController(BurgerKingDBContext context, UserManager<CustomUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCourier( string name , int phone , int license , int experience , string vehicle)
        {
            CustomUser user = await _userManager.GetUserAsync(User);

            Courier courier = new Courier
            {
                Name = name,
                PhoneNumber = phone,
                DriverLicenseNumber = license,
                DrivingExperience = experience,
                VehicleType = vehicle,
                CustomUserId = user.Id,
                CustomUser = user

            };

            await  _context.AddAsync(courier);
            await _context.SaveChangesAsync();

            await _userManager.AddToRoleAsync(user,Roles.Courier.ToString() );
            return RedirectToAction("Orders", "Delivery");
        }




        public async Task<IActionResult> Orders()
        {
            CustomUser user = await _userManager.GetUserAsync(User);
            List<Order> orders = _context.Orders.Where(x => x.IsSubmited == true).Where(x => x.PickUpType == false).Include(x=>x.cardItems).Include(x=>x.CustomUser).ToList();
            List<Restaurant> restaurants = _context.Restaurant.ToList();
            
            
            DeliveryVM deliveryVM = new DeliveryVM
            {
                CustomUser = user,
                Orders = orders,
                Restaurant = restaurants,
                
            };

            return View(deliveryVM);
        }


        public async Task<IActionResult> Detail(int id)
        {   
            CustomUser user = await _userManager.GetUserAsync(User);
            Order order = _context.Orders.Where(x=>x.PickUpType==false).Include(x=>x.cardItems).Include(x=>x.CustomUser).Include(x=>x.cardItems).FirstOrDefault(x => x.Id == id);
            if (order == null) return NotFound();
            //Restaurant restaurant = _context.Restaurant.FirstOrDefault(x => x.Id == user.SelectedRestaurantId);

           
                
            
            return View(order);
        }

        public async Task<IActionResult> Delivered(int id)
        {

            CustomUser user = await _userManager.GetUserAsync(User);
            if (id == 0) return BadRequest();
            Order order = _context.Orders.Where(x => x.IsSubmited == true).Where(x => x.PickUpType == false).Include(x => x.cardItems).Include(x => x.CustomUser).FirstOrDefault(x => x.Id == id);
            if (order == null) return NotFound();

            order.Status = false;
            await _context.SaveChangesAsync();

            Courier courier = _context.Courier.FirstOrDefault(x => x.CustomUserId == user.Id);
            courier.IsDelivering = true;
            await _context.SaveChangesAsync();

            courier.DeliveringOrderId = id;
            await _context.SaveChangesAsync();
            //List<Restaurant> restaurants = _context.Restaurant.ToList();

            //OrderVM orderVM = new OrderVM
            //{

            //    CustomUser = user,
            //    Orders = orde,
            //    Restaurant = restaurants,
            //};


            Order Deliveryorder = _context.Orders.Where(x => x.PickUpType == false).Include(x => x.cardItems).Include(x => x.CustomUser).Include(x => x.cardItems).FirstOrDefault(x => x.Id == id);
            if (order == null) return NotFound();

            DeliveryDetailVM deliveryDetailVM = new DeliveryDetailVM
            {
                CustomUser = user,
                Order = Deliveryorder,

            };


            return View(deliveryDetailVM);
        }

        public async Task<IActionResult> EndDelivering(int id)
        {

            CustomUser user = await _userManager.GetUserAsync(User);
            if (id == 0) return BadRequest();
            Order order = _context.Orders.Where(x => x.IsSubmited == true).Where(x => x.PickUpType == false).Include(x => x.cardItems).Include(x => x.CustomUser).FirstOrDefault(x => x.Id == id);
            if (order == null) return NotFound();

            order.Status = true;
            await _context.SaveChangesAsync();


            Courier courier = _context.Courier.FirstOrDefault(x => x.CustomUserId == user.Id);
            courier.IsDelivering = false;
            await _context.SaveChangesAsync();


            return RedirectToAction("Index", "Home");
        }
    }

}
