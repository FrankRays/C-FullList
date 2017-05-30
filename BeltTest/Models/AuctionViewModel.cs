using System.ComponentModel.DataAnnotations;
using System;

namespace BeltTest.Models {
    public class AuctionViewModel : BaseEntity {
        [Required]
        [MinLength(4)]
        public string product {get;set;}
        [Required]
        [MinLength(10)]
        public string description {get;set;}
        [Required]
        public double bid {get;set;}
        [Required]
        public DateTime end_date{get;set;}

    }
}