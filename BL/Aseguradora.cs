using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Aseguradora
    {
        public static ML.Result AddEF(ML.Aseguradora aseguradora) { 
        
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AMedinaProgramacionNCapasContext context = new DL.AMedinaProgramacionNCapasContext()) {

                    var query = context.Database.ExecuteSqlRaw($"AseguradoraAdd '{aseguradora.Nombre}',{aseguradora.Usuario.Id_Usuario},'{aseguradora.Imagen}'");               
                }


            }
            catch (Exception ex) {

                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }

            return result;
        
        }

        public static ML.Result GetAllEF()

        {

            ML.Result result = new ML.Result();

            try
            {
                using (DL.AMedinaProgramacionNCapasContext context = new DL.AMedinaProgramacionNCapasContext())

                {

                    var aseguradoras = context.Aseguradoras.FromSqlRaw($"AseguradoraGetAll").ToList();

                    result.Objects = new List<object>();


                    if (aseguradoras != null)
                    {

                        foreach (var obj in aseguradoras)
                        {
                            ML.Aseguradora aseguradora = new ML.Aseguradora();
                            aseguradora.Usuario = new ML.Usuario();

                            aseguradora.IdAseguradora = obj.IdAseguradora;
                            aseguradora.Nombre = obj.Nombre;
                            aseguradora.Imagen = obj.Imagen;
                            aseguradora.FechaCreacion = obj.FechaCreacion.ToString("dd-MM-yyyy");
                            aseguradora.FechaModificacion = obj.FechaModificacion.ToString("dd-MM-yyyy");
                            aseguradora.Usuario.Id_Usuario = obj.IdUsuario;
                            aseguradora.Usuario.UserName = obj.NombreUsuario;
                           

                            result.Objects.Add(aseguradora);

                        }

                        result.Correct = true;
                    }

                }

            }
            catch (Exception Ex)
            {

                result.Correct = false;
                result.Message = Ex.Message;
                result.Ex = Ex;
            }

            return result;
        }

        

        public static ML.Result GetByIdEF(int Id)
        {

            ML.Result result = new ML.Result();

            try
            {
                using (DL.AMedinaProgramacionNCapasContext context = new DL.AMedinaProgramacionNCapasContext())

                {

                    var aseguradoras = context.Aseguradoras.FromSqlRaw($"AseguradoraGetById {Id}").AsEnumerable().FirstOrDefault();


                    if (aseguradoras != null)
                    {


                        ML.Aseguradora aseguradora = new ML.Aseguradora();
                        aseguradora.Usuario = new ML.Usuario();

                        aseguradora.IdAseguradora = aseguradoras.IdAseguradora;
                        aseguradora.Nombre = aseguradoras.Nombre;
                        aseguradora.Imagen = aseguradora.Imagen;
                        aseguradora.FechaCreacion = aseguradoras.FechaCreacion.ToString("dd-MM-yyyy");
                        aseguradora.FechaModificacion = aseguradoras.FechaModificacion.ToString("dd-MM-yyyy");
                        aseguradora.Usuario.Id_Usuario = aseguradoras.IdUsuario;
                        aseguradora.Usuario.UserName = aseguradoras.NombreUsuario;

                        result.Object = aseguradora;



                        result.Correct = true;
                    }

                }

            }
            catch (Exception Ex)
            {

                result.Correct = false;
                result.Message = Ex.Message;
                result.Ex = Ex;
            }

            return result;
        }

        public static ML.Result UpdateEF(ML.Aseguradora aseguradora)
        {

            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMedinaProgramacionNCapasContext context = new DL.AMedinaProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AseguradoraUpdate {aseguradora.IdAseguradora},'{aseguradora.Nombre}',{aseguradora.Usuario.Id_Usuario}," +
                        $"´{aseguradora.Imagen}´");


                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }

            }
            catch (Exception Ex)
            {

                result.Correct = false;
                result.Message = Ex.Message;
                result.Ex = Ex;
            }

            return result;
        }

        public static ML.Result DeleteEF(ML.Aseguradora aseguradora)
        {

            ML.Result result = new ML.Result();

            try
            {
                using (DL.AMedinaProgramacionNCapasContext context = new DL.AMedinaProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AseguradoraDelete {aseguradora}");

                    if (query != null)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }

            }
            catch (Exception Ex)
            {

                result.Correct = false;
                result.Message = Ex.Message;
                result.Ex = Ex;
            }

            return result;
        }
    }
}
