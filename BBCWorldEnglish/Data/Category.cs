using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace blogApp.Data
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        // Kategorinin bağlı olduğu makaleler için ilişki
        public virtual ICollection<Article> Articles { get; set; }
    }
}