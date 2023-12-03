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
        public string? name { get; set; }
        public string? lName { get; set; }
        public string? address { get; set; }
        public string id { get; set; }
        public string email { get; set; }
    }
}
