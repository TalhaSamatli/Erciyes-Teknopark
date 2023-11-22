using blogApp.EfData.Soyut;
using blogApp.Data;

namespace blogApp.EfData.Somut
{
    public class EfArticleRepo : IArticleRepository
    {
    private DataContext _context;
    public EfArticleRepo(DataContext context){
        _context = context;
    }
    public IQueryable<Article> Articles => _context.Articles;
    public void CreateArticle(Article article)
    {
        _context.Articles.Add(article);
        _context.SaveChanges();
    }
    }
}