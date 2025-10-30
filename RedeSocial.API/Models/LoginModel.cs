using System.ComponentModel.DataAnnotations;

namespace RedeSocial.API.Models {
    public class LoginModel {

        [Required(ErrorMessage = "O email é obrigatorio.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatoria.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
