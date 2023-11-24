using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Empresa
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string RUT { get; set; } = "";

        public List<Categoria>? Categorias { get; set; }

        public List<Producto>? Productos{ get; set; }

        public List<Sucursal>? Sucursales { get; set; }
    }
}
