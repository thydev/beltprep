using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace beltprep.Models
{
    public class Auction : BaseEntity
    {
        public int AuctionId { get; set; }
        public string ProductName { get; set; }
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

        // 
        // public List<Message> ReceivedMessages { get; set; }
        
        [InverseProperty("Auction")]
        public List<Bid> Bids { get; set; }

        public Auction()
        {
            Bids = new List<Bid>();
        }

        public decimal HighestBid {
            get {
                if (this.Bids.Count() > 0) 
                {
                    Bid aBid = this.Bids.OrderByDescending(r => r.BidAmount).FirstOrDefault();
                    if (aBid != null)
                    {
                        return aBid.BidAmount;
                    }
                }
                
                return 0;
            }
        }

        public decimal highestBid2 { get; set;}

        public User HighestBidder {
            get {
                if (this.Bids.Count() > 0) 
                {
                    Bid aBid = this.Bids.OrderByDescending(r => r.BidAmount).FirstOrDefault();
                    if (aBid != null)
                    {
                        return aBid.User;
                    }
                }
                return null;
            }
        }

        public int RemainingDay {
            get {
                double days = this.EndDate.Subtract(DateTime.Now).TotalDays;
                return (int)days;
            }
        }
    }
}