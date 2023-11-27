﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Reclamo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = "";
        public DateTime Fecha { get; set; }
        public int EmpresaId { get; set; }
        public Empresa? EmpresaAsociada { get; set; }
        public long OCId { get; set; }
        public Orden? OrdenAsociada { get; set; }
    }
}
