using System;
using System.Collections.Generic;

namespace DL;

public partial class Bitacora
{
    public int IdBitacora { get; set; }

    public int? IdUsuarios { get; set; }

    public int? IdProductos { get; set; }

    public virtual Producto? IdProductosNavigation { get; set; }

    public virtual Usuario? IdUsuariosNavigation { get; set; }
}
