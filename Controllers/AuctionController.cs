using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using beltprep.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace userdb.Controllers
{
    public class AuctionController : Controller
    {
        private UserDBContext _context;
        public AuctionController(UserDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Dashboard")]
        public IActionResult Dashboard()
        {
            if (!IsUserLoggedIn()) return RedirectToAction("Login", "User");
            List<Auction> AllAuctions = _context.Auctions.Where(r => r.IsEnded == false)
                                    // .Include(r => r.Bids)
                                    .Include(r => r.User).OrderBy(r => r.RemainingDay).ToList();


            foreach (var item in AllAuctions)
            {
                if (_context.Bids.Where(r => r.AuctionId == item.AuctionId).Count() > 0) {
                    Bid abid = _context.Bids.Where(r => r.AuctionId == item.AuctionId).OrderByDescending(r => r.BidAmount).FirstOrDefault();
                    if( abid == null) {
                        item.highestBid2 = 0;
                    } else {
                        item.highestBid2 = abid.BidAmount;
                        
                    }

                    //Update the auction bid
                    if (item.IsEnded == false && item.RemainingDay == 0) 
                    {
                        User winner = _context.Users.SingleOrDefault(r => r.UserId == abid.UserId);
                        winner.Wallet -= abid.BidAmount;
                        Auction aAuc = item;
                        aAuc.IsEnded = true;
                        _context.Users.Update(winner);
                        _context.Auctions.Update(aAuc);
                        _context.SaveChanges();
                    }
                }
            }

            List<Auction> AllAuctionsDisplay = _context.Auctions.Where(r => r.IsEnded == false)
                                    // .Include(r => r.Bids)
                                    .Include(r => r.User).OrderBy(r => r.RemainingDay).ToList();


            foreach (var item in AllAuctionsDisplay)
            {
                if (_context.Bids.Where(r => r.AuctionId == item.AuctionId).Count() > 0)
                {
                    Bid abid = _context.Bids.Where(r => r.AuctionId == item.AuctionId).OrderByDescending(r => r.BidAmount).FirstOrDefault();
                    if( abid == null) {
                        item.highestBid2 = 0;
                    } else {
                        item.highestBid2 = abid.BidAmount;
                        
                    }
                }
            }

            User CurrentUser = _context.Users.SingleOrDefault(r => r.UserId == HttpContext.Session.GetInt32("UserId"));
            ViewBag.Auctions = AllAuctionsDisplay;
            ViewBag.User = CurrentUser;
            return View("Dashboard");
        }


        [HttpGet]
        [Route("Auction/New")]
        public IActionResult New()
        {
            if (!IsUserLoggedIn()) return RedirectToAction("Login", "User");
            
            return View("New");
        }

        
        [HttpPost]
        [Route("Auction/Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AuctionViewModel item)
        {

            // As soon as the model is submitted TryValidateModel() is run for us, ModelState is already set
            if(ModelState.IsValid)
            {
                
                Auction newItem = new Auction {
                    ProductName = item.ProductName,
                    Description = item.Description,
                    StartAmount = item.StartAmount,
                    EndDate = item.EndDate,
                    IsEnded = false,
                    UserId = (int)HttpContext.Session.GetInt32("UserId"),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _context.Auctions.Add(newItem);
                _context.SaveChanges();

                return RedirectToAction("Dashboard", "Auction");
            }
            return View("New", item);
        }

        [HttpGet]
        [Route("Auction/ShowBid/{AuctionId}")]
        public IActionResult ShowBid(int AuctionId)
        {
            if (!IsUserLoggedIn()) return RedirectToAction("Login", "User");
            Auction theAuction = _context.Auctions
                    .Include(r => r.Bids).ThenInclude(r => r.User)
                    .Include(r => r.User).SingleOrDefault(r => r.AuctionId == AuctionId);
            // User EditUser = _context.Users

            //                 .Include(r => r.ReceivedMessages)
            //                     .ThenInclude(r => r.Sender)
            //                     .ThenInclude(r => r.Comments)
            //                 .SingleOrDefault(r => r.UserId == UserId);

            ViewBag.Auction = theAuction;
            
            return View("Show");
        }
        
        [HttpPost]
        [Route("Auction/PostBid")]
        [ValidateAntiForgeryToken]
        public IActionResult PostBid(int AuctionId, decimal BidAmount)
        {
            if (!IsUserLoggedIn()) return RedirectToAction("Login", "User");

            Auction theAuction = _context.Auctions.SingleOrDefault(r => r.AuctionId == AuctionId);
            User theUser = _context.Users.SingleOrDefault(r => r.UserId == (int)HttpContext.Session.GetInt32("UserId"));
            if (BidAmount > theAuction.HighestBid)
            {
                if (BidAmount <= theUser.Wallet)
                {
                    Bid newItem = new Bid {
                        AuctionId = AuctionId,
                        BidAmount = BidAmount,
                        IsWon = false,
                        UserId = (int)HttpContext.Session.GetInt32("UserId"),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    _context.Bids.Add(newItem);
                    _context.SaveChanges();
                    return RedirectToAction("Dashboard", "Auction");
                } else {
                    TempData["Errors"] = $"You don't have enough money to bie. Your current wallet ${theUser.Wallet}";
                }
            } else {
                TempData["Errors"] = "The bid amount must be greater thatn the current bid.";
            }

            //??
            return RedirectToAction("ShowBid", new { AuctionId = AuctionId});
        }


        [HttpPost]
        [Route("Auction/Delete/{AuctionId}")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int AuctionId)
        {
            Auction item = _context.Auctions.SingleOrDefault(r => r.AuctionId == AuctionId);
            _context.Auctions.Remove(item);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        private bool IsUserLoggedIn()
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0 ) {
                return false;
            } else {
                return true;
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
