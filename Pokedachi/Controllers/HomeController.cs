using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace Pokedachi.Controllers
{
    public class Pikachu{
            Random rng = new Random();
            public int full {get; set;}
            public int happy {get;set;}
            public int energy {get; set;}
            public int meals {get; set;}
            public bool alive {get; set;}
            public string image {get; set;}
            public Pikachu(){
                this.reset();
            }
            public string feed(){
                if(this.meals == 0){
                    return "You don't have any meals to feed Pikachu!";
                } 
                int chance = rng.Next(1,101);
                if(chance > 26){
                int filling = rng.Next(5,11);
                this.full += filling;
                this.meals --;
                this.image = "/images/pikachu2.png";
                return $"You fed Pikachu! Fullness +{filling}, Meals -1";
                }
                else {
                    this.meals --;
                    this.image = "/images/pikachu4.png";
                    return "Pikachu didn't like that food at all and refused to eat! Meals -1";
                }
            }
            public string play(){
                if(this.energy < 5){
                    return "Pikachu is too tired to play right now.";
                }
                int chance = rng.Next(1,101);
                if(chance > 26){
                    int enjoy = rng.Next(5,11);
                    this.happy += enjoy;
                    this.energy -= 5;
                    this.image = "/images/pikachu2.png";
                    return $"You played with Pikachu! Happiness +{enjoy}, Energy -5";
                } 
                else{
                    this.energy -= 5;
                    this.image = "/images/pikachu4.png";
                    return $"Pikachu was not entertained by your shenanigans. Energy -5";
                }  
            }
            public string work(){
                 if(this.energy < 5){
                    return "Pikachu is too tired to work right now.";
                }
                int newmeal = rng.Next(1,4);
                this.meals += newmeal;
                this.energy -= 5;
                this.image = "/images/pikachu1.png";
                return $"You and Pikachu worked to make some more meals. Meals +{newmeal}, Energy -5";
            }
            public string sleep(){
                this.energy += 15;
                this.happy -= 5;
                this.full -= 5;
                this.image = "/images/pikachu3.png";
                return $"Pikachu took a nap. Energy +15, Happiness -5, Fullness -5";
            }
            public void reset(){
                full = 20;
                happy = 20;
                energy = 50;
                meals = 3;
                alive = true;
                // image = "~/images/pikachu1.png";
                image = "/images/pikachu1.png";
                
            }
            public bool checkFail(){
                if(this.happy <= 0 || this.full <= 0){
                    this.image = "/images/pikachu5.png";
                    return true;

                }
                return false;
            }
            public bool checkWin(){
                if(this.happy > 99 && this.full > 99 && this.energy > 99){
                    this.image = "/images/pikachu6.png";
                    return true;
                }
                return false;
            }
            
        }
    public class HomeController : Controller
    {
        public Pikachu pika {get;set;}

        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if(pika == null){
                if(HttpContext.Session.GetObjectFromJson<Pikachu>("pika") == null){
                pika = new Pikachu();
                }
                else{
                    pika = HttpContext.Session.GetObjectFromJson<Pikachu>("pika");
                }
            } 
            if(HttpContext.Session.GetString("message") == null){
                HttpContext.Session.SetString("message", "Hey! You just brought Pikachu home! Do your best to keep him happy and fed, or he'll become a dead beat waste of life.");
            }
            ViewBag.dead = false;
            ViewBag.win = false;
            Console.WriteLine(pika);
            if(pika.checkFail() == true){
                ViewBag.dead = true;
                ViewBag.message = "Looks like your Pikachu has become a deadbeat. Try again?";
                ViewBag.img= pika.image;
                return View();
            }
            if(pika.checkWin() == true){
                ViewBag.win = true;
                ViewBag.message = "Congrats! You win!";
                ViewBag.img=pika.image;
                return View();

            }
            ViewBag.fullness = pika.full;
            ViewBag.img = pika.image;
            ViewBag.happiness = pika.happy;
            ViewBag.meals = pika.meals;
            ViewBag.energy = pika.energy;
            ViewBag.message = HttpContext.Session.GetString("message");
            HttpContext.Session.SetObjectAsJson("pika", pika);
            return View();
        }
        [HttpGet]
        [Route("feed")]
        public IActionResult Feed(){
            Pikachu pika = HttpContext.Session.GetObjectFromJson<Pikachu>("pika");
            HttpContext.Session.SetString("message", pika.feed());
            HttpContext.Session.SetObjectAsJson("pika", pika);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("play")]
        public IActionResult Play(){
            Pikachu pika = HttpContext.Session.GetObjectFromJson<Pikachu>("pika");
            HttpContext.Session.SetString("message", pika.play() );
            HttpContext.Session.SetObjectAsJson("pika", pika);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("work")]
        public IActionResult Work(){
            Pikachu pika = HttpContext.Session.GetObjectFromJson<Pikachu>("pika");
            HttpContext.Session.SetString("message", pika.work());
            HttpContext.Session.SetObjectAsJson("pika", pika);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("sleep")]
        public IActionResult Sleep(){
            Pikachu pika = HttpContext.Session.GetObjectFromJson<Pikachu>("pika");
            HttpContext.Session.SetString("message", pika.sleep());
            HttpContext.Session.SetObjectAsJson("pika", pika);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("reset")]
        public IActionResult Reset(){
            Pikachu pika = HttpContext.Session.GetObjectFromJson<Pikachu>("pika");
            pika.reset();
            HttpContext.Session.SetObjectAsJson("pika", pika);
            HttpContext.Session.SetString("message", "Hey! You just brought Pikachu home! Do your best to keep him happy and fed, or he'll become a dead beat waste of life.");
            return RedirectToAction("Index");
        }
    }
    
}
