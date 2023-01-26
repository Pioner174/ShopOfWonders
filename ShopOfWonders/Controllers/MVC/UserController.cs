using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOW.ShopOfWonders.Model;

namespace SOW.ShopOfWonders.Controllers.MVC
{
    public class UserController : Controller
    {
        private IdentityContext _context;

        public UserController(IdentityContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }
    }
}
