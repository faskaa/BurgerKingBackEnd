using BurgerKingBackEnd.DAL;
using BurgerKingBackEnd.Entities;
using BurgerKingBackEnd.Helpers;
using BurgerKingBackEnd.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BurgerKingBackEnd.Controllers
{
    public class AccountController : Controller
    {

        private readonly BurgerKingDBContext _context;
        private readonly UserManager<CustomUser> _userManager;
        private readonly SignInManager<CustomUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountController(BurgerKingDBContext context,UserManager<CustomUser> userManager , SignInManager<CustomUser> signInManager ,RoleManager<IdentityRole> roleManager)
        {
            _context  = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async  Task<IActionResult> Index()
        {

            CustomUser user = await _userManager.GetUserAsync(User);
            if (user is null) return BadRequest();

            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View();
            CustomUser user = await _userManager.FindByNameAsync(login.Username);

            if(user is null)
            {
                ModelState.AddModelError(string.Empty, "Username or password is incorrect");
                return View();
            }

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, login.Password, true, true);
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "Your account has been blocked for 1 minutes for excessive experimentation");
                    return View();
                }
                
                ModelState.AddModelError(string.Empty, "Username or password is incorrect");
                return View();
            }
            return RedirectToAction("Index", "Home"); 
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM account)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            var user = new CustomUser
            {
                Email = account.Email,
                UserName = account.Username
            };

           IdentityResult result =  await _userManager.CreateAsync(user, account.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View();
            }

            await _userManager.AddToRoleAsync(user , Roles.Member.ToString());
            return RedirectToAction("Index", "Home");
        }

        public async  Task<IActionResult> SignOut()
        {
            //CustomUser user = await _userManager.GetUserAsync(User);
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }



        public async Task<IActionResult> Delete()
        {
            CustomUser user = await _userManager.GetUserAsync(User);
            if (user == null) return BadRequest();
            await _signInManager.SignOutAsync();
            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index", "Home");
        }


        //public JsonResult ShowAuthentification()
        //{
        //    return Json(User.Identity.IsAuthenticated);
        //}


    }
}
