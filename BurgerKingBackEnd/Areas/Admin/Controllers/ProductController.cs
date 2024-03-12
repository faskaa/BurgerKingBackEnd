using BurgerKingBackEnd.Areas.Admin.ViewModels;
using BurgerKingBackEnd.DAL;
using BurgerKingBackEnd.Entities;
using BurgerKingBackEnd.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.AccessControl;

namespace BurgerKingBackEnd.Areas.Admin.Controllers
{
        [Area("Admin")]
        [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly BurgerKingDBContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(BurgerKingDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }

        public IActionResult Index()
        {
            ViewBag.Title = "Admin Panel - Burger King";
            List<Product> products = _context.Product.ToList();
            return View(products);
        }

        public IActionResult Detail(int id)
        {
            ViewBag.Title = "Admin Panel - Burger King";
            if (id == 0) return BadRequest();
            Product product = _context.Product.FirstOrDefault(x=>x.Id==id)!;
            if (product == null) return NotFound();
            return View(product);
        }



        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Title = "Admin Panel - Burger King";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM product)
        {
            ViewBag.Title = "Admin Panel - Burger King";
            if (product == null) return BadRequest();
            if (!ModelState.IsValid) return View();

            if (!product.IImage.IsImage())
            {
                ModelState.AddModelError(string.Empty, "Please upload valid images.");
                return View();
            }

            if ((double)product.IImage.Length / 1024 / 1024 > 1)
            {
                ModelState.AddModelError(string.Empty, "Please set the image smaller than 1 mb");
                return View();
            };

            if (product.Calory < 0)
            {
                ModelState.AddModelError(string.Empty, "You cannot write negative to calory");
                return View();
            }

            if (product.Price <= 0)
            {
                ModelState.AddModelError(string.Empty, "You cannot write negative or zero to price");
                return View();
            }

            string webRootPath = _env.WebRootPath;
            product.Image = await product.IImage.GeneratePhoto(webRootPath,"Assets" ,"Images");

            Product newProduct = new Product
            {
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                Calory = product.Calory,
                Image = product.Image,
            };

            _context.Product.Add(newProduct);
            _context.SaveChanges();
                        
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.Title = "Admin Panel - Burger King";
            if (id == 0) return BadRequest();
            ProductUpdateVM product = _context.Product.Select(x=>new ProductUpdateVM
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Price = x.Price,
                Calory = x.Calory,
                Image = x.Image,
            }).FirstOrDefault(x => x.Id == id)!;
            if(product is null) return NotFound();

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id , ProductUpdateVM newProduct)
        {
            ViewBag.Title = "Admin Panel - Burger King";
            if (id == 0) return BadRequest();
            ProductUpdateVM product = _context.Product.Select(x=>new ProductUpdateVM
            {
                Id=x.Id,
                Title = x.Title,
                Description = x.Description,
                Price = x.Price,
                Calory = x.Calory,
                Image = x.Image,
            }).FirstOrDefault(x=>x.Id==id)!;
            if (product is null) return NotFound();
            if(!ModelState.IsValid) return View(product);
            if (newProduct.Calory < 0)
            {
                ModelState.AddModelError(string.Empty, "You cannot write negative to calory");
                return View(product);
            }

            if (newProduct.Price <= 0)
            {
                ModelState.AddModelError(string.Empty, "You cannot write negative or zero to price");
                return View(product);
            }


            Product oldProduct = _context.Product.FirstOrDefault(x => x.Id == id)!;
            string webRootPath = _env.WebRootPath;
            if(newProduct.IImage != null)
            {
                if (!newProduct.IImage.IsImage())
                {
                    ModelState.AddModelError(string.Empty, "Please upload valid images.");
                    return View(product);
                }
                if ((double)newProduct.IImage.Length / 1024 / 1024 > 1)
                {
                    ModelState.AddModelError(string.Empty, "Please set the image smaller than 1 mb");
                    return View(product);
                }

                newProduct.Image = await newProduct.IImage.GeneratePhoto(webRootPath, "Assets", "Images");
                oldProduct.Image = newProduct.Image;
            }
            oldProduct.Title = newProduct.Title;
            oldProduct.Description = newProduct.Description;
            oldProduct.Price = newProduct.Price;
            oldProduct.Calory = newProduct.Calory;
            _context.SaveChanges();


            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            ViewBag.Title = "Admin Panel - Burger King";
            if (id == 0) return BadRequest();
            Product product = _context.Product.FirstOrDefault(x => x.Id == id)!;
            if (product == null) return NotFound();


            _context.Product.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
