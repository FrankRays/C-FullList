using System.ComponentModel.DataAnnotations;

namespace beltReviewer.Models {
    public class RegisterViewModel : BaseEntity {
        [Required]
        [MinLength(2)]
        public string username {get;set;}
        [Required]
        [MinLengthAttribute(8)]
        [DataTypeAttribute(DataType.Password)]
        public string password {get;set;}
        [Required]
        [EmailAddress]
        public string email {get;set;}
        [Compare("password", ErrorMessage = "Password and confirmation must match.")]
        [DataTypeAttribute(DataType.Password)]
        public string passwordconfirmation {get;set;}
    }
}