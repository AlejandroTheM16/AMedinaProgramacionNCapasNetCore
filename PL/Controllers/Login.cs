using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Login : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("Login");
        }

        
       
    }
}
