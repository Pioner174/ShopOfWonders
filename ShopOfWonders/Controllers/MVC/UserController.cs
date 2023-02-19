using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOW.ShopOfWonders.Models;

namespace SOW.ShopOfWonders.Controllers.MVC
{
    [Route("mvc/user/")]

    public class UserController : Controller
    {
        private IdentityContext _context;


        public UserController(IdentityContext context)
        {
            _context = context;
        }

        [HttpGet("Index")]
        public async Task<ActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }
    }
}
