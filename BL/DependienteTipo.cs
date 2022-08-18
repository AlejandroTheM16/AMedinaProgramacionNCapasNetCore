using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class DependienteTipo
    {
        public static ML.Result GetAllEF()

        {

            ML.Result result = new ML.Result();

            try
            {
                using (DL.AMedinaProgramacionNCapasContext context = new DL.AMedinaProgramacionNCapasContext())

                {

                    var dependientetipos = context.DependienteTipos.FromSqlRaw("DependienteTipoGetAll").ToList();

                    result.Objects = new List<object>();


                    if (dependientetipos != null)
                    {

                        foreach (var objDependienteTipo in dependientetipos)
                        {
                            ML.DependienteTipo dependienteTipo = new ML.DependienteTipo();

                            dependienteTipo.IdDependienteTipo = objDependienteTipo.IdDependienteTipo;
                            dependienteTipo.Nombre = objDependienteTipo.Nombre;


                            result.Objects.Add(dependienteTipo);

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
    }
}
