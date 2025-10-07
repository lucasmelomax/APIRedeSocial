
using System.ComponentModel.DataAnnotations;

namespace RedeSocial.Application.DTOs {
    public class UserPutDTO {
        public int UsersId { get; set; }

        [Required]
        [StringLength(150)]
        public string? Bio { get; set; }

        [Required]
        [StringLength(80)]
        public string? Name { get; set; }

        [Required]
        [StringLength(80)]
        public string? Email { get; set; }

        [Required]
        [StringLength(50)]
        public string? Password { get; set; }

        [Required]
        [StringLength(300)]
        public string? ImageURL { get; set; }

        [Required]
        [StringLength(50)]
        public string? Username { get; set; }
    }
}
