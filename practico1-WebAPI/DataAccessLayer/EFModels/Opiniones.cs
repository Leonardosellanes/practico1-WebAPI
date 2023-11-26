using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EFModels
{
    public class Opiniones
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [MaxLength(128), MinLength(3), Required]
        public string Titulo { get; set; } = "";

        [MaxLength(256), MinLength(3), Required]
        public string Descripcion { get; set; } = "";

        public int Estrellas { get; set; }

        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public Productos? ProductoAsociados { get; set; }

        [ForeignKey("ClienteId")]
        public ApplicationUser? Cliente { get; set; }
    }
}
