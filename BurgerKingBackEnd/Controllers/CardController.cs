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
            _userManager = userManager;;
        }


        public async Task<IActionResult> PickUp()
        {
            //var cardItems =  _context.CardItems.ToList();
            //CardVM cardVM = new CardVM
            //{
            //    cardItems = _context.CardItems.ToList(),
            //    customUser = await _userManager.GetUserAsync(User)
            //};

            //var user = await _userManager.GetUserAsync(User);
            //CardVM cardVM = new CardVM
            //{
            //    cardItems = _context.CardItems.Contains(X)
            //}

            var customUser = await _userManager.GetUserAsync(User);


            CardVM cardVM = new CardVM
            {
                cardItems = _context.CardItems.Where(x=>x.UserId == customUser.Id ).ToList(),
            };

            return View(cardVM);
        }
    }
}
