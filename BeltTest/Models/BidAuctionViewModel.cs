using System.ComponentModel.DataAnnotations;
using System;

namespace BeltTest.Models {
    public class BidAuctionViewModel : BaseEntity {
        [Required] 
        public float bid {get;set;}
    }
}
