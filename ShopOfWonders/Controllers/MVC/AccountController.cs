using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOW.DataModels;
using SOW.ShopOfWonders.Models;
using SOW.ShopOfWonders.Models.ViewModels;

namespace SOW.ShopOfWonders.Controllers.MVC
{
    [Route("mvc/account/{action=Index}")]
    public class AccountController: Controller

    {
        //Предоставляет API для управления пользователем в хранилище сохраняемости
        private UserManager<User> _userManger;
        //Предоставляет API-интерфейсы для входа пользователя
        private SignInManager<User> _signInManager;
        //Предоставляет API-интерфейсы для использования БД
        private IdentityContext _context;

        public AccountController(UserManager<User> userManger, SignInManager<User> signInManager, IdentityContext context)
        
        
        {
            _userManger = userManger;
            _signInManager = signInManager;
            _context = context;
        }


        public IActionResult Login(string returnUrl)
        {
            return View(ViewBag.ReturnUrl = returnUrl);
        }


        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([FromForm] RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User() { UserName = viewModel.Login, Email = viewModel.Email };

                IdentityResult result = await _userManger.CreateAsync(user,viewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(viewModel);
        }



        public ActionResult Index()
        {
            return View();
        }

    }
}
