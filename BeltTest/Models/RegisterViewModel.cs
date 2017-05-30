using System.ComponentModel.DataAnnotations;

namespace BeltTest.Models {
    public class RegisterViewModel : BaseEntity {
        [Required]
        [MinLength(3)]
        public string fname {get;set;}
        [Required]
        [MinLength(3)]
        public string lname {get;set;}

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string username {get;set;}
        [Required]
        [MinLength(7)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,20}$")]
        [DataTypeAttribute(DataType.Password)]
        public string password {get;set;}
        [Compare("password", ErrorMessage = "Password and confirmation must match.")]
        [DataTypeAttribute(DataType.Password)]
        public string passwordconfirmation {get;set;}
    }
}