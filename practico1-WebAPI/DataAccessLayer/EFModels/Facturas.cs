using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EFModels
{
    public class Facturas
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        public float TotalComisiones { get; set; } 

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFin { get; set; }
        
        public int EmpresaId { get; set; }

        [ForeignKey("EmpresaId")]
        public Empresas? EmpresaAsociada { get; set; }
    }
}
