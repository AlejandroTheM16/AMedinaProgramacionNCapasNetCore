using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Dependiente
    {
        public static ML.Result GetAllEF() {

            ML.Result result = new ML.Result();

            try
            {

                using (DL.AMedinaProgramacionNCapasContext context = new DL.AMedinaProgramacionNCapasContext()) {

                    var dependientes = context.Dependientes.FromSqlRaw($"DependienteGetAll").ToList();

                    result.Objects = new List<object>();

                    if (dependientes != null) {

                        foreach (var obj in dependientes)
                        {
                            ML.Dependiente dependiente = new ML.Dependiente();
                            dependiente.Empleado = new ML.Empleado();
                            dependiente.DependienteTipo = new ML.DependienteTipo();

                            dependiente.IdDependiente = obj.IdDependiente;
                            dependiente.Empleado.NumeroEmpleado = obj.IdEmpleado;
                            dependiente.Nombre = obj.Nombre;
                            dependiente.ApellidoPaterno = obj.ApellidoPaterno;
                            dependiente.ApellidoMaterno = obj.ApellidoMaterno;
                            dependiente.FechaNacimiento = obj.FechaNacimiento.ToString();
                            dependiente.EstadoCivil = obj.EstadoCivil;
                            dependiente.Genero = obj.Genero;
                            dependiente.Telefono = obj.Telefono;
                            dependiente.RFC = obj.Rfc;
                            dependiente.DependienteTipo.IdDependienteTipo = obj.IdDependienteTipo;

                            result.Objects.Add(dependiente);


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



        public static ML.Result Add(ML.Dependiente dependiente)
        {

            ML.Result result = new ML.Result();
            

            try
            {
                using (DL.AMedinaProgramacionNCapasContext context = new DL.AMedinaProgramacionNCapasContext())
                {

                    var dependientes = context.Database.ExecuteSqlRaw($"DependienteAdd '{dependiente.Empleado.NumeroEmpleado}','{dependiente.Nombre}','{dependiente.ApellidoPaterno}','{dependiente.ApellidoMaterno}'," +
                        $"'{dependiente.FechaNacimiento}','{dependiente.EstadoCivil}','{dependiente.Genero}','{dependiente.Telefono}','{dependiente.RFC}',{dependiente.DependienteTipo.IdDependienteTipo}");

                    if (dependientes >= 1)
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

        public static ML.Result Update(ML.Dependiente dependiente) {

            ML.Result result = new ML.Result();

            try
            {
                using (DL.AMedinaProgramacionNCapasContext context = new DL.AMedinaProgramacionNCapasContext()) {

                    var dependientes = context.Database.ExecuteSqlRaw($"DependienteUpdate {dependiente.IdDependiente},'{dependiente.Empleado.NumeroEmpleado}','{dependiente.Nombre}','{dependiente.ApellidoPaterno}','{dependiente.ApellidoMaterno}'," +
                        $"'{dependiente.FechaNacimiento}','{dependiente.EstadoCivil}','{dependiente.Genero}','{dependiente.Telefono}','{dependiente.RFC}',{dependiente.DependienteTipo.IdDependienteTipo}");

                    if (dependientes >= 1)
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

        public static ML.Result GetById(string IdEmpleado) { 

            ML.Result result = new ML.Result();

            try
            {
                using (DL.AMedinaProgramacionNCapasContext context = new DL.AMedinaProgramacionNCapasContext()) {

                    var dependientes = context.Dependientes.FromSqlRaw($"DependienteGetById '{IdEmpleado}'").ToList();

                    result.Objects = new List<object>();

                    if (dependientes != null) {

                        foreach (var obj in dependientes) {

                            ML.Dependiente dependiente = new ML.Dependiente();
                            dependiente.Empleado = new ML.Empleado();
                            dependiente.DependienteTipo = new ML.DependienteTipo();

                            dependiente.IdDependiente = obj.IdDependiente;
                            dependiente.Empleado.NumeroEmpleado = obj.IdEmpleado;
                            dependiente.Nombre = obj.Nombre;
                            dependiente.ApellidoPaterno = obj.ApellidoPaterno;
                            dependiente.ApellidoMaterno = obj.ApellidoMaterno;
                            dependiente.FechaNacimiento = obj.FechaNacimiento.ToString();
                            dependiente.EstadoCivil = obj.EstadoCivil;
                            dependiente.Genero = obj.Genero;
                            dependiente.Telefono = obj.Telefono;
                            dependiente.RFC = obj.Rfc;
                            dependiente.DependienteTipo.IdDependienteTipo = obj.IdDependienteTipo;
                            dependiente.DependienteTipo.Nombre = obj.NombreTipo;
                            dependiente.Empleado.Nombre = obj.NombreEmpleado;
                            dependiente.Empleado.ApellidoPaterno = obj.ApellidoPaternoEmpleado;
                            dependiente.Empleado.ApellidoMaterno = obj.ApellidoMaternoEmpleado;

                            result.Objects.Add(dependiente);

                        }
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


        public static ML.Result GetByIdDependiente(int IdDependiente)
        {

            ML.Result result = new ML.Result();

            try
            {
                using (DL.AMedinaProgramacionNCapasContext context = new DL.AMedinaProgramacionNCapasContext())
                {

                    var dependientes = context.Dependientes.FromSqlRaw($"DependienteGetByIdDependiente {IdDependiente}").AsEnumerable().FirstOrDefault();


                    if (dependientes != null)                         
                    {                      

                            ML.Dependiente dependiente = new ML.Dependiente();
                            dependiente.Empleado = new ML.Empleado();
                            dependiente.DependienteTipo = new ML.DependienteTipo();

                            dependiente.IdDependiente = dependientes.IdDependiente;
                            dependiente.Empleado.NumeroEmpleado = dependientes.IdEmpleado;
                            dependiente.Nombre = dependientes.Nombre;
                            dependiente.ApellidoPaterno = dependientes.ApellidoPaterno;
                            dependiente.ApellidoMaterno = dependientes.ApellidoMaterno;
                            dependiente.FechaNacimiento = dependientes.FechaNacimiento.ToString();
                            dependiente.EstadoCivil = dependientes.EstadoCivil;
                            dependiente.Genero = dependientes.Genero;
                            dependiente.Telefono = dependientes.Telefono;
                            dependiente.RFC = dependientes.Rfc;
                            dependiente.DependienteTipo.IdDependienteTipo = dependientes.IdDependienteTipo;
                            dependiente.DependienteTipo.Nombre = dependientes.NombreTipo;
                            dependiente.Empleado.Nombre = dependientes.NombreEmpleado;
                            dependiente.Empleado.ApellidoPaterno = dependientes.ApellidoPaternoEmpleado;
                            dependiente.Empleado.ApellidoMaterno = dependientes.ApellidoMaternoEmpleado;

                            result.Object = dependiente;
                        
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


    }
}
