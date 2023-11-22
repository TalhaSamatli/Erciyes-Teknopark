using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class LoginViewModel{

        [Required]
        [Display(Name = "UserName")]
        public string? UserName {get;set;}

        //[Required]
        [DataType(DataType.Password)]
        [Display(Name = "UserPassword")]
        public string? UserPassword {get; set;}
    }
}