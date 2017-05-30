using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace RandomWordGen.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("attempts") == null){
                HttpContext.Session.SetInt32("attempts", 0);
            }
            if(HttpContext.Session.GetString("word") == null){
                HttpContext.Session.SetString("word", "");
            }
            ViewBag.attempt = HttpContext.Session.GetInt32("attempts");
            ViewBag.word = HttpContext.Session.GetString("word");
            return View();
        }
        [HttpGet]
        [Route("randomize")]
        public IActionResult Randomize(){
            string random = "";
            Random ranNum = new Random();
            int runs = ranNum.Next(7,11);
            for(int i = 0; i <= runs; i++ ){
                int code = ranNum.Next(65,123);
                char character = (char) code;
                Console.WriteLine(character);
                random += character.ToString();
            }
            HttpContext.Session.SetString("word", random);
            int current = (int) HttpContext.Session.GetInt32("attempts");
            current ++;
            HttpContext.Session.SetInt32("attempts", current);
            return RedirectToAction("Index");
        }
    }
}
