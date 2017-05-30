using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using EntityQuotes.Models;

namespace EntityQuotes.Controllers
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
            return View();
        }
        [HttpPost]
        [Route("")]  
        public IActionResult Index(RegisterViewModel model){
            if(ModelState.IsValid){
                Quote newQuote = new Quote{
                    username = model.username,
                    quote = model.quote,
                    created_at = DateTime.Now,
                    
                };
                db.quotes.Add(newQuote);
                db.SaveChanges();
                Console.WriteLine("Success");
                return RedirectToAction("displayQuotes");
            }
            Console.WriteLine("Fail");
            return View(model);
        }
        [HttpGet]
        [Route("displayQuotes")]
        public IActionResult DisplayQuotes(){
            Console.WriteLine("Display?");
            Console.WriteLine(db.quotes.ToList());
            List<Quote> model = db.quotes.ToList();
            
            return View("displayQuotes", model);
        }   
    }
}
