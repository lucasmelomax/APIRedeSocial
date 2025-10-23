
namespace RedeSocial.Application.DTOs {
    public class LikesDTO {
        public int LikesId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UsersId { get; set; }
        public int PostsId { get; set; }
    }
}
