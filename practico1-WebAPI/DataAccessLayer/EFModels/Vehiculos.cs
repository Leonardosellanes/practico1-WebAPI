using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EFModels
{
    public class Vehiculos
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }

        [MaxLength(128), MinLength(3), Required]
        public string Nombres { get; set; } = "";

        [MaxLength(128), MinLength(3), Required]
        public string Matricula { get; set; } = "";

        public long PersonaId { get; set; }
        public Personas Persona { get; set; }
    }
}
