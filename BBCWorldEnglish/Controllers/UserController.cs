using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using blogApp.Data;
using BlogApp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace blogApp.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext _context;
        public UserController(DataContext context) { _context = context; }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(User model)
        {
            model.UserRole = "Writer";
            _context.Users.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Success");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
Console.WriteLine("İşlem başladı");
            if (ModelState.IsValid)
            {
                var isUser = _context.Users.FirstOrDefault(x => x.UserName == model.UserName && x.UserPassword == model.UserPassword);
                if (isUser != null)
                {

                    var userClaims = new List<Claim>();
                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserID.ToString()));
                    userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName ?? ""));
                    if (isUser.UserName == "sacit")
                    {
                        userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                    }

                    var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Kullınıcı adı veya şifresi hatalı!");
            }
            return View(model);
        }
        public IActionResult Success()
        {
            return View();
        }
    }
}
