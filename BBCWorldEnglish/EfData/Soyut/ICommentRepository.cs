using blogApp.Data;

namespace blogApp.EfData.Soyut
{
    public interface ICommentRepository
    {
    IQueryable<Comment> Comments {get;}
    }
}