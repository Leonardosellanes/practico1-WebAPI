using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace DataAccessLayer.EFModels
{
    public class Productos
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [MaxLength(128), MinLength(3), Required]
        public string Titulo { get; set; } = "";

        [MaxLength(128), MinLength(3), Required]
        public string Descripcion { get; set; } = "";

        [MaxLength(128), MinLength(3), Required]
        public string Foto { get; set; } = "";

        [MaxLength(128), MinLength(3), Required]
        public int Precio { get; set; }

        [MaxLength(128), MinLength(3), Required]
        public string Tipo_iva { get; set; } = "";

        [MaxLength(128), MinLength(3), Required]
        public string Pdf { get; set; } = "";
        public int EmpresaId { get; set; }
        
        [ForeignKey("EmpresaId")]
        public Empresas? EmpresaAsociada { get; set; }

        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categorias? CategoriaAsociada { get; set; }

        public List<Opiniones>? OpinionesAsociadas { get; set; }
    }
}
