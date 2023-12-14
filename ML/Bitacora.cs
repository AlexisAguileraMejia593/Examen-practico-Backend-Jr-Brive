using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Bitacora
    {
        public int IdBitacora { get; set; }
        public ML.Usuario? Usuario { get; set; }
        public ML.Producto? Producto { get; set; }
        public List<object>? Bitacoras { get; set; }
    }
}