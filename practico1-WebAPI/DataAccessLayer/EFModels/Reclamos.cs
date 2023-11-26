using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EFModels
{
    public class Reclamos
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [MaxLength(128), MinLength(3), Required]
        public string Descripcion { get; set; } = "";
        [Required]
        public DateTime Fecha{ get; set; }
        public int EmpresaId { get; set; }

        [ForeignKey("EmpresaId")]
        public Empresas? EmpresaAsociada { get; set; }

        [ForeignKey("OcId")]
        public EFModels.OC? OrdenAsociada{ get; set; }
    }
}
