using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blogApp.Data
{
    public class Article
    {
        [Key]
        public int ArticleID { get; set; }

        public string ArticleTitle { get; set; }

        public string ArticleContent { get; set; }
        public string? ArticleImage { get; set; }

        public DateTime ArticlePublicationTime { get; set; }

        // Makalenin ait olduğu kategori için ilişki
        [ForeignKey("Category")]
        public int ArticleCategoryID { get; set; }
        public virtual Category Category { get; set; }

        // Makalenin yazarı için ilişki
        [ForeignKey("User")]
        public int ArticleWriterID { get; set; }
        public virtual User ArticleWriter { get; set; }

        // Makalenin altındaki yorumlar için ilişki
        public virtual ICollection<Comment> Comments { get; set; }
    }
}