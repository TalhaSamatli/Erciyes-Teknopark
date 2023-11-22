using blogApp.Data;

namespace blogApp.EfData.Soyut
{
    public interface ICategoryRepository
    {
    IQueryable<Category> Categories {get;}
    }
}