
using System.ComponentModel.DataAnnotations;

namespace RedeSocial.Domain.Models {
    public class PostsPhotos {
        public int PostsPhotosId { get; set; }

        [Required]
        [StringLength(150)]
        public string? PhotoName { get; set; }

        [Required]
        [StringLength(300)]
        public string? URL { get; set; }
        public int PostsId { get; set; }


        public Posts? Posts { get; set; }

    }
}
