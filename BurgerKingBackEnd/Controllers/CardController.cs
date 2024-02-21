using BurgerKingBackEnd.DAL;
using BurgerKingBackEnd.Entities;
using BurgerKingBackEnd.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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


        public IActionResult Index(CardVM card)
        {
           var cardItems =  _context.CardItems.Where(x=>x.UserId == card.customUser.Id).ToList();

            return Json(cardItems);
        }
    }
}
