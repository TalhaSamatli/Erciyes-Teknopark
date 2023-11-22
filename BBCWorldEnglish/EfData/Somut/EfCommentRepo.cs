using blogApp.EfData.Soyut;
using blogApp.Data;

namespace blogApp.EfData.Somut
{
    public class EfCommentRepo : ICommentRepository
    {
    private DataContext _context;
    public EfCommentRepo(DataContext context){
        _context = context;
    }
    public IQueryable<Comment> Comments => _context.Comments;
    public void CreateComment(Comment Comment)
    {
        _context.Comments.Add(Comment);
        _context.SaveChanges();
    }
    }
}