using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EFModels
{
    public class CarritoProducto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }

        public int Cantidad { get; set; }

        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public Productos? POs { get; set; }

        public long OCId { get; set; }

        [ForeignKey("OCId")]
        public OC? OCs { get; set; }

    }
}