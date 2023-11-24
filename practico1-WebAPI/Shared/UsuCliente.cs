using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class UsuCliente : Usuario
    {
        public string? shipAdress { get; set; }

        public Opinion[]? Emitida { get; set; }

        public Empresa[]? Relacionada { get; set; }
    }
}
