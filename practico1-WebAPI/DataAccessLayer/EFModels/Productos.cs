using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int CategoriaId { get; set; }
        public Categorias? Categoria { get; set; }
    }
}
