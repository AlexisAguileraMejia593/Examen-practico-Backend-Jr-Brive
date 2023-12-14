using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenPrácticoBackendJrBriveContext context = new DL.ExamenPrácticoBackendJrBriveContext())
                {
                    var query = (from users in context.Usuarios
                                 select new
                                 {
                                     IdUsuarios = users.IdUsuarios,
                                     Nombre = users.Nombre,
                                     ApellidoPaterno = users.ApellidoPaterno,
                                     ApellidoMaterno = users.ApellidoMaterno,
                                     CorreoElectronico = users.CorreoElectronico,
                                     Password = users.Password
                                 });
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var registro in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = registro.IdUsuarios;
                            usuario.Nombre = registro.Nombre;
                            usuario.ApellidoPaterno = registro.ApellidoPaterno;
                            usuario.ApellidoMaterno = registro.ApellidoMaterno;
                            usuario.CorreoElectronico = registro.CorreoElectronico;
                            usuario.Password = registro.Password;
                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
            }
            return result;
        }
        public static ML.Result GetByEmail(string Email)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenPrácticoBackendJrBriveContext context = new DL.ExamenPrácticoBackendJrBriveContext())
                {
                    var query = (from user in context.Usuarios
                                 where Email == user.CorreoElectronico
                                 select new
                                 {
                                     IdUsuario = user.IdUsuarios,
                                     Nombre = user.Nombre,
                                     ApellidoPaterno = user.ApellidoPaterno,
                                     ApellidoMaterno = user.ApellidoMaterno,
                                     CorreoElectronico = user.CorreoElectronico,
                                     Password = user.Password
                                 });
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var registro in query)
                        {
                            ML.Usuario usuarioresult = new ML.Usuario();
                            usuarioresult.IdUsuario = registro.IdUsuario;
                            usuarioresult.Nombre = registro.Nombre;
                            usuarioresult.ApellidoPaterno = registro.ApellidoPaterno;
                            usuarioresult.ApellidoMaterno = registro.ApellidoMaterno;
                            usuarioresult.CorreoElectronico = registro.CorreoElectronico;
                            usuarioresult.Password = registro.Password;
                            result.Objects.Add(usuarioresult);

                            result.Object = usuarioresult;
                            result.Correct = true;
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
            }
            return result;
        }
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                //todo lo que ejecute dentro de un using se libera al final
                using (DL.ExamenPrácticoBackendJrBriveContext context = new DL.ExamenPrácticoBackendJrBriveContext())
                {
                    DL.Usuario usuarioentity = new DL.Usuario();

                    usuarioentity.Nombre = usuario.Nombre;
                    usuarioentity.ApellidoPaterno = usuario.ApellidoPaterno;
                    usuarioentity.ApellidoMaterno = usuario.ApellidoMaterno;
                    usuarioentity.CorreoElectronico = usuario.CorreoElectronico;
                    usuarioentity.Password = usuario.Password;
                    context.Usuarios.Add(usuarioentity);
                    int rowsAffected = context.SaveChanges();
                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al agregar el Usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenPrácticoBackendJrBriveContext context = new DL.ExamenPrácticoBackendJrBriveContext())
                {
                    var query = context.Usuarios.SingleOrDefault(p => p.IdUsuarios == usuario.IdUsuario);
                    if (query != null)
                    {
                        query.Nombre = usuario.Nombre;
                        query.ApellidoPaterno = usuario.ApellidoPaterno;
                        query.ApellidoMaterno = usuario.ApellidoMaterno;
                        query.CorreoElectronico = usuario.CorreoElectronico;
                        query.Password = usuario.Password;
                        // Actualiza aquí los demás campos del producto

                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Producto no encontrado";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenPrácticoBackendJrBriveContext context = new DL.ExamenPrácticoBackendJrBriveContext())
                {
                    var query = (from a in context.Usuarios
                                 where a.IdUsuarios == IdUsuario
                                 select a).First();
                    context.Usuarios.Remove(query);
                    int rowAffected = context.SaveChanges();
                    if (rowAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
            }
            return result;
        }
    }
}
