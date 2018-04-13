using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace beltprep.Models
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string UserName { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Decimal Wallet { get; set; }

        public List<Auction> Auctions { get; set; }
        public List<Bid> Bids { get; set; }

        public User()
        {
            Bids = new List<Bid>();
            Auctions = new List<Auction>();
        }

        public string FullName { 
            get {
                return this.FirstName + " " + this.LastName;
            }
        }

    }
}