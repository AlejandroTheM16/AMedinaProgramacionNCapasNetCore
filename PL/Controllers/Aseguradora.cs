using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Aseguradora : Controller
    {


        public ActionResult GetAll() // ActionMethod: Métodos que  tenemos en el controlador
        {
            ML.Aseguradora aseguradora = new ML.Aseguradora();

            ML.Result result = BL.Aseguradora.GetAllEF();

            if (result.Correct)
            {
                aseguradora.Aseguradoras = result.Objects;
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error en la busqueda" + result.Message;
                return View("Modal");
            }
            return View(aseguradora); //ACTION RESULT: Tipos de retorno EJEMPLO: ActionResult -> Vista en HTML
        }

        [HttpGet]
        public ActionResult Form(int? IdAseguradora)
        {

            ML.Aseguradora aseguradora = new ML.Aseguradora();
            ML.Usuario usuario = new ML.Usuario();
            aseguradora.Usuario = new ML.Usuario();
            ML.Result resultUsuario = BL.Usuario.GetAllEF(usuario);

            if (IdAseguradora == null)
            {

                aseguradora.Usuario.Usuarios = resultUsuario.Objects;
                return View(aseguradora);

            }
            else
            {

                ML.Result result = BL.Aseguradora.GetByIdEF(IdAseguradora.Value);

                if (result.Correct)
                {
                    aseguradora = (ML.Aseguradora)result.Object;
                    aseguradora.Usuario.Usuarios = resultUsuario.Objects;                    
                    return View(aseguradora);
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error en la busqueda" + result.Message;
                    return View("Modal");
                }

            }


        }

        [HttpPost]
        public ActionResult Form(ML.Aseguradora aseguradora)
        {

            IFormFile imagen = Request.Form.Files["fuImage"];
            if (imagen != null)
            {
                byte[] ImagenByte = ConvertToBytes(imagen);
                aseguradora.Imagen = Convert.ToBase64String(ImagenByte);

            }


            if (aseguradora.IdAseguradora == 0)
            {

                ML.Result result = BL.Aseguradora.AddEF(aseguradora);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "Registro existoso";
                    return View("Modal");
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error" + result.Message;
                    return View("Modal");
                }

            }
            else
            {

                ML.Result result = BL.Aseguradora.UpdateEF(aseguradora);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "Actualizacion existosa";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error" + result.Message;

                }
            }

            return View("Modal");

        }

        public static byte[] ConvertToBytes(IFormFile imagen)
        {
            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
    }
}
