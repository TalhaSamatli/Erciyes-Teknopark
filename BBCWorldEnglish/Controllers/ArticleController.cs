using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using blogApp.Models;
using blogApp.Data;
using Microsoft.EntityFrameworkCore;


namespace blogApp.Controllers
{
    public class ArticleController : Controller
    {
        private readonly DataContext _context;
        public ArticleController(DataContext context) { _context = context; }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Article model, IFormFile fileInput)
        {
            if (fileInput != null && fileInput.Length > 0)
            {
               
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(fileInput.FileName);

                
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await fileInput.CopyToAsync(fileStream);
                }

                
                model.ArticleImage = fileName;
            }
            
            model.ArticleWriterID = 1;
            model.ArticlePublicationTime = DateTime.Now;
            model.ArticleCategoryID = 1;

            _context.Articles.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Archive");
        }
        
        public async Task<IActionResult> Archive()
        {
            
            var articles = await _context.Articles.Include(a => a.Category).Include(a => a.ArticleWriter).ToListAsync();

            
            return View(articles);
        }
        
        [HttpGet]
        public IActionResult Article(int? id)
        {
            var article = _context.Articles
            .Include(a => a.ArticleWriter)
            .Include(a => a.Category)
            .FirstOrDefault(i => i.ArticleID == id);
            if (article == null)
            {
                Console.WriteLine("Veri bulunamadı");
                return NotFound();
            }

            return View("Article", article);
        }
        public async Task<IActionResult> Category(int id)
        {
            
            var articlesInCategory = await _context.Articles
                .Include(a => a.Category)
                .Include(a => a.ArticleWriter)
                .Where(a => a.ArticleCategoryID == id)
                .ToListAsync();

            
            if (!articlesInCategory.Any())
            {
                return NotFound();
            }

            
            return View(articlesInCategory);
        }
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}