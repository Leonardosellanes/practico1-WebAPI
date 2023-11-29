using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization;

namespace DataAccessLayer.EFModels
{
    [DataContract(Name = "EFModels_OC")]
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

        public Reclamos? Rcs { get; set; } = null;
        public String ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public ApplicationUser? Cliente { get; set; } = null;


        public int? EmpresaId { get; set; }

        [ForeignKey("EmpresaId")]
        public Empresas? EmpresaAsociada { get; set; } = null;

        public int? SucursalId { get; set; }

        [ForeignKey("SucursalId")]
        public Sucursales? SucursalAsociada { get; set; } = null;
    }
}
