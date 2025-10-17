using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedeSocial.Domain.Models;

namespace RedeSocial.Application.DTOs {
    public class PostsPhotosDTO {
        public int PostsPhotosId { get; set; }

        [Required]
        [StringLength(150)]
        public string? PhotoName { get; set; }

        [Required]
        [StringLength(300)]
        public string? URL { get; set; }

        [Required]
        public int PostsId { get; set; }
    }
}
