using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Sucursal
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Ubicacion { get; set; } = string.Empty;
        public int TiempoEntrega { get; set; }
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; } 
    }
}
