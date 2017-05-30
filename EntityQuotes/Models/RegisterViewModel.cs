using System.ComponentModel.DataAnnotations;

namespace EntityQuotes.Models{
    public class RegisterViewModel : BaseEntity{
        [Required] 
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+.$")]
        public string username { get; set; }
        [Required]
        [MinLength(5)]
        public string quote {get;set;}

    }
}