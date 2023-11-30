using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Shared
{
    public class Orden
    {
        public long Id { get; set; }


        public string MedioDePago { get; set; } = "";

        public string DireccionDeEnvio { get; set; } = "";

        public DateTime FechaEstimadaEntrega { get; set; }

        public decimal Total { get; set; } = 0 ;

        public string EstadoOrden { get; set; } = "";

        public DateTime? Fecha { get; set; }

        public String? ClienteId { get; set; }

        public List<Carrito>? Carritos { get; set; }

        public Reclamo? Rcs { get; set; } = null;

        public UsuCliente? Cli { get; set; } = null;

        public int? EmpresaId { get; set; }

        public int? SucursalId { get; set; }
        
    }
}
