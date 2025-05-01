using HelpMeMate.Data;
using Microsoft.AspNetCore.Mvc;
using HelpMeMate.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpMeMate.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);

                if (user != null)
                {      
                    HttpContext.Session.SetString("Username", user.Username);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Geçersiz giriş denemesi");
            }
            return View(model);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
