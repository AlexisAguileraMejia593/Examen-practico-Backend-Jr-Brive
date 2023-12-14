using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public int IdUsuarios { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public string? CorreoElectronico { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Bitacora> Bitacoras { get; set; } = new List<Bitacora>();
}
