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

        [HttpPost]
        public IActionResult Form(ML.Usuario usuarioLogin) {

            ML.Result result = BL.Usuario.GetByUserName(usuarioLogin);

            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                if (usuario.Password == usuarioLogin.Password)
                {

                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ViewBag.Mensaje = "El usuario y/o la contraseña no coinciden";
                    return View("Modal");

                }

            }
            else
            {

                ViewBag.Mensaje = "Credenciales Invalidas" + result.Message;
                return View("Modal");

            }

            return View("Modal");

        }

        
       
    }
}
