using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Application.DTOs {
    public class UpdateUserDTO {
        public string? Bio { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ImageURL { get; set; }
        public string? Username { get; set; }
    }
}
