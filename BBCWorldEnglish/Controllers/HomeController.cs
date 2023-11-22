using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using blogApp.Models;
using blogApp.Data;
using BlogApp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace blogApp.Controllers;

public class HomeController : Controller
{
    private readonly DataContext _context;
    public HomeController(DataContext context) { _context = context; }

    
    public async Task<IActionResult> Index()
    {
        var articles = await _context.Articles.Include(a => a.Category).Include(a => a.ArticleWriter).ToListAsync();

            
            return View(articles);
    }
    
    
    public async Task<IActionResult> categories()
    {
        var categories = await _context.Categories.ToListAsync();
        return View(categories);
    }

    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        Console.WriteLine("Home Controller Login");        
        if (ModelState.IsValid)
        {
            Console.WriteLine("Model Valid");
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
            else
            {
                ModelState.AddModelError("", "Kullınıcı adı veya şifresi hatalı!");
            }
        }
        else
        {
            Console.WriteLine("Model Hatalı geliyor");
            ModelState.AddModelError("", "Kullınıcı adı veya şifresi hatalı!");
        }
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


        return RedirectToAction("Index", "Home");
    }

    
    public IActionResult Register()
    {
        return RedirectToAction("Create", "User");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

