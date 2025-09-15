using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedeSocial.Domain.Models;

namespace RedeSocial.Application.DTOs
{
    public class UsersDTO
    {
        public int UsersId { get; set; }
        public bool Active { get; set; }

        [Required]
        [StringLength(150)]
        public string? Bio { get; set; }
        public DateTime CreatedAt { get; set; }

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
