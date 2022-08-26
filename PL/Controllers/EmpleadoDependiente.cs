using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoDependiente : Controller
    {

        //const string SessionName = "NumeroEmpleado";

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


        //[HttpGet]
        //public ActionResult DependienteGetAll() { 

        //    ML.Dependiente dependiente = new ML.Dependiente();
        //    dependiente.Empleado = new ML.Empleado();
        //    dependiente.DependienteTipo = new ML.DependienteTipo();
        //    ML.Result resutlDependienteTipo = BL.DependienteTipo.GetAllEF();

        //    if (resutlDependienteTipo.Correct) {

        //        ML.Result result = BL.Dependiente.GetAllEF();

        //        if (result.Correct)
        //        {

        //            dependiente.Dependientes = result.Objects;
        //            dependiente.DependienteTipo.DependienteTipos = resutlDependienteTipo.Objects;

        //        }
        //        else {

        //            ViewBag.Mensaje = "Ocurrio Un error al traer los datos"+ result.Message;
        //            return View("Modal");
        //        }

        //        return View(dependiente);
        //    }

        //    return View("Modal");


        //}

        [HttpGet]
        public ActionResult DependienteGetById(string? NumeroEmpleado) {            

            if (NumeroEmpleado == null)
            {
                
               NumeroEmpleado = HttpContext.Session.GetString("NumeroEmpleado".ToString());

            }
                ML.Dependiente dependiente = new ML.Dependiente();
                dependiente.DependienteTipo = new ML.DependienteTipo();
                dependiente.Empleado = new ML.Empleado();
                //ML.Result resultEmpleado = BL.Empleado.GetById(NumeroEmpleado);               
                ML.Result resutlDependienteTipo = BL.DependienteTipo.GetAllEF();

                if (resutlDependienteTipo.Correct)
                {

                    ML.Result result = BL.Dependiente.GetById(NumeroEmpleado);

                    if (result.Correct)
                    {
                        //dependiente = (ML.Dependiente)result.Object;
                        dependiente.Dependientes = result.Objects;
                        //dependiente.Empleado.Empleados = resultEmpleado.Objects;                        
                        dependiente.DependienteTipo.DependienteTipos = resutlDependienteTipo.Objects;
                        HttpContext.Session.SetString("NumeroEmpleado", NumeroEmpleado);
                        return View(dependiente);

                    }
                    else
                    {

                        ViewBag.Mensaje = "Ocurrio Un error al traer los datos" + result.Message;
                        return View("Modal");
                    }

                    
                }

            
            //else {

                
            //    HttpContext.Session.GetString(SessionName);
            //    return View(DependienteGetById(NumeroEmpleado));

            //}

            return View("Modal");
        
        }

        [HttpPost]
        public ActionResult Form(ML.Dependiente dependiente) {

            HttpContext.Session.GetString("NumeroEmpleado");

            if (dependiente.IdDependiente == 0)
            {

                ML.Result result = BL.Dependiente.Add(dependiente);

                if (result.Correct)
                {

                    ViewBag.Mensaje = "-Registro exitoso-";
                    return View("Modal");

                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un erro en la insercion! " + result.Message;
                    return View("Modal");
                }
            }
            else {            

                ML.Result resultUpdate = BL.Dependiente.Update(dependiente);

                if (resultUpdate.Correct)
                {

                    ViewBag.Mensaje = "-Actualizacion exitosa-";
                    return View("Modal");

                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un erro en la actualizacion! " + resultUpdate.Message;
                    return View("Modal");
                }

         


            }

            return View(DependienteGetById(dependiente.Empleado.NumeroEmpleado));
        
        }

        [HttpGet]
        public ActionResult Form(int? IdDependiente) {

            ML.Dependiente dependiente = new ML.Dependiente();
            dependiente.DependienteTipo = new ML.DependienteTipo();
            dependiente.Empleado = new ML.Empleado();
            ML.Result resultDependienteTipo = BL.DependienteTipo.GetAllEF();           


            if (IdDependiente == null)
            {
                HttpContext.Session.GetString("NumeroEmpleado");
                dependiente.DependienteTipo.DependienteTipos = resultDependienteTipo.Objects;
                return View(dependiente);

            }
            else {

                ML.Result result = BL.Dependiente.GetByIdDependiente(IdDependiente.Value);

                if (result.Correct)
                {
                    
                    dependiente = (ML.Dependiente)result.Object;
                    //dependiente.Dependientes = result.Objects;
                    //dependiente.Empleado.Empleados = resultEmpleado.Objects;
                    dependiente.DependienteTipo.DependienteTipos = resultDependienteTipo.Objects;
                    
                    return View(dependiente);

                }
                else
                {

                    ViewBag.Mensaje = "Ocurrio Un error al traer los datos" + result.Message;
                    return View("Modal");
                }

                return View("Modal");

            }
        
        }

        public ActionResult Delete(int IdDependiente) { 

            ML.Dependiente dependiente = new ML.Dependiente();

            if (IdDependiente != null) {

                ML.Result result = BL.Dependiente.Delete(IdDependiente);

                if (result.Correct)
                {

                    ViewBag.Mensaje = "-Se elimino el dependiente-";
                    return View("Modal");

                }
                else {

                    ViewBag.Mensaje = "Ocurrio Un error al eliminar los datos" + result.Message;
                    return View("Modal");

                }
            
            }

            return View("Modal");
        
        }

        
    }
}
