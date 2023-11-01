using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Reclamo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = "";
        public DateOnly Fecha { get; set; }
        public int EmpresaId { get; set; }
    }
}
