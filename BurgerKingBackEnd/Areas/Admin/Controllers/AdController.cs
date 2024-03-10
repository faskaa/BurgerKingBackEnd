using BurgerKingBackEnd.Areas.Admin.ViewModels;
using BurgerKingBackEnd.DAL;
using BurgerKingBackEnd.Entities;
using BurgerKingBackEnd.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BurgerKingBackEnd.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdController : Controller
    {

        private readonly BurgerKingDBContext _context;
        private readonly IWebHostEnvironment _env;

        public AdController(BurgerKingDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        public IActionResult Index()
        {
            ViewBag.Title = "Admin Panel - Burger King";
            List<Ad> ad= _context.Ads.ToList();
            return View(ad);
        }

        public IActionResult Detail(int id)
        {
            ViewBag.Title = "Admin Panel - Burger King";
            if (id == 0) return BadRequest();
            Ad ad = _context.Ads.FirstOrDefault(x=>x.Id == id)!;
            if (ad == null) return NotFound();
            return View(ad);
        }


        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Title = "Admin Panel - Burger King";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdCreateVM ad)
        {
            ViewBag.Title = "Admin Panel - Burger King";
            if (ad is null) return BadRequest();

            if (!ModelState.IsValid) return View();

            if (!ad.IImage.IsImage())
            {
                ModelState.AddModelError(string.Empty, "Please upload valid images.");
                return View();
            }

            if ((double)ad.IImage.Length / 1024 / 1024 > 1 )
            {
                ModelState.AddModelError(string.Empty, "Please set the image smaller than 1 mb");
                return View();
            };

            string webRootPath = _env.WebRootPath;

            ad.Image = await ad.IImage.GeneratePhoto(webRootPath, "Assets", "Images");

            Ad newAd = new Ad
            {
                Title = ad.Title,
                Description = ad.Description,
                Image = ad.Image,
            };
            _context.Ads.Add(newAd);
            _context.SaveChanges();

            return RedirectToAction("Index", "Ad");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.Title = "Admin Panel - Burger King";
            if (id == 0) return BadRequest();
            AdUpdateVM ad = _context.Ads.Select(x=>new AdUpdateVM
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Image = x.Image,
            }).FirstOrDefault(a => a.Id == id)!;
            if (ad == null) return NotFound();
            return View(ad);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id , AdUpdateVM newAd)
        {
            ViewBag.Title = "Admin Panel - Burger King";
            if (id == 0) return BadRequest();
            AdUpdateVM ad = _context.Ads.Select(x=> new AdUpdateVM
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Image = x.Image,

            }).FirstOrDefault(a => a.Id == id)!;
            if (ad == null) return NotFound();
            if (!ModelState.IsValid) return View(ad);

            if (newAd.IImage != null)
            {
                if (!newAd.IImage.IsImage())
                {
                    ModelState.AddModelError(string.Empty, "Please upload valid images.");
                    return View(ad);
                }
                if ((double)newAd.IImage.Length / 1024 / 1024 > 1)
                {
                    ModelState.AddModelError(string.Empty, "Please set the image smaller than 1 mb");
                    return View(ad);
                }
            }

            Ad oldAd = _context.Ads.FirstOrDefault(x => x.Id == id)!;

            string webRootPath = _env.WebRootPath;
            oldAd.Title = newAd.Title;
            if (newAd.IImage != null)
            {
                newAd.Image = await newAd.IImage.GeneratePhoto(webRootPath, "Assets", "Images");
                oldAd.Image = newAd.Image;
            }
            oldAd.Description = newAd.Description;
            
            _context.SaveChanges();

            return RedirectToAction("Index", "Ad");
        }


        public IActionResult Delete(int id)
        {
            ViewBag.Title = "Admin Panel - Burger King";
            if (id==0) return BadRequest();
            Ad ad = _context.Ads.FirstOrDefault(a => a.Id == id)!;
            if (ad == null) return NotFound();
            _context.Ads.Remove(ad);
            _context.SaveChanges();
            return RedirectToAction("Index", "Ad");
        }
    }
}
