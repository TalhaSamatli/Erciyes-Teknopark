using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace blogApp.Data
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }

        public string CommentContent { get; set; }

        public DateTime CommentPublicationTime { get; set; }

        // Yorumun sahibi kullanıcı için ilişki
        [ForeignKey("User")]
        public int CommentWriterID { get; set; }
        public virtual User CommentWriter { get; set; }

        // Yorumun ait olduğu makale için ilişki
        [ForeignKey("Article")]
        public int CommentArticleID { get; set; }
        public virtual Article Article { get; set; }
    }
}