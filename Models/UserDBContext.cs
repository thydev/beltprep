using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace beltprep.Models
{
    public class UserDBContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options) 
        { 
            // ??? What inside here?
        }

        // This DbSet contains objects and database table
        // DbSet<ModelName> DatabaseTabaleName {get; set;}
        public DbSet<User> Users { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }
        
    }
}