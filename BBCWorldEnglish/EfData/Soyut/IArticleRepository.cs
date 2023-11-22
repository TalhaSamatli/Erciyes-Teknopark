using blogApp.Data;

namespace blogApp.EfData.Soyut
{
    public interface IArticleRepository
    {
    IQueryable<Article> Articles {get;}
    void CreateArticle(Article article);
    }
}