using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Empresa : Controller
    {
        public ActionResult GetAll() // ActionMethod: Métodos que  tenemos en el controlador
        {
            ML.Empresa empresa = new ML.Empresa();

            ML.Result result = BL.Empresa.GetAll();

            if (result.Correct)
            {
                empresa.Empresas = result.Objects;
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error en la busqueda" + result.Message;
                return View("Modal");
            }
            return View(empresa); //ACTION RESULT: Tipos de retorno EJEMPLO: ActionResult -> Vista en HTML
        }

        [HttpGet]
        public ActionResult Form(int? IdEmpresa)
        {

            ML.Empresa empresa = new ML.Empresa();           
           

            if (IdEmpresa == null)
            {
                
                return View(empresa);

            }
            else
            {

                ML.Result result = BL.Empresa.GetById(IdEmpresa.Value);

                if (result.Correct)
                {
                                    
                    empresa = (ML.Empresa)result.Object;
                    return View(empresa);

                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error en la busqueda" + result.Message;
                    return View("Modal");
                }

            }


        }

        [HttpPost]
        public ActionResult Form(ML.Empresa empresa)
        {

            IFormFile logo = Request.Form.Files["fuImage"];
            if (logo != null)
            {
                byte[] ImagenByte = ConvertToBytes(logo);
                empresa.Logo = Convert.ToBase64String(ImagenByte);

            }


            if (empresa.IdEmpresa == 0)
            {

                ML.Result result = BL.Empresa.Add(empresa);

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

                ML.Result result = BL.Empresa.Update(empresa);

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
