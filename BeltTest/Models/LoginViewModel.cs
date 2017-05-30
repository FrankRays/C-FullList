using System.ComponentModel.DataAnnotations;

namespace BeltTest.Models{
    public class LoginViewModel : BaseEntity{
        [Required]
        public string username {get; set;}

        [Required]
        [DataTypeAttribute(DataType.Password)]
        public string password {get;set;}
        
    }
}