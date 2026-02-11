using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Application.DTOs
{
    public class LoginDTO
    {

        [Required(ErrorMessage = "O email é obrigatorio.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatoria.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
