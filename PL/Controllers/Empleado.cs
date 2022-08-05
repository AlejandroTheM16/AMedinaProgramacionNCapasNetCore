using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Empleado : Controller
    {
        public ActionResult GetAll()
        {

            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();
            ML.Result resultEmpresa = BL.Empresa.GetAll();           

            
            if (resultEmpresa.Correct) {

                empleado.Nombre = (empleado.Nombre == null) ? "" : empleado.Nombre;
                empleado.ApellidoPaterno = (empleado.ApellidoPaterno == null) ? "" : empleado.ApellidoPaterno;
                empleado.ApellidoMaterno = (empleado.ApellidoMaterno == null) ? "" : empleado.ApellidoMaterno;
                empleado.Empresa.IdEmpresa = (empleado.Empresa.IdEmpresa == null) ? 0 : empleado.Empresa.IdEmpresa;               
                ML.Result result = BL.Empleado.GetAll(empleado);

                if (result.Correct)
                {
                    empleado.Empleados = result.Objects;
                    empleado.Empresa.Empresas = resultEmpresa.Objects;
                }
                else
                {
                    ViewBag.Mensaje = "¡Ocurrio un error!" + result.Message;
                    return View("Modal");
                }

                return View(empleado);

            }

            return View("Modal");

        }

        [HttpPost]
        public ActionResult GetAll(ML.Empleado empleado)
        {
            
            ML.Result resultEmpresa = BL.Empresa.GetAll();

            empleado.Nombre = (empleado.Nombre == null) ? "" : empleado.Nombre;
            empleado.ApellidoPaterno = (empleado.ApellidoPaterno == null) ? "" : empleado.ApellidoPaterno;
            empleado.ApellidoMaterno = (empleado.ApellidoMaterno == null) ? "" : empleado.ApellidoMaterno;
            empleado.Empresa.IdEmpresa = (empleado.Empresa.IdEmpresa == null) ? 0 : empleado.Empresa.IdEmpresa;

            ML.Result result = BL.Empleado.GetAll(empleado);

            if (resultEmpresa.Correct) {

                if (result.Correct)
                {
                    empleado.Empleados = result.Objects;
                    empleado.Empresa.Empresas = resultEmpresa.Objects;
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error " + result.Message;
                    return View("Modal");
                }

                return View(empleado);
            }

            return View("Modal");
        }


        [HttpGet]
        public ActionResult Form(string NumeroEmpleado)
        {

            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();

            ML.Result resultEmpresa = BL.Empresa.GetAll();

            if (resultEmpresa.Correct)
            {

                if (NumeroEmpleado == null)
                {
                    empleado.Empresa.Empresas = resultEmpresa.Objects;
                    empleado.Action = "Add";
                    return View(empleado);
                }
                else
                {

                    ML.Result result = BL.Empleado.GetById(NumeroEmpleado);

                    if (result.Correct)
                    {
                        empleado = (ML.Empleado)result.Object;
                        empleado.Empresa.Empresas = resultEmpresa.Objects;
                        empleado.Action = "Update";
                        return View(empleado);

                    }
                    else
                    {

                        ViewBag.Mensaje = "¡Ocurrio un error!" + result.Message;
                        return View("Modal");

                    }

                }

            }
            else {

                ViewBag.Mensaje = "Ocurrio un error al realizar la consulta" + resultEmpresa.Message;
                return View("Modal");
            }

        }

        [HttpPost]
        public ActionResult Form(ML.Empleado empleado) {

            IFormFile foto = Request.Form.Files["fuImage"];
            if (foto != null)
            {
                byte[] ImagenByte = ConvertToBytes(foto);
                empleado.Foto = Convert.ToBase64String(ImagenByte);

            }

            if (empleado.Action == "Add")
            {

                ML.Result result = BL.Empleado.Add(empleado);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "-Registro exitoso-" + result.Message;
                    return View("Modal");
                }
                else
                {
                    ViewBag.Mensaje = "¡Ocurrio un error!" + result.Message;
                    return View("Modal");
                }
            }
            else {

                if (empleado.Action == "Update")
                {

                    ML.Result result = BL.Empleado.Update(empleado);

                    if (result.Correct)
                    {
                        ViewBag.Mensaje = "-Registro actualizado-" + result.Message;
                        return View("Modal");
                    }
                    else
                    {
                        ViewBag.Mensaje = "¡Ocurrio un error!" + result.Message;
                        return View("Modal");                        
                    }

                }
                else {

                    ViewBag.Mensaje = "Accion no reconocida";
                     
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
