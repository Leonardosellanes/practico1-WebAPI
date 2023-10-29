﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EFModels
{
    public class Sucursales
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [MaxLength(128), MinLength(3), Required]
        public string Nombre { get; set; } = "";

        [MaxLength(128), MinLength(3), Required]
        public string Ubicacion { get; set; } = "";

        [MaxLength(128), MinLength(3), Required]
        public int TiempoEntrega { get; set; }

        public int EmpresaId { get; set; }
        public Empresas? Empresa { get; set; }
    }
}
