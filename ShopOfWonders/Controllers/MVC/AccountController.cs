using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SOW.DataModels;
using SOW.ShopOfWonders.Models;
using SOW.ShopOfWonders.Models.Interfaces;
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


        private IUserConnector _userRepo;

        public AccountController(UserManager<User> userManger, SignInManager<User> signInManager
            , IdentityContext context, IUserConnector userRepo)
        {
            _userManger = userManger;
            _signInManager = signInManager;
            _context = context;
            _userRepo = userRepo;
        }

        [AllowAnonymous]
        public IActionResult LogIn(string returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LoginViewModel loginModel)
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


        [Authorize]
        public async Task<ActionResult> Index()
        {

            if (HttpContext?.User != null)
            {
                User user = await _userManger.GetUserAsync(HttpContext.User);

                return View(await _userRepo.GetUserVMForIDAsync(user!.Id));
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Index(UserViewModel viewModel)
        {

            User user = await _userManger.GetUserAsync(HttpContext.User);

            viewModel.Id = user.Id;

            await _userRepo.UpdateUserForUserVMAsync(viewModel);
            
            return View(await _userRepo.GetUserVMForIDAsync(viewModel.Id)) ;
            
        }


        [Authorize]
        public async Task<RedirectResult> Logout(string returnUrl = "/mvc/account")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

    }
}
