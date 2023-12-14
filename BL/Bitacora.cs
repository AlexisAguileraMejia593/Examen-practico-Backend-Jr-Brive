using System.Reflection.Metadata;

namespace BL
{
    public class Bitacora
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenPrácticoBackendJrBriveContext context = new DL.ExamenPrácticoBackendJrBriveContext())
                {
                    var query = (from bitacor in context.Bitacoras
                                 join product in context.Productos on bitacor.IdProductos equals product.IdProductos
                                 join users in context.Usuarios on bitacor.IdUsuarios equals users.IdUsuarios
                                 join sucurs in context.Sucursals on product.IdSucursal equals sucurs.IdSucursal
                                 select new
                                 {
                                     IdBitacora = bitacor.IdBitacora,
                                     IdUsuarios = users.IdUsuarios,
                                     NombreUsuarios = users.Nombre,
                                     ApellidoPaterno = users.ApellidoPaterno,
                                     ApellidoMaterno = users.ApellidoMaterno,
                                     CorreoElectronico = users.CorreoElectronico,
                                     Password = users.Password,
                                     IdProductos = product.IdProductos,
                                     NombreProductos = product.Nombre,
                                     SDK = product.Sdk,
                                     IdSucuarsal = sucurs.IdSucursal,
                                     NombreSucursal = sucurs.Nombre,
                                     Direccion = sucurs.Direccion,
                                     Telefono = sucurs.Telefono
                                 });
                    if (query != null )
                    {
                        result.Objects = new List<object>();
                        foreach ( var registro in query)
                        {
                            ML.Bitacora bitacora = new ML.Bitacora();
                            bitacora.IdBitacora = registro.IdBitacora;
                            bitacora.Usuario = new ML.Usuario();
                            bitacora.Usuario.IdUsuario = registro.IdUsuarios;
                            bitacora.Usuario.Nombre = registro.NombreUsuarios;
                            bitacora.Usuario.ApellidoPaterno = registro.ApellidoPaterno;
                            bitacora.Usuario.ApellidoMaterno = registro.ApellidoMaterno;
                            bitacora.Usuario.CorreoElectronico = registro.CorreoElectronico;
                            bitacora.Usuario.Password = registro.Password;
                            bitacora.Producto = new ML.Producto();
                            bitacora.Producto.IdProducto = registro.IdProductos;
                            bitacora.Producto.Nombre = registro.NombreProductos;
                            bitacora.Producto.SDK = registro.SDK;
                            bitacora.Producto.Sucursal = new ML.Sucursal();
                            bitacora.Producto.Sucursal.IdSucursal = registro.IdSucuarsal;
                            bitacora.Producto.Sucursal.Nombre = registro.NombreSucursal;
                            bitacora.Producto.Sucursal.Direccion = registro.Direccion;
                            bitacora.Producto.Sucursal.Telefono = registro.Telefono;
                            result.Objects.Add( bitacora );
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            } catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
            }
            return result;
        }
        public static ML.Result GetById(int IdProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenPrácticoBackendJrBriveContext context = new DL.ExamenPrácticoBackendJrBriveContext())
                {
                    var query = (from product in context.Productos
                                 join Sucurs in context.Sucursals on product.IdSucursal equals Sucurs.IdSucursal
                                 where IdProducto == product.IdProductos
                                 select new
                                 {
                                     IdProductos = product.IdProductos,
                                     NombreProductos = product.Nombre,
                                     SDK = product.Sdk,
                                     IdSucursal = Sucurs.IdSucursal,
                                     NombreSucursal = Sucurs.Nombre,
                                     Direccion = Sucurs.Direccion,
                                     Telefono = Sucurs.Telefono
                                 });
                    if( query != null )
                    {
                        result.Objects = new List<object>();
                        foreach ( var registro in query )
                        {
                            ML.Producto productoresult = new ML.Producto();
                            productoresult.IdProducto = registro.IdProductos;
                            productoresult.Nombre = registro.NombreProductos;
                            productoresult.SDK = registro.SDK;
                            productoresult.Sucursal = new ML.Sucursal();
                            productoresult.Sucursal.IdSucursal = registro.IdSucursal;
                            productoresult.Sucursal.Nombre = registro.NombreSucursal;
                            result.Objects.Add( productoresult );

                            result.Object = productoresult;
                            result.Correct = true;
                        }
                        result.Correct = true;
                    }
                }
            } catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
            }
            return result;
        }
        public static ML.Result Add(ML.Producto producto, int UserID)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenPrácticoBackendJrBriveContext context = new DL.ExamenPrácticoBackendJrBriveContext())
                {
                    DL.Producto productoentity = new DL.Producto();

                    productoentity.Nombre = producto.Nombre;
                    productoentity.Sdk = producto.SDK;
                    productoentity.IdSucursal = producto.Sucursal.IdSucursal;
                    context.Productos.Add(productoentity);
                    int rowsAffected = context.SaveChanges();

                    if (rowsAffected > 0)
                    {
                        // Crear una nueva entrada en la bitácora
                        DL.Bitacora bitacoraentity = new DL.Bitacora();
                        bitacoraentity.IdUsuarios = UserID;
                        bitacoraentity.IdProductos = productoentity.IdProductos; // El ID del producto recién creado
                        context.Bitacoras.Add(bitacoraentity);
                        rowsAffected = context.SaveChanges();

                        if (rowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Error al agregar la entrada de la bitácora";
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al agregar el Producto";
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
        public static ML.Result Update(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenPrácticoBackendJrBriveContext context = new DL.ExamenPrácticoBackendJrBriveContext())
                {
                    var query = context.Productos.SingleOrDefault(p => p.IdProductos == producto.IdProducto);
                    if (query != null)
                    {
                        query.Nombre = producto.Nombre;
                        query.Sdk = producto.SDK;
                        query.IdSucursal = producto.Sucursal.IdSucursal;
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
        public static ML.Result Delete(int IdProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenPrácticoBackendJrBriveContext context = new DL.ExamenPrácticoBackendJrBriveContext())
                {
                    var query = (from a in context.Productos
                                 where a.IdProductos == IdProducto
                                 select a).First();
                    context.Productos.Remove(query);
                    int rowAffected = context.SaveChanges();
                    if (rowAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct= false;
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