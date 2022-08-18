using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Usuario : Controller
    {
        [HttpGet]
        public ActionResult GetAll() // ActionMethod: Métodos que  tenemos en el controlador
        {

            ML.Usuario usuario = new ML.Usuario();
            usuario.Nombre = (usuario.Nombre == null) ? "" : usuario.Nombre;
            usuario.ApellidoPaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = (usuario.ApellidoMaterno == null) ? "" : usuario.ApellidoMaterno;
            //ML.Result result = BL.Usuario.GetAllEF(usuario);

            ML.Result resultApi = new ML.Result();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5050/");

                var responseTask = client.GetAsync("api/Usuario/GetAll");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    resultApi.Objects = new List<Object>();

                    foreach (var resultItem in readTask.Result.Objects) {

                        ML.Usuario resultUsuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                        resultApi.Objects.Add(resultUsuario);
                       

                    }

                    
                }
            }

            usuario.Usuarios = resultApi.Objects;

            //if (result.Correct)
            //{
            //    usuario.Usuarios = result.Objects;
            //}
            //else
            //{
            //    ViewBag.Mensaje = "Ocurrio un error " + result.Message;
            //    return View("Modal");
            //}
            return View(usuario); //ACTION RESULT: Tipos de retorno EJEMPLO: ActionResult -> Vista en HTML
        }

        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario) 
        {            
            usuario.Nombre = (usuario.Nombre == null) ? "" : usuario.Nombre;
            usuario.ApellidoPaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = (usuario.ApellidoMaterno == null) ? "" : usuario.ApellidoMaterno;
            ML.Result result = BL.Usuario.GetAllEF(usuario);

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error " + result.Message;
                return View("Modal");
            }
            return View(usuario); 
        }

        [HttpGet]
        public ActionResult Form(int? Id_Usuario)
        {

            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
            ML.Result resultRol = BL.Rol.GetAllEF();
            ML.Result resultPais = BL.Pais.GetAllEF();


            if (resultRol.Correct && resultPais.Correct)
            {

                if (Id_Usuario == null)
                {

                    usuario.Rol.Rols = resultRol.Objects;
                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                    return View(usuario);

                }
                else
                {
                    ML.Result resultApi = new ML.Result();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:5050/");

                        var responseTask = client.GetAsync("api/Usuario/GetById/?Id_Usuario="+Id_Usuario);
                        responseTask.Wait();

                        var result = responseTask.Result;

                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();

                            resultApi.Object = new List<Object>();
                            

                            var resultItem = readTask.Result.Object;

                            ML.Usuario resultUsuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                            
                            resultApi.Object = resultUsuario;                         


                        }
                    }

                    usuario = (ML.Usuario)resultApi.Object;
                    ML.Result resultEstado = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                    ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                    ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                    usuario.Rol.Rols = resultRol.Objects;
                    usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                    usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

                    //ML.Result result = BL.Usuario.GetByIdEF(Id_Usuario.Value);

                    //if (result.Correct)
                    //{
                    //    usuario = (ML.Usuario)result.Object;
                    //    ML.Result resultEstado = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                    //    ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                    //    ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                    //    usuario.Rol.Rols = resultRol.Objects;
                    //    usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                    //    usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                    //    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                    //    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                    //    return View(usuario);

                    //}
                    //else
                    //{
                    //    ViewBag.Mensaje = "Ocurrio un error en la busqueda" + result.Message;
                    //    return View("Modal");
                    //}

                }

            }

            return View(usuario);

        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {

            if (ModelState.IsValid)
            {

                IFormFile imagen = Request.Form.Files["fuImage"];
                if (imagen != null)
                {
                    byte[] ImagenByte = ConvertToBytes(imagen);
                    usuario.Imagen = Convert.ToBase64String(ImagenByte);

                }


                if (usuario.Id_Usuario == 0)
                {
                    using (var client = new HttpClient()) {
                    
                        client.BaseAddress = new Uri("http://localhost:5050/");

                        var postTask = client.PostAsJsonAsync<ML.Usuario>("api/Usuario/Add",usuario);
                        postTask.Wait();

                        var resultService = postTask.Result;

                        if (resultService.IsSuccessStatusCode)
                        {

                            ViewBag.Mensaje = "Registro Exitoso";
                            return View("Modal");
                        }
                        else {

                            ViewBag.Mensaje = "Ocurrio un erro en la operacion";
                            return View("Modal");
                        }


                    }
                    //ML.Result result = BL.Usuario.AddEF(usuario);

                    //if (result.Correct)
                    //{
                    //    ViewBag.Mensaje = "Registro existoso";
                    //}
                    //else
                    //{
                    //    ViewBag.Mensaje = "Ocurrio un error" + result.Message;
                    //    return View("Modal");
                    //}

                }
                else
                {

                    using (var client = new HttpClient())
                    {

                        client.BaseAddress = new Uri("http://localhost:5050/");

                        var postTask = client.PostAsJsonAsync<ML.Usuario>("api/Usuario/Update", usuario);
                        postTask.Wait();

                        var resultService = postTask.Result;

                        if (resultService.IsSuccessStatusCode)
                        {

                            ViewBag.Mensaje = "Actualizacion Exitosa";
                            return View("Modal");
                        }
                        else
                        {

                            ViewBag.Mensaje = "Ocurrio un erro en la operacion";
                            return View("Modal");
                        }


                    }

                    //ML.Result result = BL.Usuario.UpdateEF(usuario);

                    //if (result.Correct)
                    //{
                    //    ViewBag.Mensaje = "Actualizacion existosa";

                    //}
                    //else
                    //{
                    //    ViewBag.Mensaje = "Ocurrio un error" + result.Message;
                    //    return View("Modal");
                    //}

                }

                return View("Modal");

            }
            else {

                ML.Result resultRol = BL.Rol.GetAllEF();
                ML.Result resultPais = BL.Pais.GetAllEF();
                ML.Result resultEstado = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Rol.Rols = resultRol.Objects;
                usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                return View(usuario);
            }

        }

        [HttpGet]
        public ActionResult Delete(int? Id_Usuario)
        {

            ML.Usuario usuario = new ML.Usuario();

            if (Id_Usuario == 0)
            {

                ViewBag.Mensaje = "No se encontro el registro en la base de datos";
                return View("Modal");

            }
            else
            {

                ML.Result resultApi = new ML.Result();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5050/");

                    var responseTask = client.GetAsync("api/Usuario/Delete/?IdUsuario=" + Id_Usuario);
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();                        

                        ViewBag.Mensaje = "Se elimino el usuario";

                    }
                    else {

                        ViewBag.Mensaje = "Ocurrio un error";

                    }
                }
                //ML.Result result = BL.Usuario.DeleteEF(Id_Usuario.Value);

                //if (result.Correct)
                //{
                //    ViewBag.Mensaje = "Se elimino el usuario" + result.Message;
                //}
                //else
                //{
                //    ViewBag.Mensaje = "Ocurrio un error" + result.Message;
                //    return View("Modal");
                //}


            }

            return View("Modal");

        }

        public ActionResult UpdateStatus(int IdUsuario) { 
        
            ML.Usuario usuario = new ML.Usuario();           
            ML.Result result = BL.Usuario.GetByIdEF(IdUsuario);

            if (result.Correct)
            {
                usuario = (ML.Usuario)result.Object;
                usuario.Status = (usuario.Status) ? false : true;
                ML.Result resultUpdate = BL.Usuario.UpdateEF(usuario);

                if (resultUpdate.Correct) {
                    ViewBag.Mensaje = "Se actualizo el status" + result.Message;
                }
                else {
                    ViewBag.Mensaje = "Ocurrio un error" + result.Message;
                    return View("Modal");
                }
            }
            else {
                
                ViewBag.Mensaje = "Ocurrio un error" + result.Message;
                return View("Modal");
            }

            return View("Modal");
        }

        public JsonResult EstadoGetByIdPais(int IdPais)
        {

            ML.Result result = BL.Estado.GetByIdPais(IdPais);

            return Json(result.Objects);
        }

        public JsonResult MunicipioGetByIdEstado(int IdEstado)
        {

            ML.Result result = BL.Municipio.GetByIdEstado(IdEstado);

            return Json(result.Objects);
        }

        public JsonResult ColoniaGetByIdMunicipio(int IdMunicipio)
        {

            ML.Result result = BL.Colonia.GetByIdMunicipio(IdMunicipio);

            return Json(result.Objects);
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
