using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Domain.Models {
    public class Users {

        public int UserId { get; set; }
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
        public string? ImageURL{ get; set; }

        [Required]
        [StringLength(50)]
        public string? Username { get; set; }

        public ICollection<Followers>? Followers { get; set; } = new Collection<Followers>();
        public ICollection<Likes>? Likes { get; set; } = new Collection<Likes>();
        public ICollection<Comments>? Comments { get; set; } = new Collection<Comments>();
        public ICollection<Posts>? Posts { get; set; } = new Collection<Posts>();
    }
}
