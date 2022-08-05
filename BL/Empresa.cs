using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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

            try
            {

                using (DL.AMedinaProgramacionNCapasContext context = new DL.AMedinaProgramacionNCapasContext())
                {

                    var query = context.Database.ExecuteSqlRaw($"EmpresaAdd '{empresa.Nombre}','{empresa.Telefono}'," +
                        $"'{empresa.Email}','{empresa.DireccionWeb}','{empresa.Logo}'");


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

        public static ML.Result ConvertirExcelDataTable(string connectionString)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(connectionString))
                {
                    string query = "SELECT * FROM []";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;

                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tableEmpresa = new DataTable();

                        da.Fill(tableEmpresa);

                        if (tableEmpresa.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tableEmpresa.Rows)
                            {
                                ML.Empresa empresa = new ML.Empresa();
                                empresa.Nombre = row[0].ToString();
                                empresa.Telefono = row[1].ToString();
                                empresa.Email = row[2].ToString();                                
                                empresa.DireccionWeb = row[3].ToString();
                                //empresa.Logo = row[4].ToString();


                                result.Objects.Add(empresa);
                            }
                            result.Correct = true;
                        }
                        result.Object = tableEmpresa;

                        if (tableEmpresa.Rows.Count > 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.Message = "No existen registros en el excel";
                        }

                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.Message = ex.Message;

            }
            return result;
        }

        public static ML.Result ValidarExcel(List<object> Objects)
        {
            ML.Result result = new ML.Result();
            try
            {
                result.Objects = new List<object>();
                int i = 1;

                foreach (ML.Empresa empresa in Objects)
                {
                    ML.ExcelErrores error = new ML.ExcelErrores();
                    error.IdRegistro = i;

                    if (empresa.Nombre == "")
                    {
                        error.Message += "Ingrese el nombre ";
                    }
                    if (empresa.Telefono == "")
                    {
                        error.Message += "Ingrese el telefono";
                    }
                    if (empresa.Email == "")
                    {
                        error.Message += "Ingrese email";
                    }
                    if (empresa.DireccionWeb == "")
                    {
                        error.Message += "Ingrese direccion web";
                    }
                   

                    if (error.Message != null)
                    {
                        result.Objects.Add(error);
                    }

                    i++;
                }

            }
            catch (Exception)
            {

                throw;
            }




            return result;
        }
    }
}
