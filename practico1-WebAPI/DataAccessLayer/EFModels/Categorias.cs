﻿using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EFModels
{
    public class Categorias
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [MaxLength(128), MinLength(3), Required]
        public string Nombre { get; set; } = "";

        public int? CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categorias? CategoriaAsociada { get; set; }

        public int EmpresaId { get; set; }

        [ForeignKey("EmpresaId")]
        public Empresas? EmpresaAsociada { get; set; }

        public List<Productos>? ProductosAsociados { get; set; }
    }
}
