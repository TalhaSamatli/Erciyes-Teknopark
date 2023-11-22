using blogApp.Data;
using blogApp.EfData.Soyut;

namespace blogApp.EfData.Somut
{
    public class EfUserRepo : IUserRepository
    {
    private DataContext _context;
    public EfUserRepo(DataContext context){
        _context = context;
    }
    public IQueryable<User> Users => _context.Users;
    public void CreateUser(User User)
    {
        _context.Users.Add(User);
        _context.SaveChanges();
    }
    }
}