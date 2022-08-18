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

        [HttpPost]
        public ActionResult EmpleadoGetAll(ML.Empleado empleado)
        {

            ML.Result resultEmpresa = BL.Empresa.GetAll();

            empleado.Nombre = (empleado.Nombre == null) ? "" : empleado.Nombre;
            empleado.ApellidoPaterno = (empleado.ApellidoPaterno == null) ? "" : empleado.ApellidoPaterno;
            empleado.ApellidoMaterno = (empleado.ApellidoMaterno == null) ? "" : empleado.ApellidoMaterno;
            empleado.Empresa.IdEmpresa = (empleado.Empresa.IdEmpresa == null) ? 0 : empleado.Empresa.IdEmpresa;

            ML.Result result = BL.Empleado.GetAll(empleado);

            if (resultEmpresa.Correct)
            {

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
        public ActionResult DependienteGetAll() { 

            ML.Dependiente dependiente = new ML.Dependiente();
            dependiente.Empleado = new ML.Empleado();
            dependiente.DependienteTipo = new ML.DependienteTipo();
            ML.Result resutlDependienteTipo = BL.DependienteTipo.GetAllEF();

            if (resutlDependienteTipo.Correct) {

                ML.Result result = BL.Dependiente.GetAllEF();

                if (result.Correct)
                {

                    dependiente.Dependientes = result.Objects;
                    dependiente.DependienteTipo.DependienteTipos = resutlDependienteTipo.Objects;

                }
                else {

                    ViewBag.Mensaje = "Ocurrio Un error al traer los datos"+ result.Message;
                    return View("Modal");
                }

                return View(dependiente);
            }

            return View("Modal");
        
            
        }

        [HttpGet]
        public ActionResult DependienteGetById(string NumeroEmpleado) {

            ML.Dependiente dependiente = new ML.Dependiente();
            dependiente.Empleado = new ML.Empleado();
            //ML.Result resultEmpleado = BL.Empleado.GetById(NumeroEmpleado);
            dependiente.DependienteTipo = new ML.DependienteTipo();
            ML.Result resutlDependienteTipo = BL.DependienteTipo.GetAllEF();

            if (NumeroEmpleado != null)
            {

                if (resutlDependienteTipo.Correct)
                {

                    ML.Result result = BL.Dependiente.GetById(NumeroEmpleado);

                    if (result.Correct)
                    {
                        //dependiente = (ML.Dependiente)result.Object;
                        dependiente.Dependientes = result.Objects;
                        //dependiente.Empleado.Empleados = resultEmpleado.Objects;
                        dependiente.DependienteTipo.DependienteTipos = resutlDependienteTipo.Objects;
                        return View(dependiente);

                    }
                    else
                    {

                        ViewBag.Mensaje = "Ocurrio Un error al traer los datos" + result.Message;
                        return View("Modal");
                    }

                    
                }

            }
            else {

                //dependiente.Empleado.Empleados = resultEmpleado.Objects;
                return View(dependiente);

            }

            return View("Modal");
        
        }

        [HttpPost]
        public ActionResult Form(ML.Dependiente dependiente) {

            if (dependiente.IdDependiente == null) {

                ML.Result result = BL.Dependiente.Add(dependiente);
            }

            return View(dependiente);
        
        }

        
    }
}
