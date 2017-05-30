using System.ComponentModel.DataAnnotations;

namespace beltReviewer.Models{
    public class LoginViewModel : BaseEntity{
        [Required]
        [EmailAddress]
        public string email {get; set;}

        [Required]
        [DataTypeAttribute(DataType.Password)]
        public string password {get;set;}
        
    }
}