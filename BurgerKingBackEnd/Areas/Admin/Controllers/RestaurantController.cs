using BurgerKingBackEnd.Areas.Admin.ViewModels;
using BurgerKingBackEnd.DAL;
using BurgerKingBackEnd.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BurgerKingBackEnd.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RestaurantController : Controller
    {
        private readonly BurgerKingDBContext _context;
        private readonly IWebHostEnvironment _env;

        public RestaurantController(BurgerKingDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            List<Restaurant> restaurant = _context.Restaurant.ToList();
            return View(restaurant);
        }

        public IActionResult Detail(int id)
        {
            if (id == 0) return BadRequest();
            Restaurant restaurant = _context.Restaurant.FirstOrDefault(x => x.Id == id)!;
            if (restaurant == null) return NotFound();
            return View(restaurant);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Create(RestaurantCreateVM restaurant)
        {
            if (restaurant is null) return BadRequest();
            if (!ModelState.IsValid) return View();

            Restaurant newRestaurant = new Restaurant
            {
                Title = restaurant.Title,
                OpeningTime = restaurant.OpeningTime.TimeOfDay,
                ClosingTime = restaurant.ClosingTime.TimeOfDay,
            };

            _context.Restaurant.Add(newRestaurant);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id) 
        {
            if(id==0) return BadRequest();
            RestaurantUpdateVM restaurant = _context.Restaurant.Select(x=>new RestaurantUpdateVM
            {
                Id = x.Id,
                Title = x.Title,
                OpeningTime = x.OpeningTime,
                ClosingTime = x.ClosingTime,
            }).FirstOrDefault(x=>x.Id == id)!;
            if (restaurant == null) return NotFound();
            return View(restaurant);
        
        }

        [HttpPost]
        public IActionResult Update(int id , RestaurantUpdateVM newRestaurant)
        {
            if (id == 0) return BadRequest();
            RestaurantUpdateVM restaurant = _context.Restaurant.Select(x => new RestaurantUpdateVM
            {
                Id = id,
                Title = x.Title,
                OpeningTime = x.OpeningTime,
                ClosingTime = x.ClosingTime,
            }).FirstOrDefault(x => x.Id == id)!;
            if (restaurant == null) return NotFound();
            if(!ModelState.IsValid) return View(restaurant);

            Restaurant oldRestaurant = _context.Restaurant.FirstOrDefault(x=>x.Id == id)!;
            oldRestaurant.Title = newRestaurant.Title;
            oldRestaurant.OpeningTime = newRestaurant.OpeningTime;
            oldRestaurant.ClosingTime = newRestaurant.ClosingTime;
            _context.SaveChanges();

            return RedirectToAction("Index");

        }


        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            Restaurant restaurant = _context.Restaurant.FirstOrDefault(x=>x.Id == id)!;
            if (restaurant == null) return NotFound();

            _context.Restaurant.Remove(restaurant);
            _context.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}
