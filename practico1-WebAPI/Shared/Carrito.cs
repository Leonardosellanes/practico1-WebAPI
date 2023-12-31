﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Carrito
    {
        public long Id { get; set; }

        public int Cantidad { get; set; }

        public int ProductoId { get; set; }
        public Producto? POs { get; set; }

        public long OCId { get; set; }
        public Orden? OCs { get; set; }
    }
}
