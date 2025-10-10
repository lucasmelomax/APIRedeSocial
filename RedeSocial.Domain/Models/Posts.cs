
using System.ComponentModel.DataAnnotations;

namespace RedeSocial.Domain.Models {
    public class Posts {
        public int PostsId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [StringLength(150)]
        public string? TextContent { get; set; }

        public int CommentsId { get; set; }
        public int PostsPhotosId { get; set; }
        public int LikesId { get; set; }
        public int UsersId { get; set; }

        public List<Comments>? Comments { get; set; }
        public List<PostsPhotos>? PostsPhotos { get; set; }
        public List<Likes>? Likes { get; set; }
        public Users? Users { get; set; }
    }
}
