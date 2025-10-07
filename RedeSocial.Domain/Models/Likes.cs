
namespace RedeSocial.Domain.Models {
    public class Likes {
        public int LikesId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UsersId { get; set; }
        public int PostsId { get; set; }


        public Users? Users { get; set; }
        public Posts? Posts { get; set; }

    }
}
