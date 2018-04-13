using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace beltprep.Models
{
    public class Bid : BaseEntity
    {
        public int BidId { get; set; }
        public Decimal BidAmount { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int AuctionId { get; set; }
        public Auction Auction { get; set; }
        public bool IsWon { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Bid()
        {
            
        }

    }
}