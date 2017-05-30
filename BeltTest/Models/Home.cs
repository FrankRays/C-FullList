using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeltTest.Models{
    public abstract class BaseEntity{}

    public class User : BaseEntity {
        // public User(){
        //     auctions = new List<Auction>();
        // }
        [Key]
        public int id {get;set;}
        public string username {get;set;}
        public string fname {get;set;}
        public string lname {get;set;}
        public string password {get;set;}
        public float wallet {get;set;}
        public DateTime created_at {get;set;}
        // public List<Auction> auctions {get;set;}
    } 
    public class Auction : BaseEntity {
        [Key] 
        public int id {get;set;}
        public string product {get;set;}
        public string description {get;set;}
        public float bid {get;set;}
        public User poster {get;set;}
        public DateTime created_at {get;set;}
        public DateTime end_date {get;set;}
        public User highest_bidder {get;set;}
        public int active {get;set;}
    }
}