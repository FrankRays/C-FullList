using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;


namespace YourNamespace.Controllers
{
 public class HelloController : Controller
 {
  [HttpGet]
  [Route("")]
    public IActionResult Index()
  {
      DateTime CurrentDate = DateTime.Now;

      string DisplayTime = " "+ CurrentDate.ToString("MMM dd, yyyy")+"";
      string DisplayDate = " "+ CurrentDate.ToString("h:mm tt")+""; 
      ViewBag.displaytime = DisplayTime;
      ViewBag.displaydate = DisplayDate;
   return View(); 
  }
 }
}