using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using newquotes.Models;
using newquotes.Factory;

namespace newquotes.Controllers
{
    public class HomeController : Controller
    {
        private readonly QuoteFactory quoteFactory;
        public HomeController(){
            quoteFactory = new QuoteFactory();
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if(TempData["errors"] != null){
                ViewBag.Errors = TempData["errors"]; 
            } 
            return View();
        }
        [HttpPost]
        [Route("Quotes")]
        public IActionResult Quotes(Quote NewQuote){
            if (ModelState.IsValid){ 
                quoteFactory.Add(NewQuote);
                return RedirectToAction("DisplayQuotes");
            }
            else {
                TempData["errors"] = ModelState.Values;
                return RedirectToAction("Index");
            }
            
        }
        [HttpGet]
        [Route("DisplayQuotes")]
        public IActionResult DisplayQuotes(){
            ViewBag.quotes = quoteFactory.FindAll();
            return View("quotes");  
        }
    }
}
