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
    public class Empresas
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [MaxLength(128), MinLength(3), Required]
        public string Nombre { get; set; } = "";

        [MaxLength(128), MinLength(3), Required]
        public string RUT { get; set; } = "";

        public List<Categorias>? CategoriasAsociadas { get; set; }

        public List<Productos>? ProductosAsociados { get; set; }

        public List<Sucursales>? SucursalesAsociadas { get; set; }

        public List<Reclamos>? Reclamos { get; set; }

        public List<ApplicationUser>? Empleados { get; set; }
    }
}
