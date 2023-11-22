using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace blogApp.Data
{
    public class User
    {
         [Key]
        public int UserID { get; set; }

        public string? UserName { get; set; }

        public string? UserMail { get; set; }

        public string? UserPassword { get; set; }

        public string? UserRole { get; set; }

        // Kullanıcının yazdığı makaleler için ilişki
        public virtual ICollection<Article> Articles { get; set; }
    }
}