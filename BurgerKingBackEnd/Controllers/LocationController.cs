﻿using BurgerKingBackEnd.DAL;
using BurgerKingBackEnd.Entities;
using BurgerKingBackEnd.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BurgerKingBackEnd.Controllers
{
    public class LocationController : Controller
    {

        private readonly BurgerKingDBContext _context;
        private readonly UserManager<CustomUser> _userManager;


        public LocationController(BurgerKingDBContext context , UserManager<CustomUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        public async Task<IActionResult> PickUp(string search)
        {
            ViewBag.js = "Locations/location.js";
            ViewBag.title = "Locations - Burger King";

            CustomUser user = await _userManager.GetUserAsync(User);
            

            LocationVM model = new LocationVM
            {
                Restaurants = _context.Restaurant.Where(r => r.Title.Contains(search) && r.OpeningTime <= DateTime.Now.TimeOfDay && r.ClosingTime >= DateTime.Now.TimeOfDay).ToList(),
                customUser = user,
            };


            return View(model);
        }

        public IActionResult Delivery(string search)
        {
            ViewBag.js = "Delivery/delivery.js";
            ViewBag.title = "Locations - Burger King";

            LocationVM model = new LocationVM
            {
                Restaurants = _context.Restaurant.Where(r => r.Title.Contains(search) && r.OpeningTime<= DateTime.Now.TimeOfDay && r.ClosingTime >= DateTime.Now.TimeOfDay).ToList(),
            };


            return View(model);
        }



        //public IActionResult PickUp(int id)
        //{
        //    LocationVM model = new LocationVM
        //    {
        //        Restaurants = _context.Restaurant.Include(r => r.RestaurantProduct).ThenInclude(rp => rp.Product).Where(r => r.RestaurantProduct.Any(rp => rp.Count > 0)).ToList(),
        //    };


        //    var model = _context.Restaurant.FirstOrDefault(x => x.Id == id);
        //    return Json(model);
        //}

        //public IActionResult Search(string search)
        //{
        //    LocationVM model = new LocationVM
        //    {
        //        Restaurants = _context.Restaurant.Where(r => r.Title.Contains(search)).ToList(),
        //    };
        //    return Json(model);    
        //}
    }
}
