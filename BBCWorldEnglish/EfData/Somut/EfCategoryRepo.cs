using blogApp.EfData.Soyut;
using blogApp.Data;


namespace blogApp.EfData.Somut
{
    public class EfCategoriesRepo : ICategoryRepository
    {
    private DataContext _context;
    public EfCategoriesRepo(DataContext context){
        _context = context;
    }
    public IQueryable<Category> Categories => _context.Categories;
    public void CreateCategori(Category Categori)
    {
        _context.Categories.Add(Categori);
        _context.SaveChanges();
    }
    }
}