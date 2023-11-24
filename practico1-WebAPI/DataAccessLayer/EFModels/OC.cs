using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EFModels
{
    public class OC
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }

        [MaxLength(128), MinLength(3), Required]
        public string MedioDePago { get; set; } = "";

        [MaxLength(256), Required]
        public string DireccionDeEnvio { get; set; } = "";

        [Required]
        public DateTime FechaEstimadaEntrega { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total { get; set; }

        [MaxLength(128), MinLength(3), Required]
        public string EstadoOrden { get; set; } = "";

        [Required]
        public DateTime Fecha { get; set; }

        public List<CarritoProducto>? CarritoProducto { get; set; }

        [ForeignKey("ReclamoId")]
        public Reclamos? Rcs { get; set; }

        [ForeignKey("FacturaId")]
        public Facturas? FAs { get; set; }

        [ForeignKey("ClienteId")]
        public ApplicationUser? Cliente { get; set; }
    }
}
