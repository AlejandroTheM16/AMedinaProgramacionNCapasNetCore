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
    public class Usuario
    {
        public static ML.Result GetAllEF(ML.Usuario usuarioBusquedaAbierta)

        {

            ML.Result result = new ML.Result();

            try
            {
                using (DL.AMedinaProgramacionNCapasContext context = new DL.AMedinaProgramacionNCapasContext())

                {

                    var usuarios = context.Usuarios.FromSqlRaw($"UsuarioGetAll '{usuarioBusquedaAbierta.Nombre}','{usuarioBusquedaAbierta.ApellidoPaterno}'," +
                        $"'{usuarioBusquedaAbierta.ApellidoMaterno}'").ToList();

                    result.Objects = new List<object>();


                    if (usuarios != null)
                    {

                        foreach (var obj in usuarios)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.Rol = new ML.Rol();
                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

                            usuario.Id_Usuario = obj.IdUsuario;
                            usuario.Nombre = obj.Nombre;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.Genero = obj.Genero;
                            usuario.Correo = obj.Correo;
                            usuario.Telefono = obj.Telefono;                          
                            usuario.FechaNacimiento = obj.FechaDeNacimiento.ToString("dd-MM-yyyy");
                            //usuario.IdRol = int.Parse(dr[10].ToString());
                            //usuario.Rol.IdRol = obj.Idrol.Value;
                            usuario.UserName = obj.UserName;
                            usuario.Password = obj.Password;
                            usuario.CURP = obj.Curp;
                            usuario.Imagen = obj.Imagen;
                            usuario.Rol.IdRol = obj.Idrol;
                            usuario.Rol.Nombre = obj.NombreRol;
                            usuario.Status = obj.Status;
                            usuario.Direccion.Calle = obj.NombreCalle;
                            usuario.Direccion.NumeroInterior = obj.NumeroInterior;
                            usuario.Direccion.NumeroExterior = obj.NumeroExterior;
                            usuario.Direccion.Colonia.IdColonia = obj.IdColonia;
                            usuario.Direccion.Colonia.Nombre = obj.NombreColonia;
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = obj.IdMunicipio;
                            usuario.Direccion.Colonia.Municipio.Nombre = obj.NombreMunicipio;
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = obj.IdEstado;
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = obj.NombreEstado;
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = obj.IdPais;
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = obj.NombrePais;


                            result.Objects.Add(usuario);

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
                    var ObjUsuario = context.Usuarios.FromSqlRaw($"UsuarioGetById {Id}").AsEnumerable().FirstOrDefault();
                    

                    if (ObjUsuario != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Rol = new ML.Rol();
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

                        usuario.Id_Usuario = ObjUsuario.IdUsuario;
                        usuario.Nombre = ObjUsuario.Nombre;
                        usuario.ApellidoPaterno = ObjUsuario.ApellidoPaterno;
                        usuario.ApellidoMaterno = ObjUsuario.ApellidoMaterno;
                        usuario.Genero = ObjUsuario.Genero;
                        usuario.Correo = ObjUsuario.Correo;
                        usuario.Telefono = ObjUsuario.Telefono;
                        usuario.FechaNacimiento = ObjUsuario.FechaDeNacimiento.ToString("dd-MM-yyyy");
                        usuario.Rol.IdRol = ObjUsuario.Idrol;
                        usuario.Rol.Nombre = ObjUsuario.NombreRol;
                        usuario.UserName = ObjUsuario.UserName;
                        usuario.Password = ObjUsuario.Password;
                        usuario.CURP = ObjUsuario.Curp;
                        usuario.Imagen = ObjUsuario.Imagen;
                        usuario.Status = ObjUsuario.Status;
                        usuario.Direccion.Calle = ObjUsuario.NombreCalle;
                        usuario.Direccion.NumeroInterior = ObjUsuario.NumeroInterior;
                        usuario.Direccion.NumeroExterior = ObjUsuario.NumeroExterior;
                        usuario.Direccion.Colonia.IdColonia = ObjUsuario.IdColonia;
                        usuario.Direccion.Colonia.Nombre = ObjUsuario.NombreColonia;
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = ObjUsuario.IdMunicipio;
                        usuario.Direccion.Colonia.Municipio.Nombre = ObjUsuario.NombreMunicipio;
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = ObjUsuario.IdEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = ObjUsuario.NombreEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = ObjUsuario.IdPais;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = ObjUsuario.NombrePais;

                        result.Object = usuario;

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

        public static ML.Result AddEF(ML.Usuario usuario)
        {

            ML.Result result = new ML.Result();

            try
            {
                using (DL.AMedinaProgramacionNCapasContext context = new DL.AMedinaProgramacionNCapasContext())
                {
                    

                    var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}','{usuario.ApellidoPaterno}','{usuario.ApellidoPaterno}','{usuario.Genero}'," +
                        $"'{usuario.Correo}','{usuario.Telefono}','{usuario.FechaNacimiento}',{usuario.Rol.IdRol},'{usuario.UserName}','{usuario.Password}','{usuario.CURP}'," +
                        $"'{usuario.Imagen}',{usuario.Status==true},'{usuario.Direccion.Calle}','{usuario.Direccion.NumeroInterior}','{usuario.Direccion.NumeroExterior}',{usuario.Direccion.Colonia.IdColonia}");


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

        public static ML.Result UpdateEF(ML.Usuario usuario)
        {

            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMedinaProgramacionNCapasContext context = new DL.AMedinaProgramacionNCapasContext())
                {
                    

                    var query = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.Id_Usuario},'{usuario.Nombre}','{usuario.ApellidoPaterno}','{usuario.ApellidoMaterno}','{usuario.Genero}'," +
                       $"'{usuario.Correo}','{usuario.Telefono}','{usuario.FechaNacimiento}',{usuario.Rol.IdRol},'{usuario.UserName}','{usuario.Password}','{usuario.CURP}','{usuario.Imagen}',{usuario.Status}," +
                       $"'{usuario.Direccion.Calle}','{usuario.Direccion.NumeroInterior}','{usuario.Direccion.NumeroExterior}',{usuario.Direccion.Colonia.IdColonia}");

                
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

        public static ML.Result DeleteEF(int Id_Usuario)
        {

            ML.Result result = new ML.Result();

            try
            {
                using (DL.AMedinaProgramacionNCapasContext context = new DL.AMedinaProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioDelete {Id_Usuario}");

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
