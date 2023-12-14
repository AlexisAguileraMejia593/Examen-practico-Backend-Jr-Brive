using System;
using System.Collections.Generic;

namespace DL;

public partial class Sucursal
{
    public int IdSucursal { get; set; }

    public string? Nombre { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
