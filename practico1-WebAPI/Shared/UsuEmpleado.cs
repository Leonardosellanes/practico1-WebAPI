using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class UsuEmpleado : Usuario
    {
        public Boolean? isAdmin { get; set; }

        public Empresa[] Empleadora { get; set; }

    }
}
