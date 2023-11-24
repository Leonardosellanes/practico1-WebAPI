﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class OC
    {
        public long Id { get; set; }


        public string MedioDePago { get; set; } 

        public string DireccionDeEnvio { get; set; } 

        public DateTime FechaEstimadaEntrega { get; set; }

        public decimal Total { get; set; }

        public string EstadoOrden { get; set; } 

        public DateTime Fecha { get; set; }

        public List<Carrito> Carritos { get; set; }

        public Reclamo[] Rcs { get; set; }

        public Factura[] FAs { get; set; }

        public UsuCliente[] Cli { get; set; }

    }
}
