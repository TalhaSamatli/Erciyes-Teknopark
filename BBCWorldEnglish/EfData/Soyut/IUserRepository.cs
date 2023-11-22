using blogApp.Data;

namespace blogApp.EfData.Soyut
{
    public interface IUserRepository
    {
    IQueryable<User> Users {get;}
    }
}