using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace beltReviewer.Models {
    public abstract class BaseEntity{}
    public class User : BaseEntity{
        [Key]
        public int user_id {get;set;}
        public string username {get;set;}
        public string password {get;set;}
        public string email {get;set;}
        public DateTime created_at {get;set;}
        public List<Rental> rentals {get;set;}
        public User() {
            rentals = new List<Rental>();
        } 
        public int admin {get;set;}
    }
    public class Rental : BaseEntity{
        [Key]
        public int rental_id {get;set;}
        public int User_id {get;set;}
        public DateTime due_date {get;set;}
        public DateTime return_date {get;set;}

    }
    public class Car : BaseEntity {
        [Key]
        public int car_id {get;set;}
        public Make make_id {get;set;}
        public Model model_id {get;set;}
        public int quantity {get;set;}
    }
    public class Make :BaseEntity{
        [Key]
        public int make_id {get;set;}
        public string name {get;set;}

    }
    public class Model : BaseEntity {
        [Key]
        public int model_id {get;set;}
        public string name {get;set;}
        
    }
}
