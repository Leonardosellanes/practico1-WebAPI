using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EFModels
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(128), MinLength(6),Required]
        public string Email {  get; set; }

        [MaxLength(128), MinLength(6), Required]
        public string Password { get; set; }

        [MaxLength(128), MinLength(5)]
        public string? Name { get; set; }

        [MaxLength(128), MinLength(5)]
        public string? LName { get; set; }

        [MaxLength(128), MinLength(7)]

        public bool IsAdmin { get; set; }

        public string? Address { get; set; }

        public List<OC>? OCs { get; set; }

        public List<Empresas>? Ecs { get; set; }

        public List<Opiniones>? OPs { get; set; }
    }
}