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

        public List<Categoria>? Cat_asociadas { get; set; }

    }
}
