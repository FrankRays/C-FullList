using System.ComponentModel.DataAnnotations;

namespace newquotes.Models{
    public abstract class BaseEntity{}
    public class Quote : BaseEntity{

        [Key]
        public long Id {get; set;}
        [Required]
        [MinLength(3)]
        public string username {get; set;}
        [Required]
        public string quote{get; set;}
        public string created_at {get;set;}
    }
}