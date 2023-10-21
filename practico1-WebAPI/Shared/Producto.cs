using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Producto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string Foto { get; set; } = "";
        public int Precio { get; set; }
        public string Tipo_iva { get; set; } = "";
        public string Pdf { get; set; } = "";
        public int CategoriaId {  get; set; }
        public Categoria? Categoria { get; set; }
    }
}
