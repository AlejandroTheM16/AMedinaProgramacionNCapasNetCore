using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empresa
    {

        public static ML.Result GetAll() {

            ML.Result result = new ML.Result();


            try
            {
                using (DL.AMedinaProgramacionNCapasContext context = new DL.AMedinaProgramacionNCapasContext()) {

                    var empresas = context.Empresas.FromSqlRaw($"EmpresaGetAll");

                    result.Objects = new List<object>();

                    if (empresas != null) {

                        foreach (var obj in empresas) {

                            ML.Empresa empresa = new ML.Empresa();

                            empresa.IdEmpresa = obj.IdEmpresa;
                            empresa.Nombre = obj.Nombre;
                            empresa.Telefono = obj.Telefono;
                            empresa.DireccionWeb = obj.DireccionWeb;
                            empresa.Email = obj.Email;
                            empresa.Logo = obj.Logo;

                            result.Objects.Add(empresa);

                        }

                        result.Correct = true;

                    }
                }


            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;

            }

            return result;

        }

        public static ML.Result GetById(int IdEmpresa) {

            ML.Result result = new ML.Result();

            try
            {
                using (DL.AMedinaProgramacionNCapasContext context = new DL.AMedinaProgramacionNCapasContext()) {

                    var query = context.Empresas.FromSqlRaw($"EmpresaGetById {IdEmpresa}").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Empresa empresa = new ML.Empresa();

                        empresa.IdEmpresa = query.IdEmpresa;
                        empresa.Nombre = query.Nombre;
                        empresa.Telefono = query.Telefono;
                        empresa.Email = query.Email;
                        empresa.DireccionWeb = query.DireccionWeb;
                        empresa.Logo = query.Logo;

                        result.Object = empresa;                       

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

        public static ML.Result Add(ML.Empresa empresa) {

            ML.Result result = new ML.Result();
            StreamReader archivo = new StreamReader(@"C:\Users\digis\Documents\LayoutEmpresa.txt");

            string line;
            ML.Result resultErrores = new ML.Result();
            resultErrores.Objects = new List<object>();

            line = archivo.ReadLine();

            while ((line = archivo.ReadLine()) != null)
            {

                string[] datos = line.Split('|');

                //ML.Empresa empresa = new ML.Empresa();

                empresa.Nombre = datos[0];
                empresa.Telefono = datos[1];
                empresa.Email = datos[2];
                empresa.DireccionWeb = datos[3];

                result = BL.Empresa.Add(empresa);

                if (result.Correct == false)
                {

                    resultErrores.Objects.Add(
                        "No se inserto el Nombre: " + empresa.Nombre + "\n" +
                        "No se inserto el Telefono: " + empresa.Telefono + "\n" +
                        "No se inserto el Email: " + empresa.Email + "\n" +
                        "No se inserto la DireccionWeb: " + empresa.DireccionWeb + " " +
                        result.Message
                        );
                }

            }

            archivo.Close();

            if (resultErrores.Objects != null)
            {

                TextWriter tw = new StreamWriter(@"C:\Users\digis\Documents\ErroresCargaMasiva.txt");

                foreach (string error in resultErrores.Objects)
                {
                    tw.WriteLine(error);
                    Console.WriteLine(error);
                }

                tw.Close();

            }

            return result;

            //ML.Result result = new ML.Result();

            //try
            //{

            //    using (DL.AMedinaProgramacionNCapasContext context = new DL.AMedinaProgramacionNCapasContext())
            //    {

            //        var query = context.Database.ExecuteSqlRaw($"EmpresaAdd '{empresa.Nombre}','{empresa.Telefono}'," +
            //            $"'{empresa.Email}','{empresa.DireccionWeb}','{empresa.Logo}'");


            //        if (query >= 1)
            //        {
            //            result.Correct = true;
            //        }
            //        else
            //        {
            //            result.Correct = false;
            //        }

            //    }

            //}
            //catch (Exception Ex)
            //{

            //    result.Correct = false;
            //    result.Message = Ex.Message;
            //    result.Ex = Ex;
            //}

            //return result;

        }

        public static ML.Result Update(ML.Empresa empresa){

            ML.Result result = new ML.Result();

            try
            {

                using (DL.AMedinaProgramacionNCapasContext context = new DL.AMedinaProgramacionNCapasContext()) {

                    var query = context.Database.ExecuteSqlRaw($"EmpresaUpdate {empresa.IdEmpresa},'{empresa.Nombre}','{empresa.Telefono}'," +
                        $"'{empresa.Email}','{empresa.DireccionWeb}','{empresa.Logo}'");

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

        public static ML.Result Delete(ML.Empresa empresa) {

            ML.Result result = new ML.Result();

            try
            {

                using (DL.AMedinaProgramacionNCapasContext context = new DL.AMedinaProgramacionNCapasContext()) {

                    var query = context.Database.ExecuteSqlRaw($"EmpresaDelete {empresa.IdEmpresa}");

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
    }
}
