using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using actuallogin.Factory;
using actuallogin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace actuallogin.Controllers
{
    public class HomeController : Controller
    {
        private readonly LoginFactory loginFactory;
        public HomeController(LoginFactory user){
            loginFactory = user;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if(TempData["noLogin"] != null){
                ViewBag.noLogin = TempData["noLogin"];
            }
            // else{
            //     ViewBag.noLogin = " ";
            // }
            if(TempData["errors"] != null){
                ViewBag.Errors = TempData["errors"];
            }
            // else {
            //     ViewBag.noLogin = " ";
            // }
            return View();
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(string email, string password)
        {
            User user = loginFactory.FindByEmail(email);
            
            if(user != null && password != null){
                string pass = user.password;
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                int id = user.id;
                int results = (int) Hasher.VerifyHashedPassword(user, pass, password);
                if (results != 0){
                HttpContext.Session.SetInt32("Id", id);
                return RedirectToAction("success");
                }   
            }
            TempData["noLogin"] = "Email or Password was not entered correctly.";
            return RedirectToAction("index");
        }
        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            if(TempData["errors"] != null){
                ViewBag.Errors = TempData["errors"];
            }
            return View();
        }
        [HttpPost]
        [Route("processReg")]
        public IActionResult ProcessReg(User newUser)
        {
            if(ModelState.IsValid){
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.password = Hasher.HashPassword(newUser, newUser.password);
                try{
                loginFactory.Add(newUser);
                }
                catch {
                    TempData["noLogin"] = "Email already exists.";
                    return RedirectToAction("index");
                }
                User user = loginFactory.FindByEmail(newUser.email);
                int id = user.id;
                HttpContext.Session.SetInt32("Id", id);
                return RedirectToAction("success");
            }
            // TempData["errors"] 
            List<string> errormessage = new List<string>();
            foreach(var error in ModelState.Values){
                if(error.Errors.Count > 0){
                    errormessage.Add(error.Errors[0].ErrorMessage);
                }
            }   
            TempData["errors"] = errormessage;
            return RedirectToAction("register");
        }
        [HttpGet]
        [Route("success")]
        public IActionResult Success()
        {
            if(HttpContext.Session.GetInt32("Id") == null){
                TempData["noLogin"] = "Please log in to continue!";
                return RedirectToAction("index");
            }
            int id = (int) HttpContext.Session.GetInt32("Id");
            User user = loginFactory.FindByID(id);
            ViewBag.user = user;
            return View();
        }
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("index");
        }
    }
}
