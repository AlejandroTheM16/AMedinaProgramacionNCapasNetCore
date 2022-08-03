using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empleado
    {
        public static ML.Result GetAll() { 
        
            ML.Result result = new ML.Result();

            try
            {

                using (DL.AMedinaProgramacionNCapasContext context = new DL.AMedinaProgramacionNCapasContext()) {

                    var query = context.Empleados.FromSqlRaw($"EmpleadoGetAll");

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Empleado empleado = new ML.Empleado();
                            empleado.Empresa = new ML.Empresa();

                            empleado.NumeroEmpleado = obj.NumeroEmpleado;
                            empleado.RFC = obj.Rfc;
                            empleado.Nombre = obj.Nombre;
                            empleado.ApellidoPaterno = obj.ApellidoPaterno;
                            empleado.ApellidoMaterno = obj.ApellidoMaterno;
                            empleado.Email = obj.Email;
                            empleado.Telefono = obj.Telefono;
                            empleado.FechaNacimiento = obj.FechaNacimiento.ToString("dd-MM-yyyy");
                            empleado.NSS = obj.Nss;
                            empleado.FechaIngreso = obj.FechaIngreso.ToString("dd-MM-yyyy");
                            empleado.Foto = obj.Foto;
                            empleado.Empresa.IdEmpresa = obj.IdEmpresa;
                            empleado.Empresa.Nombre = obj.NombreEmpresa;

                            result.Objects.Add(empleado);

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


        public static ML.Result GetById(string NumeroEmpleado) { 

            ML.Result result = new ML.Result();

            try
            {

                using (DL.AMedinaProgramacionNCapasContext context = new DL.AMedinaProgramacionNCapasContext()) {

                    var query = context.Empleados.FromSqlRaw($"EmpleadoGetById '{NumeroEmpleado}'").AsEnumerable().FirstOrDefault();

                    if (query != null) { 
                    
                        ML.Empleado empleado = new ML.Empleado();
                        empleado.Empresa = new ML.Empresa();

                        empleado.NumeroEmpleado = query.NumeroEmpleado;
                        empleado.RFC = query.Rfc;
                        empleado.Nombre = query.Nombre;
                        empleado.ApellidoPaterno = query.ApellidoPaterno;
                        empleado.ApellidoMaterno = query.ApellidoMaterno;
                        empleado.Email = query.Email;
                        empleado.Telefono = query.Telefono;
                        empleado.FechaNacimiento = query.FechaNacimiento.ToString("dd-MM-yyyy");
                        empleado.NSS = query.Nss;
                        empleado.FechaIngreso = query.FechaIngreso.ToString("dd-MM-yyyy");
                        empleado.Foto = query.Foto;
                        empleado.Empresa.IdEmpresa = query.IdEmpresa;
                        empleado.Empresa.Nombre = query.NombreEmpresa;

                        result.Object = empleado;
                    }

                    result.Correct = true;
                
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

        public static ML.Result Add(ML.Empleado empleado) {

            ML.Result result = new ML.Result();

            try
            {
                using (DL.AMedinaProgramacionNCapasContext context = new DL.AMedinaProgramacionNCapasContext()) {

                    var query = context.Database.ExecuteSqlRaw($"EmpleadoAdd '{empleado.NumeroEmpleado}','{empleado.RFC}','{empleado.Nombre}','{empleado.ApellidoPaterno}','{empleado.ApellidoMaterno}','{empleado.Email}'," +
                        $"'{empleado.Telefono}','{empleado.FechaNacimiento}','{empleado.NSS}','{empleado.FechaIngreso}','{empleado.Foto}',{empleado.Empresa.IdEmpresa}");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else {

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

        public static ML.Result Update(ML.Empleado empleado) {

            ML.Result result = new ML.Result();

            try
            {
                using (DL.AMedinaProgramacionNCapasContext context = new DL.AMedinaProgramacionNCapasContext()) {

                    var query = context.Database.ExecuteSqlRaw($"EmpleadoUpdate '{empleado.NumeroEmpleado}','{empleado.RFC}','{empleado.Nombre}','{empleado.ApellidoPaterno}','{empleado.ApellidoMaterno}','{empleado.Email}'," +
                        $"'{empleado.Telefono}','{empleado.FechaNacimiento}','{empleado.NSS}','{empleado.FechaIngreso}','{empleado.Foto}',{empleado.Empresa.IdEmpresa}");

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
    }
}
