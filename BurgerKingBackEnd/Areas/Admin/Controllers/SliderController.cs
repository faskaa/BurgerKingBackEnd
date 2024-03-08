using BurgerKingBackEnd.Areas.Admin.ViewModels;
using BurgerKingBackEnd.DAL;
using BurgerKingBackEnd.Entities;
using BurgerKingBackEnd.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Drawing;

namespace BurgerKingBackEnd.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SliderController : Controller
    {
        private readonly BurgerKingDBContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(BurgerKingDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        public IActionResult Index()
        {
            List<Slider> slider = _context.Sliders.ToList();
            return View(slider);
        }

        public IActionResult Detail(int id)
        { if (id == 0) return BadRequest();
            Slider slider = _context.Sliders.FirstOrDefault(x => x.Id == id)!;
            if (slider == null) return NotFound();
            return View(slider);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SliderCreateVM slider)
        {
            if (slider == null) return BadRequest();
            if (!ModelState.IsValid) return View();
            if (!slider.IImage.IsImage() || !slider.IDeliveryImage.IsImage() || !slider.IOrderImage.IsImage() )
            {
                ModelState.AddModelError(string.Empty, "Please upload valid images.");
                return View();
            }
            if ((double)slider.IImage.Length / 1024 / 1024 > 1 || (double)slider.IOrderImage.Length / 1024 / 1024 > 1 || (double)slider.IDeliveryImage.Length / 1024 / 1024 > 1)
            {
                ModelState.AddModelError(string.Empty, "Please set the image smaller than 1 mb");
                return View();
            };

           
            string webRootPath = _env.WebRootPath;

            slider.Image = await slider.IImage.GeneratePhoto(webRootPath, "Assets", "Icons");
            slider.DeliveryImage = await slider.IDeliveryImage.GeneratePhoto(webRootPath, "Assets", "Icons");
            slider.OrderImage = await slider.IOrderImage.GeneratePhoto(webRootPath, "Assets", "Icons");


            Slider newSlider = new Slider
            {
                Title = slider.Title,
                Image = slider.Image,
                DeliveryImage = slider.DeliveryImage,
                OrderImage = slider.OrderImage
            };

            
            _context.Sliders.Add(newSlider);
            _context.SaveChanges();

            return RedirectToAction("Index", "Slider");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (id == 0) return BadRequest();
            SliderUpdateVM slider = _context.Sliders.Select(x=> new SliderUpdateVM
            {
                Id = x.Id,
                Title = x.Title,
                Image = x.Image,
                DeliveryImage = x.DeliveryImage,
                OrderImage = x.OrderImage

            }).FirstOrDefault(x => x.Id == id)!;
            if (slider == null) return NotFound();
            return View(slider);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, SliderUpdateVM Newslider)
        {
            if (id == 0) return BadRequest();
            SliderUpdateVM slider = _context.Sliders.Select(x => new SliderUpdateVM
            {
                Id = x.Id,
                Title = x.Title,
                Image = x.Image,
                DeliveryImage = x.DeliveryImage,
                OrderImage = x.OrderImage

            }).FirstOrDefault(x => x.Id == id)!;
            if (slider == null) return NotFound();
            if (!ModelState.IsValid) return View(slider);
            if (Newslider.IImage !=null)
            {
                    if (!Newslider.IImage.IsImage())
                    {
                        ModelState.AddModelError(string.Empty, "Please upload valid images.");
                        return View(slider);
                    }
                    if ((double)Newslider.IImage.Length / 1024 / 1024 > 1)
                    {
                        ModelState.AddModelError(string.Empty, "Please set the image smaller than 1 mb");
                        return View(slider);
                    }
            }

            if (Newslider.IOrderImage != null)
            {
                    if (!Newslider.IOrderImage.IsImage())
                    {
                        ModelState.AddModelError(string.Empty, "Please upload valid images.");
                         return View(slider);
                    }
                    if ((double)Newslider.IOrderImage.Length / 1024 / 1024 > 1)
                    {
                        ModelState.AddModelError(string.Empty, "Please set the image smaller than 1 mb");
                        return View(slider);
                    }
            }

            if (Newslider.IDeliveryImage != null)
            {
                    if (!Newslider.IDeliveryImage.IsImage())
                    {
                        ModelState.AddModelError(string.Empty, "Please upload valid images.");
                        return View(slider);
                    }
                    if ((double)Newslider.IDeliveryImage.Length / 1024 / 1024 > 1)
                    {
                        ModelState.AddModelError(string.Empty, "Please set the image smaller than 1 mb");
                        return View(slider);
                    }
            }

            Slider oldSlider = _context.Sliders.FirstOrDefault(x => x.Id == id)!;
            oldSlider.Title = Newslider.Title;
            string webRootPath = _env.WebRootPath;
            if (Newslider.IImage != null)
            {
                Newslider.Image = await Newslider.IImage.GeneratePhoto(webRootPath, "Assets", "Icons");
               oldSlider.Image = Newslider.Image;
            }
            if (Newslider.IOrderImage != null)
            {
                Newslider.OrderImage = await Newslider.IOrderImage.GeneratePhoto(webRootPath, "Assets", "Icons");
                 oldSlider.OrderImage= Newslider.OrderImage;
            }
            if (Newslider.IDeliveryImage != null)
            {
                Newslider.DeliveryImage = await Newslider.IDeliveryImage.GeneratePhoto(webRootPath, "Assets", "Icons");
                 oldSlider.DeliveryImage = Newslider.DeliveryImage;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Slider");
        }

        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            Slider slider = _context.Sliders.FirstOrDefault(x => x.Id == id)!;
            if (slider == null) return NotFound();
            _context.Sliders.Remove(slider);
            _context.SaveChanges();
            return RedirectToAction("Index" , "Slider");
        }
    }
}
