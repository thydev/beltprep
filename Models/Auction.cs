using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace beltprep.Models
{
    public class Auction : BaseEntity
    {
        public int AuctionId { get; set; }
        public string Description { get; set; }
        public Decimal StartAmount { get; set; }

        public DateTime EndDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public bool IsEnded { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        // [InverseProperty("Sender")]
        // public List<Message> SentMessages { get; set; }

        // [InverseProperty("Receiver")]
        // public List<Message> ReceivedMessages { get; set; }

        public List<Bid> Bids { get; set; }

        public Auction()
        {
            Bids = new List<Bid>();
        }

    }
}