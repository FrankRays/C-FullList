using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BeltTest.Models;

namespace BeltTest.Controllers
{
    public class HomeController : Controller
    {
        private YourContext db;

        public HomeController(YourContext context){
            db = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if(TempData["error"] != null){
                ViewBag.error = TempData["error"];
            }
            return View();
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginViewModel logmodel){
            if(ModelState.IsValid){
                User user;
                try {
                    user = db.Users.SingleOrDefault(item => item.username == logmodel.username);
                } catch {
                    TempData["error"] = "Username or password was incorrect.";
                    return RedirectToAction("index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(user, user.password, logmodel.password)){
                    HttpContext.Session.SetInt32("id", user.id);
                    return RedirectToAction("dashboard");
                }else{
                    TempData["error"] = "Username or password was incorrect.";
                    return RedirectToAction("index");
                }
            }
            return View("index",logmodel);
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterViewModel regmodel){
            Console.WriteLine("Register");
            // if(ModelState)
            if(ModelState.IsValid){
                User testuser;
                try {
                    testuser = db.Users.SingleOrDefault(item => item.username == regmodel.username);
                    if(testuser != null){
                        TempData["error"] = "Username already exists.";
                        return RedirectToAction("index");
                    } 
                } catch {
                    Console.WriteLine("All Good");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                User newuser = new User{
                    username = regmodel.username,
                    created_at = DateTime.Now,
                    fname = regmodel.fname,
                    lname = regmodel.lname,
                    wallet = 1000
                };
                newuser.password = Hasher.HashPassword(newuser, regmodel.password);
                db.Users.Add(newuser);
                db.SaveChanges();
                User user = db.Users.SingleOrDefault(newUser => newUser.username == regmodel.username);
                HttpContext.Session.SetInt32("id",user.id);
                return RedirectToAction("dashboard");
            }
            return View("index",regmodel);
        }
        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard(){
            if(HttpContext.Session.GetInt32("id") == null){
                TempData["error"] = "Please login to continue.";
                return RedirectToAction("index");  
            }
            User currentUser = db.Users.SingleOrDefault(user => user.id == (int) HttpContext.Session.GetInt32("id"));
            ViewData["user"] = currentUser.fname;
            ViewData["id"] = currentUser.id;  
            ViewData["wallet"] = currentUser.wallet;
            List<Auction> allAuctions = db.Auctions.Include(Auction => Auction.highest_bidder).Include(Auction => Auction.poster).Where(Auction => Auction.active == 1).ToList();
            foreach(Auction auction in allAuctions){
                TimeSpan delta = auction.end_date.Subtract(DateTime.Now);
                TimeSpan zero = DateTime.Now.Subtract(DateTime.Now);
                if (auction.active == 1 && delta < zero){ 
                    auction.highest_bidder.wallet -= auction.bid;
                    auction.poster.wallet += auction.bid;
                    auction.active = 0;
                    db.SaveChanges(); 
                }
            }
            allAuctions = db.Auctions.Include(Auction => Auction.highest_bidder).Include(Auction => Auction.poster).Where(Auction => Auction.active == 1).ToList();
            return View("dashboard", allAuctions);
        }
        [HttpGet]
        [Route("new")]
        public IActionResult New(){
            if(HttpContext.Session.GetInt32("id") == null){
                TempData["error"] = "Please login to continue.";
                return RedirectToAction("index");
            }
            return View("new");
        }
        [HttpPost]
        [Route("processNew")] 
        public IActionResult processNew(AuctionViewModel aucmodel){
            if(ModelState.IsValid){ 
                User currentUser = db.Users.SingleOrDefault(user => user.id == (int) HttpContext.Session.GetInt32("id"));
                Auction newAuction = new Auction{
                    product = aucmodel.product,  
                    description = aucmodel.description,
                    bid = (float) aucmodel.bid,
                    created_at = DateTime.Now,
                    end_date = aucmodel.end_date,
                    active = 1,
                    poster = currentUser, 
                };
                db.Auctions.Add(newAuction);
                // currentUser.auctions.Add(newAuction);
                db.SaveChanges();
                return RedirectToAction("dashboard");
            }
            return View(aucmodel);
        }
        [HttpGet]
        [Route("bid/{id}")] 
        public IActionResult Bid(int id){
            if(HttpContext.Session.GetInt32("id") == null){
                TempData["error"] = "Please login to continue.";
                return RedirectToAction("index");
            }
            User currentUser = db.Users.SingleOrDefault(user => user.id == (int) HttpContext.Session.GetInt32("id"));
            Auction currentAuction = db.Auctions.Include(Auction => Auction.highest_bidder).Include(Auction => Auction.poster).SingleOrDefault(item => item.id == id);
            ViewData["current"] = currentAuction;
            ViewData["user"] = currentUser;
            if(TempData["error"] != null){
                ViewData["error"] = TempData["error"];
            }
            return View("bid");
        }
        [HttpPost]
        [Route("bid/{id}")]
        public IActionResult Bid(int id, BidAuctionViewModel bidmodel){
            if(HttpContext.Session.GetInt32("id") == null){
                TempData["error"] = "Please login to continue.";
                return RedirectToAction("index");
            }
            Auction currentAuction = db.Auctions.Include(Auction => Auction.highest_bidder).Include(Auction => Auction.poster).SingleOrDefault(item => item.id == id);
            if(currentAuction.active == 0){
                TempData["error"] = "You cannot bid on a closed auction.";
                return RedirectToRoute($"bid/{currentAuction.id}"); 
            }
            currentAuction.bid = bidmodel.bid;
            User currentUser = db.Users.SingleOrDefault(user => user.id == (int) HttpContext.Session.GetInt32("id"));
            currentAuction.highest_bidder = currentUser;
            return RedirectToAction("dashboard");
        }
        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult delete(int id){
            if(HttpContext.Session.GetInt32("id") == null){
                TempData["error"] = "Please login to continue.";
                return RedirectToAction("index");
            }
            Auction toRemove = db.Auctions.SingleOrDefault(item => item.id == id);
            db.Auctions.Remove(toRemove);
            db.SaveChanges();
            return RedirectToAction("dashboard");
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("index");
        }
    }

}
