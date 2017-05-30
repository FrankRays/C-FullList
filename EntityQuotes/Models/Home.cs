using System;
using System.ComponentModel.DataAnnotations;

namespace EntityQuotes.Models{
    public abstract class BaseEntity{}
    public class Quote : BaseEntity{
        [Key]
        public int idquotes {get;set;}  
        
        public string username {get;set;}
        
        public string quote{get;set;}
        public DateTime created_at{get;set;}
    }
}