using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using beltReviewer.Models;


namespace beltReviewer.Controllers
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
                    user = db.newUser.SingleOrDefault(item => item.email == logmodel.email);
                } catch {
                    TempData["error"] = "Email or password was incorrect.";
                    return RedirectToAction("index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(user, user.password, logmodel.password)){
                    HttpContext.Session.SetInt32("user_id", user.user_id);
                    return View("dash");
                }else{
                    TempData["error"] = "Email or password was incorrect.";
                    return RedirectToAction("index");
                }
            }
            return View(logmodel);
        }
        [HttpPost]
        [Route("reg")] 
        public IActionResult Register(RegisterViewModel regmodel){
            Console.WriteLine("Register");
            // if(ModelState)
            if(ModelState.IsValid){
                PasswordHasher<User> Hasher = new PasswordHasher<User>();

                User newuser = new User{
                    username = regmodel.username,
                    created_at = DateTime.Now,
                    email = regmodel.email,
                    admin = 0
                };
                User testEmail = db.newUser.SingleOrDefault(newUser => newUser.email == regmodel.email);
                if(testEmail != null){
                    TempData["error"] = "Email address is already taken.";
                    return RedirectToAction("index");
                }
                newuser.password = Hasher.HashPassword(newuser, regmodel.password);
                db.newUser.Add(newuser);
                db.SaveChanges();
                User user = db.newUser.SingleOrDefault(newUser => newUser.email == regmodel.email);
                HttpContext.Session.SetInt32("user_id",user.user_id);
                if(user.user_id == 1 && user.admin == 0){
                    user.admin = 1;
                    db.SaveChanges();
                }
                return View("dash");
            }
            return View(regmodel);
        }
        [HttpGet]
        [Route("admin")]
        public IActionResult Admin(){
            return View("admin");
        }
        [HttpPost]
        [Route("admin")]
        public IActionResult Admin(LoginViewModel model){
            if(ModelState.IsValid){
                User user;
                try {
                    user = db.newUser.SingleOrDefault(item => item.email == model.email);
                } catch {
                    TempData["error"] = "Username or password was incorrect.";
                    return RedirectToAction("index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(user, user.password, model.password)){
                    if(user.admin == 1){
                    HttpContext.Session.SetInt32("user_id", user.user_id);

                    return View("addash");
                    }
                    else{
                        TempData["error"] = "Username or password was incorrect.";
                        return RedirectToAction("admin");
                    }
                }else{
                    TempData["error"] = "Username or password was incorrect.";
                    return RedirectToAction("admin");
                }

            }
            return View(model);
        }
    }
}
