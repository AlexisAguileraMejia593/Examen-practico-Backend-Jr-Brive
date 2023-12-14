using System;
using System.Collections.Generic;

namespace DL;

public partial class Producto
{
    public int IdProductos { get; set; }

    public string? Nombre { get; set; }

    public string? Sdk { get; set; }

    public int? IdSucursal { get; set; }

    public virtual ICollection<Bitacora> Bitacoras { get; set; } = new List<Bitacora>();

    public virtual Sucursal? IdSucursalNavigation { get; set; }
}
