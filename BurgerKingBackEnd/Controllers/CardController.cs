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


            var customUser = await _userManager.GetUserAsync(User);

            List<CardItem> cardItem = _context.CardItems.Where(x => x.UserId == customUser.Id).ToList();
            List<CardItem> uniqueCardItems = cardItem.DistinctBy(x => x.Title).Select(
                uniqueItem => new CardItem
                {
                    Title = uniqueItem.Title,
                    Quantity = cardItem.Where(x => x.Title == uniqueItem.Title).Sum(x => x.Quantity),
                    Price = cardItem.Where(x => x.Title == uniqueItem.Title).Sum(x => x.Price)
                }).ToList();

            CardVM cardVM = new CardVM
            {
                cardItems = uniqueCardItems
            };



            return View(cardVM);
        }
    }
}
