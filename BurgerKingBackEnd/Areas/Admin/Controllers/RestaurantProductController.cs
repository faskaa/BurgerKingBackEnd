using BurgerKingBackEnd.Areas.Admin.ViewModels;
using BurgerKingBackEnd.DAL;
using BurgerKingBackEnd.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BurgerKingBackEnd.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RestaurantProductController : Controller
    {

        private readonly BurgerKingDBContext _context;

        public RestaurantProductController(BurgerKingDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Admin Panel - Burger King";
            List<RestaurantProduct> restaurantProduct = _context.RestaurantProduct.ToList();
            List<Restaurant> restaurant = _context.Restaurant.ToList();
            List<Product> product = _context.Product.ToList();

            PRIndexVM pRIndexVM = new PRIndexVM
            {
                restaurantProduct = restaurantProduct,
                product = product,
                restaurant = restaurant

            };
            return View(pRIndexVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Title = "Admin Panel - Burger King";

            List<Restaurant> restaurant = _context.Restaurant.ToList();
            List<Product> product = _context.Product.ToList();

            PRCreateVM pRCreateVM = new PRCreateVM
            {
                restaurant = restaurant,
                product = product,
            };
            return View(pRCreateVM);
        }

        [HttpPost]
        public IActionResult Create(PRCreateVM pr)
        {
            ViewBag.Title = "Admin Panel - Burger King";
            List<Restaurant> prrestaurant = _context.Restaurant.ToList();
            List<Product> prproduct = _context.Product.ToList();
            PRCreateVM pRCreateVM = new PRCreateVM
            {
                restaurant = prrestaurant,
                product = prproduct,
            };
            if (pr is null) return BadRequest();
            if (!ModelState.IsValid) return View(pRCreateVM);

            if(pr.ProductId== 0)
            {
                ModelState.AddModelError(string.Empty, "You cannot write 0 to ID");
                return  View(pRCreateVM);
            }

            if (pr.RestaurantId == 0)
            {
                ModelState.AddModelError(string.Empty, "You cannot write 0 to ID");
                return View(pRCreateVM);
            }


            Product product = _context.Product.FirstOrDefault(x => x.Id == pr.ProductId)!;
            if (product is null)
            {
                ModelState.AddModelError(string.Empty, "The Product could not be found in this ID");
                return View(pRCreateVM);
            }

            Restaurant restaurant = _context.Restaurant.FirstOrDefault(x => x.Id == pr.RestaurantId)!;
            if (restaurant is null)
            {
                ModelState.AddModelError(string.Empty, "The Restaurant could not be found in this ID");
                return View(pRCreateVM);
            }


            RestaurantProduct newPR = new RestaurantProduct
            {
                RestaurantId = pr.RestaurantId,
                ProductId = pr.ProductId,
                Count = pr.Count
            };

            _context.RestaurantProduct.Add(newPR);
            _context.SaveChanges();



            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.Title = "Admin Panel - Burger King";
            List<Restaurant> prRestaurant = _context.Restaurant.ToList();
            List<Product> prProduct = _context.Product.ToList();
            if (id == 0) return BadRequest();
            PRUpdateVM product = _context.RestaurantProduct.Select(x=>new PRUpdateVM
            {
                Id = x.Id,
                RestaurantId=x.RestaurantId,
                ProductId=x.ProductId,
                Count = x.Count,
                product = prProduct,
                restaurant = prRestaurant,
                
            }).FirstOrDefault(x=>x.Id == id)!;
            if (product is null) return NotFound();

            return View(product);
        }

        [HttpPost]
        public IActionResult Update(int id, PRUpdateVM pr)
        {
            ViewBag.Title = "Admin Panel - Burger King";
            List<Restaurant> pr2Restaurant = _context.Restaurant.ToList();
            List<Product> pr2Product = _context.Product.ToList();
            if (id == 0) return BadRequest();
            PRUpdateVM product = _context.RestaurantProduct.Select(x => new PRUpdateVM
            {
                Id = x.Id,
                RestaurantId = x.RestaurantId,
                ProductId = x.ProductId,
                Count = x.Count,
                product = pr2Product,
                restaurant = pr2Restaurant,
            }).FirstOrDefault(x => x.Id == id)!;
            if (product is null) return NotFound();
            if (!ModelState.IsValid) return View(product);


            if (pr.ProductId == 0)
            {
                ModelState.AddModelError(string.Empty, "You cannot write 0 to ID");
                return View(product);
            }

            if (pr.RestaurantId == 0)
            {
                ModelState.AddModelError(string.Empty, "You cannot write 0 to ID");
                return View(product);
            }


            Product prProduct = _context.Product.FirstOrDefault(x => x.Id == pr.ProductId)!;
            if (prProduct is null)
            {
                ModelState.AddModelError(string.Empty, "The Product could not be found in this ID");
                return View(product);
            }

            Restaurant prRestaurant = _context.Restaurant.FirstOrDefault(x => x.Id == pr.RestaurantId)!;
            if (prRestaurant is null)
            {
                ModelState.AddModelError(string.Empty, "The Restaurant could not be found in this ID");
                return View(product);
            }

            RestaurantProduct oldPR = _context.RestaurantProduct.FirstOrDefault(x => x.Id == id)!;
            oldPR.RestaurantId = pr.RestaurantId;
            oldPR.ProductId = pr.ProductId;
            oldPR.Count = pr.Count;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Detail(int id)
        {
            ViewBag.Title = "Admin Panel - Burger King";
            if (id == 0) return BadRequest();
            RestaurantProduct restaurantProduct = _context.RestaurantProduct.FirstOrDefault(x => x.Id == id)!;
            Restaurant restaurant = _context.Restaurant.FirstOrDefault(x=>x.Id == restaurantProduct.RestaurantId);
            Product product = _context.Product.FirstOrDefault(x=>x.Id==restaurantProduct.ProductId);
            if (restaurant == null) return NotFound();
            PRDetailVM pRIndexVM = new PRDetailVM
            {
                restaurantProduct = restaurantProduct,
                product = product,
                restaurant = restaurant

            };
            return View(pRIndexVM);

        }

        public IActionResult Delete(int id)
        {
            ViewBag.Title = "Admin Panel - Burger King";
            if (id == 0) return BadRequest();
            RestaurantProduct restaurantProduct = _context.RestaurantProduct.FirstOrDefault(a => a.Id == id)!;
            if (restaurantProduct == null) return NotFound();
            _context.RestaurantProduct.Remove(restaurantProduct);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
