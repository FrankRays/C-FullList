using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace firstaspnet.Controllers
{
 public class HelloController : Controller
 {
 
 [HttpGet]
 [Route("")]
  public IActionResult Index()
  {
   return View("Index");
  }
 }
}