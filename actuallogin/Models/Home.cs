using System.ComponentModel.DataAnnotations;

namespace actuallogin.Models{
    public abstract class BaseEntity{}
    public class User : BaseEntity{
        [Key]
        public int id {get; set;}
        [Required]
        [MinLength(3)]
        public string firstname {get; set;}
        [Required]
        [MinLength(3)]
        public string lastname {get; set;}
        [Required]
        [EmailAddress]
        public string email {get; set;}
        [Required]
        [MinLength(8)]
        [Compare("password2", ErrorMessage = "Passwords do not match.")]
        public string password {get;set;}

        [Required]
        [MinLength(8)]
        public string password2 {get;set;}
    }

}