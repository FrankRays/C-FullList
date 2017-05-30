using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Login.Controllers
{
 public class LoginController : Controller
 {
  [HttpGet]
  [Route("")]
    public IActionResult Index()
  {
      return View();
  }
  [HttpPost]
  [Route("survey")]
    public IActionResult Survey(string name, string location, string language, string comment){
        ViewBag.name = name;
        Console.WriteLine(name);
        ViewBag.location = location;
        ViewBag.language = language;
        ViewBag.comment = comment;
        return View("Logged");
    }
 }
}