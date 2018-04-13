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
    public class UserController : Controller
    {
        private UserDBContext _context;
        public UserController(UserDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Login()
        {
            if (IsUserLoggedIn()) return RedirectToAction("Dashboard", "Auction");
            return View("Register");
        }


        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserViewModel item)
        {
            if (_context.Users.Count() > 0) {
                // if(_context.Users.Any(r => r.UserName.ToLower() == item.UserName.ToLower()))
                // {
                //     ModelState.AddModelError("UserName", "UserName address already exists. Please enter a different UserName.");
                // }
            }

            // As soon as the model is submitted TryValidateModel() is run for us, ModelState is already set
            if(ModelState.IsValid)
            {
                
                User newUser = new User {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    UserName = item.UserName,
                    Password = item.Password,
                    Wallet = 1000,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
    
                _context.Users.Add(newUser);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("UserId", newUser.UserId);

                return RedirectToAction("Dashboard", "Auction");
            }
            return View("Register", item);
        }


        // [HttpGet]
        // [Route("Login")]
        // public IActionResult Login()
        // {
        //     if (IsUserLoggedIn()) return RedirectToAction("Dashboard", "Auction");
        //     return View("Login");
        // }

        [HttpPost]
        [Route("VerifyLogin")]
        [ValidateAntiForgeryToken]
        public IActionResult VerifyLogin(string UserName, string Password)
        {
            string PasswordToCheck = Password;
            // Attempt to retrieve a user from your database based on the Email submitted
            var user = _context.Users.SingleOrDefault(r => r.UserName == UserName);
            if(user != null && PasswordToCheck != null)
            {
                var Hasher = new PasswordHasher<User>();
                // Pass the user object, the hashed password, and the PasswordToCheck
                if(0 != Hasher.VerifyHashedPassword(user, user.Password, PasswordToCheck))
                {
                    //Handle success
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    // if (user.Level == 1) {
                    //     return RedirectToAction("DashboardAdmin");
                    // }
                    return RedirectToAction("Dashboard", "Auction");
                }
                TempData["UserName"] = UserName;
                TempData["LoginError"] = "Password is incorrect!";
            } else {
                if (UserName != null) {
                    TempData["LoginError"] = "User name is incorrect!";
                    TempData["UserName"] = UserName;
                } else {
                    TempData["LoginError"] = "Please Input User name and password!";
                }
            }

            //Handle failure

            return RedirectToAction("Login");
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // [HttpGet]
        // [Route("dashboard")]
        // public IActionResult Dashboard()
        // {
        //     if (!IsUserLoggedIn()) return RedirectToAction("Login", "User");
            
        //     IEnumerable<User> AllUsers = _context.Users.OrderBy(r => r.FullName).ToList();
        //     User CurrentUser = _context.Users.SingleOrDefault(r => r.UserId == HttpContext.Session.GetInt32("UserId"));
        //     ViewBag.Users = AllUsers;
        //     if (CurrentUser.Level == 1) {
        //         return View("DashboardAdmin");
        //     }
        //     return View("Dashboard");
        // }
        
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
