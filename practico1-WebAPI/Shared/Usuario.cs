using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Usuario
    {
        public string? Name { get; set; }
        public string? LName { get; set; }
        public string? Address { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
    }
}
