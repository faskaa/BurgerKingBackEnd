using BurgerKingBackEnd.Areas.Admin.ViewModels;
using BurgerKingBackEnd.DAL;
using BurgerKingBackEnd.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BurgerKingBackEnd.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashBoardController : Controller
    {

        private readonly BurgerKingDBContext _context;
        private readonly UserManager<CustomUser> _userManager;


        public DashBoardController(BurgerKingDBContext context, UserManager<CustomUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            ViewBag.Title = "Admin Panel - Burger King";

             List<CustomUser> users = _context.CustomUsers.ToList();
            DashboardVM dashboardVM = new DashboardVM()
            {
                Users = users,

            };
            return View(dashboardVM);
        }
    }
}
