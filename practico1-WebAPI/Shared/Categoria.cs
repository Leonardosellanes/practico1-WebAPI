using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public int? CategoriaId {  get; set; }
        public Categoria? CategoriaAsociada { get; set; }
        public int EmpresaId { get; set; }
        public List<Producto>? Productos { get; set; }

    }
}
