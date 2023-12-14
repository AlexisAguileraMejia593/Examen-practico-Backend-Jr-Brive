namespace BL
{
    public class Sucursal
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenPrácticoBackendJrBriveContext context = new DL.ExamenPrácticoBackendJrBriveContext())
                {
                    var query = (from Sucurs in context.Sucursals
                                 select new
                                 {
                                     IdSucursal = Sucurs.IdSucursal,
                                     Nombre = Sucurs.Nombre,
                                     Direccion = Sucurs.Direccion,
                                     Telefono = Sucurs.Telefono
                                 });
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var registro in query)
                        {
                            ML.Sucursal sucursal = new ML.Sucursal();
                            sucursal.IdSucursal = registro.IdSucursal;
                            sucursal.Nombre = registro.Nombre;
                            sucursal.Direccion = registro.Direccion;
                            sucursal.Telefono = registro.Telefono;
                            result.Objects.Add(sucursal);
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
        public static ML.Result GetById(int IdSucursal)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenPrácticoBackendJrBriveContext context = new DL.ExamenPrácticoBackendJrBriveContext())
                {
                    var query = (from Sucurs in context.Sucursals
                                 where IdSucursal == Sucurs.IdSucursal
                                 select new
                                 {
                                     IdSucursal = Sucurs.IdSucursal,
                                     Nombre = Sucurs.Nombre,
                                     Direccion = Sucurs.Direccion,
                                     Telefono = Sucurs.Telefono
                                 });
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var registro in query)
                        {
                            ML.Sucursal sucursalresult = new ML.Sucursal();
                            sucursalresult.IdSucursal = registro.IdSucursal;
                            sucursalresult.Nombre = registro.Nombre;
                            sucursalresult.Direccion = registro.Direccion;
                            sucursalresult.Telefono = registro.Telefono;
                            result.Objects.Add(sucursalresult);

                            result.Object = sucursalresult;
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
        public static ML.Result Add(ML.Sucursal sucursal)
        {
            ML.Result result = new ML.Result();
            try
            {
                //todo lo que ejecute dentro de un using se libera al final
                using (DL.ExamenPrácticoBackendJrBriveContext context = new DL.ExamenPrácticoBackendJrBriveContext())
                {
                    DL.Sucursal sucursalentity = new DL.Sucursal();

                    sucursalentity.Nombre = sucursal.Nombre;
                    sucursalentity.Direccion = sucursal.Direccion;
                    sucursalentity.Telefono = sucursal.Telefono;
                    context.Sucursals.Add(sucursalentity);
                    int rowsAffected = context.SaveChanges();
                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al agregar la Sucursal";
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
        public static ML.Result Update(ML.Sucursal sucursal)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenPrácticoBackendJrBriveContext context = new DL.ExamenPrácticoBackendJrBriveContext())
                {
                    var query = context.Sucursals.SingleOrDefault(p => p.IdSucursal == sucursal.IdSucursal);
                    if (query != null)
                    {
                        query.Nombre = sucursal.Nombre;
                        query.Direccion = sucursal.Direccion;
                        query.Telefono = sucursal.Telefono;
                        // Actualiza aquí los demás campos del producto

                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Sucursal no encontrado";
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
        public static ML.Result Delete(int IdSucursal)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenPrácticoBackendJrBriveContext context = new DL.ExamenPrácticoBackendJrBriveContext())
                {
                    var query = (from a in context.Sucursals
                                 where a.IdSucursal == IdSucursal
                                 select a).First();
                    context.Sucursals.Remove(query);
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