using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SOW.DataModels;
using SOW.ShopOfWonders.Models;
using SOW.ShopOfWonders.Models.ViewModels;

namespace SOW.ShopOfWonders.Controllers.MVC
{
    [Route("mvc/account/{action=Index}")]
    public class AccountController : Controller

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

        [AllowAnonymous]
        public IActionResult SignIn(string returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManger.FindByNameAsync(loginModel.Name);

                if (user != null)
                {

                    if ((await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                    {
                        return Redirect(loginModel?.ReturnUrl ?? "/mvc/account");
                    }
                }
            }
            ModelState.AddModelError("", "Неправильный логин или пароль.");
            return View(loginModel);
        }

        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User() { UserName = viewModel.Login, Email = viewModel.Email };

                IdentityResult result = await _userManger.CreateAsync(user, viewModel.Password);
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
