using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace TheWall.Models{
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
    public class Post : BaseEntity {
        [Key]
        public int id {get;set;}
        [Required]
        public string content {get; set;}
        public DateTime created_at {get;set;}
        public User User_id {get;set;}
    }
    public class Comment : BaseEntity {
        [Key]
        public int c_id {get;set;}
        public string content {get;set;}
        public DateTime created_at {get;set;}
        public User User_id {get;set;}
        public Post WallPost_id {get;set;}
    } 

}