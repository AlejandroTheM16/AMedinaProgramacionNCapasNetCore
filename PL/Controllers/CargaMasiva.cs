using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class CargaMasiva : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly IHostingEnvironment _hostingEnvironment;

        public CargaMasiva(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpGet]
        public IActionResult CargaMasivaEmpresa()
        {
            ML.Result result = new ML.Result();
            return View(result);
        }

        [HttpPost]
        public IActionResult CargaMasivaEmpresa(ML.Empresa empresa)
        {
            IFormFile archivo = Request.Form.Files["FileExcel"];

            if (HttpContext.Session.GetString("PathArchivo") == null) //Validar si no existe un archivo
            {

                if (archivo != null)
                {
                    if (archivo.Length > 0)
                    {
                        string FileName = Path.GetFileName(archivo.FileName);
                        string folderPath = _configuration["PathFolder:value"];
                        string extensioArchivo = Path.GetExtension(archivo.FileName).ToLower();
                        string extensionModulo = _configuration["TipoExcel"];  //Varible Globla

                        if (extensioArchivo == extensionModulo)
                        {
                            string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, folderPath, Path.GetFileNameWithoutExtension(FileName)) + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                            if (!System.IO.File.Exists(filePath))
                            {
                                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                                {
                                    archivo.CopyTo(stream);  //Guardar una copia de mi archivo
                                }

                                string connectionString = _configuration["ConnectionStringExcel:value"] + filePath;

                                ML.Result resultEmpresas = BL.Empresa.ConvertirExcelDataTable(connectionString);

                                if (resultEmpresas.Correct)
                                {
                                    ML.Result resultValidacion = BL.Empresa.ValidarExcel(resultEmpresas.Objects);

                                    if (resultValidacion.Objects.Count == 0)  //No Errores
                                    {
                                        resultValidacion.Correct = true;
                                        HttpContext.Session.SetString("PathArchivo", filePath);
                                    }

                                    return View(resultValidacion);
                                }
                                else
                                {
                                    ViewBag.Message = "No se encontraron registros / Tenia Errores";
                                }
                            }
                        }
                        else
                        {
                            ViewBag.Message = "Seleccione un archivo valido (.xlsx)";
                        }


                    }
                    else
                    {
                        ViewBag.Mesaage = "No tiene datos el archivo";
                    }

                }
                else
                {
                    ViewBag.Mesaage = "Seleccione un archivo";
                }

            }
            else {

                string rutaArchivoExcel = HttpContext.Session.GetString("PathArchivo");
                string connectionString = _configuration["ConnectionStringExcel:value"] + rutaArchivoExcel;

                ML.Result resultData = BL.Empresa.ConvertirExcelDataTable(connectionString);
                if (resultData.Correct)
                {
                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = new List<Object>();

                    foreach (ML.Empresa empresaItem in resultData.Objects)
                    {
                        ML.Result resultAdd = BL.Empresa.Add(empresaItem);
                        if (!resultAdd.Correct)
                        {
                            resultErrores.Objects.Add("No se inserto la empresa con el nombre:  " + empresa.Nombre + "No se se inserto la empresa con el telefono: " + empresa.Telefono+
                                "No se se inserto la empresa con el email: " + empresa.Email + "No se se inserto la empresa con la direccion web: " + empresa.DireccionWeb);
                        }
                    }

                    if (resultErrores.Objects.Count > 0)
                    {
                        string folderError = _configuration["PathFolderError:value"];
                        string fileError = Path.Combine(_hostingEnvironment.WebRootPath, folderError + @"\logErrores.txt");
                        using (StreamWriter writer = new StreamWriter(fileError))
                        {
                            foreach (string ln in resultErrores.Objects)
                            {
                                writer.WriteLine(ln);
                            }
                        }
                        ViewBag.Message = "Algunas empresas no han sido registradas correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "Las Empresas han sido registradas correctamente";
                    }

                }

            }

            return View();
        }
    }
}
