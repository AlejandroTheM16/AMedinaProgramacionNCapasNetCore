using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoDependiente : Controller
    {

        [HttpGet]
        public ActionResult EmpleadoGetAll()
        {

            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();
            ML.Result resultEmpresa = BL.Empresa.GetAll();


            if (resultEmpresa.Correct)
            {

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
    }
}
